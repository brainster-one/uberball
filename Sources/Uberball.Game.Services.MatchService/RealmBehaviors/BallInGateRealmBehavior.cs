
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Enter/Leave realm events handler.</summary>
	sealed class BallInGateRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			var balls = realm.Entities.OfType<Ball>();
			var gates = realm.Entities.OfType<Gate>();

			foreach (var gate in gates) {
				foreach (var ball in balls) {
					if (ball.X > gate.X && ball.X < gate.X + 64 && ball.Y > gate.Y && ball.Y < gate.Y + 64) {
						ball.X = 0;
						ball.Y = 0;

						gate.Score += 1;
					}
				}
			}

		}

	}
}
