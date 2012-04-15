
namespace Uberball.Game.Services.MatchService.RealmBehaviors.Physics {
	using FarseerPhysics.Dynamics;
	using FarseerPhysics.Factories;
	using Logic.Entities;
	using Microsoft.Xna.Framework;

	class BallPhysicsBodyFactory : IPhysicsBodyFactory<Ball> {
		public Body Create(World world, Ball entity) {
			var body = BodyFactory.CreateCircle(world, 16.0f, .5f);
			body.Position = new Vector2(entity.X, entity.Y);
			body.BodyType = BodyType.Dynamic;
			body.Restitution = .7f;
			body.Friction = .7f;
			return body;
		}
	}
}
