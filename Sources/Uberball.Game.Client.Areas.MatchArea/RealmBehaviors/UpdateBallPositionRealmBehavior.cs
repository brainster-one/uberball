
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;

	public class UpdateBallPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {

			foreach (var player in realm.Entities.OfType<BallViewModel>()) {
				player.X += (player.NewX - player.X) * .2;
				player.Y += (player.NewY - player.Y) * .2;
				//player.X = player.NewX;
				//player.Y = player.NewY;
				realm.ModifyEntity(player);
			}
		}
	}
}
