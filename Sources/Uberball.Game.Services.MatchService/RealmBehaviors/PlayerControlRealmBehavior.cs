
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Physics calculation for realm.</summary>
	sealed class PlayerControlRealmBehavior : RealmBehavior {
		/// <summary>User input.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		/// <param name="state">Input state.</param>
		public override void Input(IRealm realm, User user, InputState state) {
			var player = (Player)user["player"];
			player.VectorX = (state.Get<bool>("right") ? 1 : state.Get<bool>("left") ? -1 : 0) * 20;
			player.VectorY = (state.Get<bool>("up") ? 1 : state.Get<bool>("down") ? -1 : 0) * 80;
			player.AimAngle = state.Get<float>("aimAngle");
		}
	}
}
