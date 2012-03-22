
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;

	public class UpdatePlayerPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {

			foreach (var player in realm.Entities.OfType<PlayerViewModel>()) {
				player.X += (player.NewX - player.X) * .1;
				player.Y += (player.NewY - player.Y) * .1;
				realm.ModifyEntity(player);
			}
		}
	}
}
