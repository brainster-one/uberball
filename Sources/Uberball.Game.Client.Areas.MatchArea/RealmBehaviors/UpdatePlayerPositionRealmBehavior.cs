
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using ViewModels.Entities;

	public class UpdatePlayerPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {

			foreach (var player in realm.Entities.OfType<PlayerViewModel>()) {
				player.AimAngle += (player.NewAimAngle - player.AimAngle) * .3;
				player.X += (player.NewX - player.X) * .3;
				player.Y += (player.NewY - player.Y) * .3;

				if ((player.NewX - player.X) > 1)
					player.State = "RunRight";
				else if ((player.NewX - player.X) < -1)
					player.State = "RunLeft";
				else
					player.State = "Stay";
				//player.X = player.NewX;
				//player.Y = player.NewY;
				realm.ModifyEntity(player);
			}
		}
	}
}
