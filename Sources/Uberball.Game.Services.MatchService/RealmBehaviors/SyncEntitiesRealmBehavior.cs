
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using Ardelme.Core;
	using Khrussk.NetworkRealm;

	class SyncEntitiesRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var entity in realm.Entities) {
				realm.ModifyEntity(entity);
			}
		}

		public SyncEntitiesRealmBehavior(RealmService service) {
			_service = service;
		}
		public override void AddEntity(IRealm realm, object entity) {
			_service.AddEntity(entity);
		}

		public override void RemoveEntity(IRealm realm, object entity) {
			_service.RemoveEntity(entity);
		}

		public override void ModifyEntity(IRealm realm, object entity) {
			_service.ModifyEntity(entity);
		}

		readonly RealmService _service;
	}
}
