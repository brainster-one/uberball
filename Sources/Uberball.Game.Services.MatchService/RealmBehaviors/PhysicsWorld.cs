
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FarseerPhysics.Common;
	using FarseerPhysics.Dynamics;
	using FarseerPhysics.Factories;
	using Logic.Entities;
	using Microsoft.Xna.Framework;

	/// <summary>Physics world.</summary>
	sealed class PhysicsWorld {
		/// <summary>Initializes a new instance of the PhysicsWorld class using specified world.</summary>
		/// <param name="world">Farseer World.</param>
		public PhysicsWorld(World world) {
			_world = world;
		}

		/// <summary>Adds entity to the world.</summary>
		/// <param name="entity">Entity.</param>
		public void Add(object entity) {
			Body body = null;
			if (entity.GetType() == typeof(Decoration)) return;
			if (entity.GetType() == typeof(Ball)) body = CreateBall((Ball)entity);
			if (entity.GetType() == typeof(Player)) body = CreatePlayer((Player)entity);
			if (entity.GetType() == typeof(Ground)) body = CreateGround((Ground)entity);
			if (entity.GetType() == typeof(Bullet)) body = CreateBullet((Bullet)entity);

			if (body == null) throw new InvalidOperationException(string.Format("Can not create physics body for {0}", entity.GetType()));
			_entities.Add(entity, body);
		}

		/// <summary>Removes entity from the world.</summary>
		/// <param name="entity">Entity.</param>
		public void Remove(object entity) {
			_world.RemoveBody(GetBody(entity));
			_entities.Remove(entity);
		}

		/// <summary>Sets linear velocity for entity.</summary>
		/// <param name="entity">Entity.</param>
		/// <param name="x">X.</param>
		/// <param name="y">Y.</param>
		public void SetLinearVelocity(object entity, float x, float y) {
			GetBody(entity).LinearVelocity = new Vector2(x, y);
		}

		/// <summary>Simulates physics.</summary>
		/// <param name="dt"></param>
		public void Update(double dt) {
			_world.Step((float)dt);
		}

		/// <summary>Gets list of entities.</summary>
		public IEnumerable<KeyValuePair<object, Body>> Entities { get { return _entities.AsEnumerable(); } }

		/// <summary>Returns physics body for entity.</summary>
		/// <param name="entity">Entity.</param>
		/// <returns>Physics body.</returns>
		private Body GetBody(object entity) {
			Body body;
			_entities.TryGetValue(entity, out body);
			if (body == null) throw new InvalidOperationException("Entity is not exist in physics world.");
			return body;
		}

		/// <summary>Creates physics body for player.</summary>
		/// <param name="entity">Player.</param>
		/// <returns>Physics body.</returns>
		private Body CreateBullet(Bullet entity) {
			var body = BodyFactory.CreateCircle(_world, 5.0f, .5f);
			body.Position = new Vector2(entity.X, entity.Y);
			body.BodyType = BodyType.Dynamic;
			body.IsBullet = true;
			body.Restitution = .7f;
			body.Friction = .7f;
			body.Mass *= .01f;
			_entities.Where(x => x.Key is Player).ToList().ForEach(x => body.IgnoreCollisionWith(x.Value));
			return body;
		}

		/// <summary>Creates physics body for player.</summary>
		/// <param name="entity">Player.</param>
		/// <returns>Physics body.</returns>
		private Body CreateBall(Ball entity) {
			var body = BodyFactory.CreateCircle(_world, 16.0f, .5f);
			body.Position = new Vector2(entity.X, entity.Y);
			body.BodyType = BodyType.Dynamic;
			body.Restitution = .7f;
			body.Friction = .7f;
			return body;
		}

		/// <summary>Creates physics body for player.</summary>
		/// <param name="entity">Player.</param>
		/// <returns>Physics body.</returns>
		private Body CreatePlayer(Player entity) {
			var body = BodyFactory.CreateCircle(_world, 16.0f, .5f);
			body.Position = new Vector2(entity.X, entity.Y);
			body.BodyType = BodyType.Dynamic;
			body.Restitution = body.Friction = .5f;
			return body;
		}

		/// <summary>Creates physics body for ground.</summary>
		/// <param name="entity">Ground.</param>
		/// <returns>Physics body.</returns>
		private Body CreateGround(Ground entity) {
			var vertices = new Vertices(entity.Points.Select(point => new Vector2(point.X, point.Y)).ToArray());
			var body = BodyFactory.CreateLoopShape(_world, vertices, .5f);
			return body;
		}

		/// <summary>Farseer world.</summary>
		readonly World _world;

		/// <summary>Entity to physics body map.</summary>
		readonly Dictionary<object, Body> _entities = new Dictionary<object, Body>();
	}
}
