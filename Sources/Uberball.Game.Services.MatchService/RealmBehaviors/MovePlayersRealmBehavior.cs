﻿
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Logic.Entities;

	/// <summary>Moves player by specified vector.</summary>
	class MovePlayersRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var player in realm.Entities.OfType<Player>()) {
				player.VectorY = 7;
				player.X += player.VectorX;
				player.Y += player.VectorY;
				realm.ModifyEntity(player);
			}
		}
	}
}
