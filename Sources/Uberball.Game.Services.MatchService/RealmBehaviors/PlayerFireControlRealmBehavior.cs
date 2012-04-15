
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Physics calculation for realm.</summary>
	sealed class PlayerFireControlRealmBehavior : RealmBehavior {
		/// <summary>User input.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		/// <param name="state">Keys.</param>
		public override void Input(IRealm realm, User user, InputState state) {
			var player = (Player)user["player"];
			var aimAngle = state.Get<float>("aimAngle");
			var fire = state.Get<bool>("fire");
			var aimAngleRad = aimAngle / 180.0f * Math.PI;

			if (fire) {
				var vectorX = (float)Math.Cos(aimAngleRad);
				var vectorY = (float)Math.Sin(aimAngleRad);
				var bullet = new Bullet {
					X = player.X + vectorX * 20, Y = player.Y + vectorY * 20,
					VectorX = vectorX * 25000,
					VectorY = vectorY * 25000
				};
				realm.AddEntity(bullet);
			}
		}
	}
}
