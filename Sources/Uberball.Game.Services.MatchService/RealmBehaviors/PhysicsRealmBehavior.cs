
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using FarseerPhysics.Dynamics;
	using Logic.Entities;
	using Microsoft.Xna.Framework;

	/// <summary>Physics calculation for realm.</summary>
	sealed class PhysicsRealmBehavior : RealmBehavior {
		/// <summary>Initializes a new instance of the PhysicsRealmBehavior class.</summary>
		public PhysicsRealmBehavior() {
			_physicsWorld = new PhysicsWorld(new World(new Vector2(0, 10)));
		}

		/// <summary>Entity added to realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void AddEntity(IRealm realm, object entity) {
			_physicsWorld.Add(entity);
		}

		/// <summary>Entity removed from realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void RemoveEntity(IRealm realm, object entity) {
			_physicsWorld.Remove(entity);
		}

		/// <summary>User input.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		/// <param name="state">Keys.</param>
		public override void Input(IRealm realm, User user, InputState state) {
			var player = (Player)user["player"];
			var vectorX = (state.Get<bool>("right") ? 1 : state.Get<bool>("left") ? -1 : 0) * 20;
			var vectorY = (state.Get<bool>("up") ? 1 : state.Get<bool>("down") ? -1 : 0) * 20;

			player.AimAngle = state.Get<float>("aimAngle");
			_physicsWorld.SetLinearVelocity(player, vectorX, vectorY);
		}

		/// <summary>Calculates physics and updates entities.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="delta">Delta time.</param>
		public override void Update(IRealm realm, double delta) {
			// Update physics
			_physicsWorld.Update(delta);

			// Update entities
			foreach (var entity in _physicsWorld.Entities) {
				var e = entity.Key;
				var body = entity.Value;

				if (e.GetType() == typeof(Player)) {
					var player = (Player)e;
					player.X = body.Position.X;
					player.Y = body.Position.Y;
				}
			}
		}

		/// <summary>Physics world.</summary>
		readonly PhysicsWorld _physicsWorld;
	}
}
