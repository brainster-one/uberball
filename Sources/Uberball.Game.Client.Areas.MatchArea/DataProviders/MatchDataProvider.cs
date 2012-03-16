
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Net;
	using System.Windows;
	using System.Windows.Media;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Client.Areas.MatchArea.Behaviors;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

	public class Ent {
		/*public Ent(int id, object entity, object entityViewModel) {
			Id = _id;
			Entity = entity;
			EntityViewModel = entityViewModel;
		}*/

		public int Id;
		public object Entity;
		public object ViewModel;
	}

	/*public class EntDataStorage {
		public 
	}*/

	public class MatchDataProvider {
		public MatchDataProvider() {
			Entities = new ObservableCollection<object>();
			_client.Connected += new System.EventHandler<RealmEventArgs>(_client_Connected);
			_client.EntityAdded += Client_EntityAdded;
			_client.EntityRemoved += new System.EventHandler<RealmEventArgs>(_client_EntityRemoved);
			_client.EntityModified += new System.EventHandler<RealmEventArgs>(_client_EntityModified);
			_client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());

			_realm.AddBehavior(new UpdatePlayerPositionRealmBehavior());

			/* looks like shit */
			CompositionTarget.Rendering += (x, y) => {
				lock (_realm) { _realm.Update(0); }
			};
		}

		public ObservableCollection<object> Entities { get; private set; }

		public event EventHandler Connected;

		public void Connect(IPEndPoint endpoint) {
			_client.Connect(endpoint);
		}

		void _client_Connected(object sender, RealmEventArgs e) {
			var evnt = Connected;
			if (evnt != null) evnt(this, new EventArgs());
		}

		void Client_EntityAdded(object sender, RealmEventArgs e) {
			lock (_realm) {
				var ent = new Ent { Id = e.EntityId, Entity = e.Entity, ViewModel = CreateViewModel(e.Entity) };
				_storage.Add(ent);
				_realm.AddEntity(ent.ViewModel);
				Deployment.Current.Dispatcher.BeginInvoke(() => Entities.Add(ent.ViewModel));
			}
		}

		void _client_EntityRemoved(object sender, RealmEventArgs e) {

		}

		void _client_EntityModified(object sender, RealmEventArgs e) {
			lock (_realm) {
				var ent = _storage.First(x => e.EntityId == x.Id);
				e.EntityDiffData.ApplyChanges(ent.Entity);
				UpdateViewModel(ent.ViewModel, ent.Entity);
			}
		}

		private object CreateViewModel(object entity) {
			return new PlayerViewModel(); /* TODO: convert to viewModel correct */
		}

		private void UpdateViewModel(object viewModel, object entity) {
			var playerVm = (PlayerViewModel)viewModel;
			var playerEn = (Player)entity;

			playerVm.Name = playerEn.Name + string.Format("({0}, {1})", playerEn.X, playerEn.Y);
			playerVm.NewX = playerEn.X;
			playerVm.NewY = playerEn.Y;
		}

		// <summary>Client's realm. Provides client's realm calculations.</summary>
		private Realm _realm = new Realm();

		/// <summary>Client interface to connect to remote service.</summary>
		private RealmClient _client = new RealmClient();

		private List<Ent> _storage = new List<Ent>();
	}
}
