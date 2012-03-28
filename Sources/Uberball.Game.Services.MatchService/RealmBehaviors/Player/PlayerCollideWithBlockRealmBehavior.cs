
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Moves player by specified vector.</summary>
	class PlayerCollideWithBlockRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			var players = realm.Entities.OfType<Player>();
			var blocks = realm.Entities.OfType<Block>();

			foreach (var block in blocks) {
				foreach (var player in players) {
					/*// r2 - box
					bool intersect = !(block.X > player.X + 64
						|| block.X + 64 < player.X
						|| block.Y > player.Y + 64
						|| block.Y + 64 < player.Y);

					
					if (intersect) {
						if (player.X + 64 > block.X) player.X = block.X - 64;
						//if (player.X < block.X + 64) player.X = block.X + 64;

						if (player.Y + 64 > block.Y) player.Y = block.Y - 64;
						
					}*/
					const int cts = 12;

					var collideRight = PointInsideRect(player.X + 64 + cts, player.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					var collideLeft = PointInsideRect(player.X - cts, player.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					var collideBottom = PointInsideRect(player.X + 32, player.Y + 64 + cts, block.X, block.Y, block.X + 64, block.Y + 64);

					if (collideLeft && player.X <= block.X + 64) { player.X = block.X + 64; player.VectorX = 0; }
					if (collideRight && player.X + 64 >= block.X) { player.X = block.X - 64; player.VectorX = 0; }
					if (collideBottom && player.Y + 64 >= block.Y) { player.Y = block.Y - 64; player.VectorY = 0; }
				}
			}
			
			 

			
		}

		public bool PointInsideRect(double pointX, double pointY, double rectLeft, double rectTop, double rectRight, double rectBottom) {
			return pointX >= rectLeft && pointX <= rectRight && pointY >= rectTop && pointY <= rectBottom;
		}
	}
}
