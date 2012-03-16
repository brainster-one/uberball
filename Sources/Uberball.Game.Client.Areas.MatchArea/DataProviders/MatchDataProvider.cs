
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Net;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Client.Areas.MatchArea.Behaviors;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

	public class MatchDataProvider {
		public MatchDataProvider() {
			Entities = new ObservableCollection<object>();
			_client.Connected += new System.EventHandler<RealmEventArgs>(_client_Connected);
			_client.EntityAdded += Client_EntityAdded;
			_client.EntityRemoved += new System.EventHandler<RealmEventArgs>(_client_EntityRemoved);
			_client.EntityModified += new System.EventHandler<RealmEventArgs>(_client_EntityModified);
			_client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());

			_realm.AddBehavior(new UpdateViewModelsBehavior(Entities));
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
			_entityIds.Add(e.EntityId, e.Entity);
			_realm.AddEntity(e.Entity);
		}

		void _client_EntityRemoved(object sender, RealmEventArgs e) {
			_entityIds.Remove(e.EntityId);
			_realm.RemoveEntity(e.Entity);
		}

		void _client_EntityModified(object sender, RealmEventArgs e) {
			object entity = _entityIds[e.EntityId];
			e.EntityDiffData.ApplyChanges(entity);
			_realm.ModifyEntity(entity);
			//Deployment.Current.Dispatcher.BeginInvoke(() => Entities.Add(entity));
		}

		private RealmClient _client = new RealmClient();
		private Realm _realm = new Realm();
		private Dictionary<int, object> _entityIds = new Dictionary<int, object>();
	}
}
