
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using Ardelme.Core;
	using Khrussk.NetworkRealm;

	class SyncEntitiesRealmBehavior : RealmBehavior {
		private Khrussk.NetworkRealm.RealmService _service;

		public override void Update(IRealm realm, double delta) {
			foreach (var entity in realm.Entities) {
				realm.ModifyEntity(entity);
			}
		}

		public SyncEntitiesRealmBehavior(RealmService _service) {
			this._service = _service;
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
	}
}
