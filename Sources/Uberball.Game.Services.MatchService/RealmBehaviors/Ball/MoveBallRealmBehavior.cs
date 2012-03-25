﻿
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Logic.Entities;

	/// <summary>Moves player by specified vector.</summary>
	class MoveBallRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			foreach (var ball in realm.Entities.OfType<Ball>()) {
				ball.VectorY += 5;
				ball.X += ball.VectorX;
				ball.Y += ball.VectorY;
			}
		}
	}
}
