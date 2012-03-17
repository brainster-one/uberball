
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Net;
	using System.Windows;
	using System.Windows.Media;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Client.Areas.MatchArea.Behaviors;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

	/// <summary>Entity metadata.</summary>
	public class EntityInfo {
		/// <summary>Gets or sets entity network Id.</summary>
		public int Id { get; set; }

		/// <summary>Entity itself.</summary>
		public object Entity { get; set; }

		/// <summary>Gets or sets entity view model.</summary>
		public object ViewModel { get; set; }
	}

	/// <summary>Match data provider.</summary>
	public sealed class MatchDataProvider {
		/// <summary>Initializes a new instance of the MatchDataProvider class.</summary>
		public MatchDataProvider() {
			Entities = new ObservableCollection<object>();
			_client.Protocol.RegisterPacketType(typeof(InputPacket), new InputPacketSrializer());
			_client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			_client.Connected += _client_Connected;
			_client.ConnectionFailed += new EventHandler<RealmEventArgs>(_client_ConnectionFailed);
			_client.Disconnected += new EventHandler<RealmEventArgs>(_client_Disconnected);
			_client.EntityAdded += Client_EntityAdded;
			_client.EntityRemoved += _client_EntityRemoved;
			_client.EntityModified += _client_EntityModified;
			_realm.AddBehavior(new UpdatePlayerPositionRealmBehavior());
			_storage.CollectionChanged += EntitiesCollectionChanged;
			_mappers.Add(typeof(Player), new PlayerMapper());

			/* looks like shit */
			CompositionTarget.Rendering += (x, y) => {
				lock (_realm) { _realm.Update(0); }
			};
		}

		public ObservableCollection<object> Entities { get; private set; }

		public event EventHandler Connected;
		public event EventHandler ConnectionFailed;
		public event EventHandler Disconnected;

		public void Connect(IPEndPoint endpoint) {
			_client.Connect(endpoint);
		}

		public void Input(bool u, bool r, bool d, bool l) {
			_client.Send(new InputPacket {
				IsUpPressed = u, IsRightPressed = r, IsDownPressed = d, IsLeftPressed = l
			});
		}

		void _client_Connected(object sender, RealmEventArgs e) {
			var evnt = Connected;
			if (evnt != null) evnt(this, new EventArgs());
		}

		void _client_Disconnected(object sender, RealmEventArgs e) {
			var evnt = Disconnected;
			if (evnt != null) evnt(this, new EventArgs());
		}

		void _client_ConnectionFailed(object sender, RealmEventArgs e) {
			var evnt = ConnectionFailed;
			if (evnt != null) evnt(this, new EventArgs());
		}

		void Client_EntityAdded(object sender, RealmEventArgs e) {
			lock (_realm) {
				object viewModel = null;
				var mapper = _mappers[e.Entity.GetType()];
				mapper.Map(e.Entity, ref viewModel);
				_storage.Add(new EntityInfo { Id = e.EntityId, Entity = e.Entity, ViewModel = viewModel });
			}
		}

		void _client_EntityRemoved(object sender, RealmEventArgs e) {

		}

		void _client_EntityModified(object sender, RealmEventArgs e) {
			lock (_realm) {
				var ent = _storage.First(x => e.EntityId == x.Id);
				var viewModel = ent.ViewModel;
				var mapper = _mappers[ent.Entity.GetType()];
				e.EntityDiffData.ApplyChanges(ent.Entity);
				Deployment.Current.Dispatcher.BeginInvoke(() => mapper.Map(ent.Entity, ref viewModel));
			}
		}

		void EntitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (EntityInfo item in e.NewItems) {
					_realm.AddEntity(item.ViewModel);
					Deployment.Current.Dispatcher.BeginInvoke(() => Entities.Add(item.ViewModel));
				}
			}
		}

		// <summary>Client's realm. Provides client's realm calculations.</summary>
		private Realm _realm = new Realm();

		/// <summary>Client interface to connect to remote service.</summary>
		private RealmClient _client = new RealmClient();

		private ObservableCollection<EntityInfo> _storage = new ObservableCollection<EntityInfo>();
		private Dictionary<Type, IMapper> _mappers = new Dictionary<Type, IMapper>();
	}
}
