
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider {
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
	using Uberball.Game.Client.Areas.MatchArea.RealmBehaviors;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider;

	/// <summary>Match data provider.</summary>
	public sealed class MatchDataProvider {
		/// <summary>Initializes a new instance of the MatchDataProvider class.</summary>
		public MatchDataProvider() {
			Entities = new ObservableCollection<object>();
			_client.Protocol.RegisterPacketType(typeof(InputPacket), new InputPacketSrializer());
			_client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			
			_client.Connected += (s, e) => { var evnt = Connected; if (evnt != null) evnt(this, new EventArgs()); };
			_client.ConnectionFailed += (s, e) => { var evnt = ConnectionFailed; if (evnt != null) evnt(this, new EventArgs()); };
			_client.Disconnected += (s, e) => { var evnt = Disconnected; if (evnt != null) evnt(this, new EventArgs()); };
			
			_client.EntityAdded += (s,e) => _manager.Add(e.EntityInfo.Id, e.EntityInfo.Entity);
			_client.EntityRemoved += (s,e) => _manager.Remove(e.EntityInfo.Id);
			_client.EntityModified += (s, e) => _manager.ModifyEntity(e.EntityInfo.Id, e.EntityInfo.Diff);
			_realm.AddBehavior(new UpdatePlayerPositionRealmBehavior());

			_manager = new EntityManager(_realm, Entities);

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

		private EntityManager _manager;

		// <summary>Client's realm. Provides client's realm calculations.</summary>
		private Realm _realm = new Realm();

		/// <summary>Client interface to connect to remote service.</summary>
		private RealmClient _client = new RealmClient();
	}
}
