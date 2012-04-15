
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using ViewModels.Entities;

	public class UpdateBulletPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var bullet in realm.Entities.OfType<BulletViewModel>()) {
				bullet.X += (bullet.NewX - bullet.X) * .5f;
				bullet.Y += (bullet.NewY - bullet.Y) * .5f;
				realm.ModifyEntity(bullet);
			}
		}
	}
}
