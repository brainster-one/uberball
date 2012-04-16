
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using System.Collections.Generic;
	using Ardelme.Core;
	using FarseerPhysics.Dynamics;
	using Logic.Entities;
	using Microsoft.Xna.Framework;
	using Physics;

	/// <summary>Physics calculation for realm.</summary>
	sealed class PhysicsRealmBehavior : RealmBehavior {

		/// <summary>Entity added to realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void AddEntity(IRealm realm, object entity) {
			Body body = null;
			if (entity.GetType() == typeof(Decoration)) return;
			if (entity.GetType() == typeof(Gate)) return;
			if (entity.GetType() == typeof(Ball)) body = new BallPhysicsBodyFactory().Create(_world, (Ball)entity);
			if (entity.GetType() == typeof(Player)) body = new PlayerPhysicsBodyFactory().Create(_world, (Player)entity);
			if (entity.GetType() == typeof(Ground)) body = new GroundPhysicsBodyFactory().Create(_world, (Ground)entity);
			if (entity.GetType() == typeof(Bullet)) body = new BulletPhysicsBodyFactory(realm).Create(_world, (Bullet)entity);

			if (body == null) throw new InvalidOperationException(string.Format("Can not create physics body for {0}", entity.GetType()));
			_entities.Add(entity, body);
		}

		/// <summary>Entity removed from realm.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="entity">Entity.</param>
		public override void RemoveEntity(IRealm realm, object entity) {
			_world.RemoveBody(GetPhysicsBody(entity));
			_entities.Remove(entity);
		}

		/// <summary>Calculates physics and updates entities.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="delta">Delta time.</param>
		public override void Update(IRealm realm, double delta) {
			// Update physics
			_world.Step((float)delta);

			// Update entities
			foreach (var entity in _entities) {
				var e = entity.Key;
				var body = entity.Value;

				if (e.GetType() == typeof(Player)) {
					var player = (Player)e;
					player.X = body.Position.X;
					player.Y = body.Position.Y;

					//body.LinearVelocity = new Vector2(player.VectorX, player.VectorY);
					body.ApplyLinearImpulse(new Vector2(player.VectorX, player.VectorY));
				}
				if (e.GetType() == typeof(Ball)) {
					var ball = (Ball)e;
					ball.X = body.Position.X;
					ball.Y = body.Position.Y;

					body.ApplyLinearImpulse(new Vector2(ball.VectorX, ball.VectorY));
					ball.VectorX = ball.VectorY = 0;
				}
				if (e.GetType() == typeof(Bullet)) {
					var bullet = (Bullet)e;
					bullet.X = body.Position.X;
					bullet.Y = body.Position.Y;
					if (body.LinearVelocity.Length() < 1)
						body.ApplyForce(new Vector2(bullet.VectorX, bullet.VectorY));
					//bullet.VectorX = bullet.VectorY = 0;
				}
			}
		}

		/// <summary>Returns physics body for entity.</summary>
		/// <param name="entity">Entity.</param>
		/// <returns>Physics body.</returns>
		private Body GetPhysicsBody(object entity) {
			Body body;
			_entities.TryGetValue(entity, out body);
			if (body == null) throw new InvalidOperationException("Entity is not exist in physics world.");
			return body;
		}

		/// <summary>Farseer world.</summary>
		readonly World _world = new World(new Vector2(0, 10f));

		/// <summary>Entity to physics body map.</summary>
		readonly Dictionary<object, Body> _entities = new Dictionary<object, Body>();

	}
}
