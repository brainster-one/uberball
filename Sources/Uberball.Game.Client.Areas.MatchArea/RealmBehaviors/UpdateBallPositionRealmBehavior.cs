
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using ViewModels.Entities;

	public class UpdateBallPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var ball in realm.Entities.OfType<BallViewModel>()) {
				ball.X += (ball.NewX - ball.X) * .5;
				ball.Y += (ball.NewY - ball.Y) * .5;
				realm.ModifyEntity(ball);
			}
		}
	}
}
