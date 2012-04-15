
namespace Uberball.Game.Services.MatchService.RealmBehaviors.Physics {
	using FarseerPhysics.Dynamics;

	interface IPhysicsBodyFactory<in T> {
		Body Create(World world, T entity);
	}
}
