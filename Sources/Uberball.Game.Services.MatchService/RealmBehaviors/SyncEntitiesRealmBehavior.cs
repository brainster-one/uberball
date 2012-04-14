
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Logic.Entities;

	/// <summary>Sync entities behavior.</summary>
	sealed class SyncEntitiesRealmBehavior : RealmBehavior {
		/// <summary>Initializes a new instance of the SyncEntitiesRealmBehavior class using specified world.</summary>
		/// <param name="service">Realm service.</param>
		public SyncEntitiesRealmBehavior(RealmService service) {
			_service = service;
		}

		/// <summary>Update realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="delta">Time passed since last update.</param>
		public override void Update(IRealm realm, double delta) {
			foreach (var entity in realm.Entities.OfType<Player>()) { realm.ModifyEntity(entity); }
			foreach (var entity in realm.Entities.OfType<Ball>()) { realm.ModifyEntity(entity); }
			foreach (var entity in realm.Entities.OfType<Bullet>()) { realm.ModifyEntity(entity); }

			// It's time for sync
			if (DateTime.Now > _nextUpdate) {
				foreach (var ent in _list) {
					if (ent.Value == EntityState.Added) _service.AddEntity(ent.Key);
					if (ent.Value == EntityState.Modified) _service.ModifyEntity(ent.Key);
					if (ent.Value == EntityState.Removed) _service.RemoveEntity(ent.Key);
				}
				_list.Clear();
				_nextUpdate = DateTime.Now.AddMilliseconds(100);
			}
		}
		
		/// <summary>New entity added to realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void AddEntity(IRealm realm, object entity) {
			_list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Added));
		}

		/// <summary>Entity removed from realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void RemoveEntity(IRealm realm, object entity) {
			_list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Removed));
		}

		/// <summary>Entity's state modified.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void ModifyEntity(IRealm realm, object entity) {
			if (!_list.Any(x => x.Key == entity && x.Value == EntityState.Modified))
				_list.Add(new KeyValuePair<object, EntityState>(entity, EntityState.Modified));
		}


		/// <summary>Realm service.</summary>
		readonly RealmService _service;

		/// <summary>List of entity states.</summary>
		readonly List<KeyValuePair<object, EntityState>> _list = new List<KeyValuePair<object, EntityState>>();

		/// <summary>Next sync time.</summary>
		private DateTime _nextUpdate = DateTime.Now;
		
	}
}
