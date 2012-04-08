
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Collections.Generic;
	using System.Linq;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;

	class SyncEntitiesRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var entity in realm.Entities) {
				realm.ModifyEntity(entity);
			}
		}

		public override void AddEntity(IRealm realm, object entity) {
			lock (_list) { _list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Added)); }
		}

		public override void RemoveEntity(IRealm realm, object entity) {
			lock (_list) { _list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Removed)); }
		}

		public override void ModifyEntity(IRealm realm, object entity) {
			lock (_list) {
				if (!_list.Any(x => x.Key == entity && x.Value == EntityState.Modified))
					_list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Modified));
			}
		}

		public void Clear() {
			lock (_list) { _list.Clear(); }
		}

		public IEnumerable<KeyValuePair<object, EntityState>> EntityStates { get { return _list.AsReadOnly(); } }
		readonly List<KeyValuePair<object, EntityState>> _list = new List<KeyValuePair<object, EntityState>>();
	}
}
