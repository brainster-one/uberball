﻿
namespace Uberball.Game.Services.MatchService.RealmBehaviors.Physics {
	using Ardelme.Core;
	using FarseerPhysics.Dynamics;
	using FarseerPhysics.Factories;
	using Logic.Entities;
	using Microsoft.Xna.Framework;

	class BulletPhysicsBodyFactory : IPhysicsBodyFactory<Bullet> {
		public BulletPhysicsBodyFactory(IRealm realm) {
			_realm = realm;
		}

		public Body Create(World world, Bullet entity) {
			var body = BodyFactory.CreateCircle(world, 5.0f, .5f);
			body.Position = new Vector2(entity.X - 5.0f, entity.Y - 5.0f);
			body.BodyType = BodyType.Dynamic;
			body.IsBullet = true;
			body.Restitution = .7f;
			body.Friction = .7f;
			body.IgnoreGravity = true;
			body.Mass *= .001f;

			var hasBeenRemoved = false;
			body.OnCollision += (a, b, contact) => {
				if (hasBeenRemoved) return true; // several collisions per time
				hasBeenRemoved = true;
				_realm.RemoveEntity(entity);
				return true;
			};

			return body;
		}


		readonly IRealm _realm;
	}
}
