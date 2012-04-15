
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using System.Linq;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Physics calculation for realm.</summary>
	sealed class PlayerKickBallControlRealmBehavior : RealmBehavior {
		/// <summary>User input.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		/// <param name="state">Keys.</param>
		public override void Input(IRealm realm, User user, InputState state) {
			var player = (Player)user["player"];
			var aimAngle = state.Get<float>("aimAngle");
			var kickBall = state.Get<bool>("kick");
			var aimAngleRad = aimAngle / 180.0f * Math.PI;
			
			//
			if (kickBall) {				
				foreach (var ball in realm.Entities.OfType<Ball>()) {
					ball.VectorX = (float)Math.Cos(aimAngleRad) * 25000;
					ball.VectorY = (float)Math.Sin(aimAngleRad) * 25000;
				}
			}
		}
	}
}
