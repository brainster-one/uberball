﻿
namespace Uberball.Game.Client.Areas.MatchArea.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;
	using System;

	public class UpdatePlayerPositionRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {

			foreach (var player in realm.Entities.OfType<PlayerViewModel>()) {
				player.X += (player.NewX - player.X) * .2;
				player.Y += (player.NewY - player.Y) * .2;

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
