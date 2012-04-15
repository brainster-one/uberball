
namespace Uberball.Game.Services.MatchService.RealmBehaviors.Physics {
	using System.Linq;
	using FarseerPhysics.Common;
	using FarseerPhysics.Dynamics;
	using FarseerPhysics.Factories;
	using Logic.Entities;
	using Microsoft.Xna.Framework;

	class GroundPhysicsBodyFactory : IPhysicsBodyFactory<Ground> {
		public Body Create(World world, Ground entity) {
			var vertices = new Vertices(entity.Points.Select(point => new Vector2(point.X, point.Y)).ToArray());
			var body = BodyFactory.CreateLoopShape(world, vertices, .5f);
			return body;
		}
	}
}
