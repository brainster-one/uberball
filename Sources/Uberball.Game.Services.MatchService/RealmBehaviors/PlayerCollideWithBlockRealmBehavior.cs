
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Uberball.Game.Logic.Entities;

	/// <summary>Moves player by specified vector.</summary>
	class PlayerCollideWithBlockRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			var players = realm.Entities.OfType<Player>();
			var blocks = realm.Entities.OfType<Block>();

			foreach (var block in blocks) {
				foreach (var player in players) {
					// r2 - box
						bool intersect = !(block.X > player.X + 64
							|| block.X + 64 < player.X
							|| block.Y > player.Y + 64
							|| block.Y + 64 < player.X);

					
					if (intersect) {
						if (player.Y + 48 > block.Y) player.Y = block.Y - 48;
					}
				}
			}
			
			 

			
		}

		public void Overlap(int x1, int y1, int x2, int y2) {

		}
	}
}
