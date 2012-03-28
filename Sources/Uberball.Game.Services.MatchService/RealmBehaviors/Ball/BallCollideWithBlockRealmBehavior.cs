
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System.Linq;
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Moves player by specified vector.</summary>
	class BallCollideWithBlockRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			var balls = realm.Entities.OfType<Ball>();
			var blocks = realm.Entities.OfType<Block>();

			foreach (var block in blocks) {
				foreach (var ball in balls) {
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

					var collideRight = PointInsideRect(ball.X + 64 + cts, ball.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					var collideLeft = PointInsideRect(ball.X - cts, ball.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					bool collideBottom = PointInsideRect(ball.X + 32, ball.Y + 64 + cts, block.X, block.Y, block.X + 64, block.Y + 64);

					if (collideLeft && ball.X <= block.X + 64) { ball.X = block.X + 64; ball.VectorX *= -.5; }
					if (collideRight && ball.X + 64 >= block.X) { ball.X = block.X - 64; ball.VectorX *= -.5; }
					if (collideBottom && ball.Y + 64 >= block.Y) { ball.Y = block.Y - 64; ball.VectorY *= -.5; ball.VectorX *= .5; }
				}
			}
			
			 

			
		}

		public bool PointInsideRect(double pointX, double pointY, double rectLeft, double rectTop, double rectRight, double rectBottom) {
			return pointX >= rectLeft && pointX <= rectRight && pointY >= rectTop && pointY <= rectBottom;
		}
	}
}
