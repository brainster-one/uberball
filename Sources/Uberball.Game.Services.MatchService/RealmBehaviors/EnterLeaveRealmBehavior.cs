
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Enter/Leave realm events handler.</summary>
	sealed class EnterLeaveRealmBehavior : RealmBehavior {
		/// <summary>New user joined to the game as observer.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		/// <returns>Aceept user to the game?</returns>
		public override bool Enter(IRealm realm, User user) {
			user["player"] = new Player {
				Name = "Player_" + DateTime.Now.Millisecond, 
				ClientSessionId = user.Session
			};
			realm.AddEntity(user["player"]);
			return true;
		}

		/// <summary>User outs from the game.</summary>
		/// <param name="realm">Realm.</param>
		/// <param name="user">User.</param>
		public override void Leave(IRealm realm, User user) {
			realm.RemoveEntity(user["player"]);
		}
	}
}
