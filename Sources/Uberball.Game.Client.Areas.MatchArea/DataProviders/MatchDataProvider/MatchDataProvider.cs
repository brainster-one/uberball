
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider {
	using System;
	using System.Collections.ObjectModel;
	using System.Net;
	using System.Windows.Media;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Client.Areas.MatchArea.RealmBehaviors;
	using Uberball.Game.NetworkProtocol;
	using Khrussk;

	public class MatchDataProviderEventArgs : EventArgs {
		public MatchDataProviderEventArgs(ConnectionState connectionState) {
			ConnectionState = connectionState;
		}
		public ConnectionState ConnectionState { get; private set; }
	}

	/// <summary>Match data provider.</summary>
	public sealed class MatchDataProvider {
		/// <summary>Initializes a new instance of the MatchDataProvider class.</summary>
		public MatchDataProvider() {
			Entities = new ObservableCollection<object>();
			_manager = new EntityManager(_realm, Entities);
			_realm.AddBehavior(new UpdatePlayerPositionRealmBehavior());
			_client.ConnectionStateChanged += _client_ConnectionStateChanged;
			_client.EntityStateChanged += _client_EntityStateChanged;
			
			/* looks like shit */
			CompositionTarget.Rendering += (x, y) => {
				lock (_realm) { _realm.Update(0); }
			};
		}

		void _client_ConnectionStateChanged(object sender, RealmClientEventArgs e) {
			var evnt = ConnectionStateChanged;
			if (evnt != null) evnt(this, new MatchDataProviderEventArgs(e.ConnectionState));
		}

		void _client_EntityStateChanged(object sender, RealmClientEventArgs e) {
			if (e.EntityInfo.Action == EntityNetworkAction.Added) 
				_manager.Add(e.EntityInfo.Id, e.EntityInfo.Entity);
			else if (e.EntityInfo.Action == EntityNetworkAction.Removed)
				_manager.Remove(e.EntityInfo.Id);
			else if (e.EntityInfo.Action == EntityNetworkAction.Modified)
				_manager.ModifyEntity(e.EntityInfo.Id, e.EntityInfo.Diff);
		}

		public ObservableCollection<object> Entities { get; private set; }

		public event EventHandler<MatchDataProviderEventArgs> ConnectionStateChanged;

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
		private RealmClient _client = new RealmClient(new UberballProtocol());
	}
}
