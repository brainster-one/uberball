﻿
using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Uberball.Game.Logic.Entities;
using System.Linq;

namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using Ardelme.Core;

	class Physics : RealmBehavior {
		public Physics() {
			_world = new World(new Vector2(0, 10));
		}

		public override void Input(Ardelme.Core.IRealm realm, Ardelme.Core.User user, int[] keys) {
			if (keys.Length == 0) return;
			foreach (var entity in _entities) {
				if (entity.Key is Player) {
					
				}
			}
		}

		public override void AddEntity(IRealm realm, object entity) {
			var x = 0.0d;
			var y = 0.0d;
			if (entity is Player) {
				x = ((Player)entity).X;
				y = ((Player)entity).Y;
			}
			if (entity is Block) {
				x = ((Block)entity).X;
				y = ((Block)entity).Y;
			}

			var body = entity is Player ?
				BodyFactory.CreateCircle(_world, 16.0f, .5f) :
				BodyFactory.CreateRectangle(_world, 64, 64, .5f, new Vector2D(x, y));

			body.Position = new Vector2((float)x, (float)y);
			body.BodyType = entity is Player ? BodyType.Dynamic : BodyType.Static;
			//body.Mass *= 0.1f;
			//body.Restitution = 0; 1.3f;
			//body.Friction = 0.5f;
			_entities.Add(entity, body);
			
		}

		public override void Update(IRealm realm, double delta) {
			_world.Step(0.016f);
			foreach (var entity in _entities) {
				if (entity.Key is Player) {
					((Player)entity.Key).X = entity.Value.Position.X +16.0f;
					((Player)entity.Key).Y = entity.Value.Position.Y +16.0f;
					entity.Value.ApplyLinearImpulse(new Vector2(0, 100 * (float)((Player)entity.Key).VectorY));
					entity.Value.LinearVelocity = new Vector2((float) ((Player) entity.Key).VectorX,
															  entity.Value.LinearVelocity.Y);
					/*if (((Player)entity.Key).VectorY > 0)
						entity.Value.ApplyForce(new Vector2(1000, -1000));*/
					//entity.Value.ApplyForce(new Vector2(75.0f * (float)((Player)entity.Key).VectorX, 300.0f * (float)((Player)entity.Key).VectorY));
					realm.ModifyEntity(entity.Key);
				}
				if (entity.Key is Block) {
					((Block)entity.Key).X = entity.Value.Position.X;
					((Block)entity.Key).Y = entity.Value.Position.Y;
					realm.ModifyEntity(entity.Key);
				}
			}
		}

		private Dictionary<object, Body> _entities = new Dictionary<object, Body>();
		private World _world;
	}
}