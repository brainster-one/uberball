
using System;

namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using ViewModels.Entities;

	public class UpdateBulletPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var bullet in realm.Entities.OfType<BulletViewModel>()) {
				if (Math.Abs(bullet.NewX - bullet.X) > 128 || Math.Abs(bullet.NewY - bullet.Y) > 128) {
					bullet.X = bullet.NewX;
					bullet.Y = bullet.NewY;
				}


				bullet.X += (bullet.NewX - bullet.X) * .5;
				bullet.Y += (bullet.NewY - bullet.Y) * .5;
				realm.ModifyEntity(bullet);
			}
		}
	}
}
