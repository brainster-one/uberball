using System.Linq;
using Ardelme.Core;
using Uberball.Game.Logic.Entities;
using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;

namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	public class UpdatePlayerPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {

			/* TODO Operate with ViewModels objects, not entities. */

			foreach (var player in realm.Entities.Cast<PlayerViewModel>()) {
				player.X += (player.NewX - player.X) * .1;
				player.Y += (player.NewY - player.Y) * .1;
				realm.ModifyEntity(player);
			}
		}
	}
}
