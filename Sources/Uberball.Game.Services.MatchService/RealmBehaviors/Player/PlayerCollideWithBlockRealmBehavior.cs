

namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using System;
	using System.Linq;
	using Ardelme.Core;
	using Logic.Entities;

	struct Vector2D {
		public Vector2D(double x, double y) {
			X = x;
			Y = y;
		}
		public double X;
		public double Y;
	}

	struct Segment2D {
		public Vector2D Start;
		public Vector2D End;
	}

	/// <summary>Moves player by specified vector.</summary>
	class PlayerCollideWithBlockRealmBehavior : RealmBehavior {
		public override void Update(IRealm realm, double delta) {
			var players = realm.Entities.OfType<Player>();
			var blocks = realm.Entities.OfType<Block>();

			foreach (var player in players)
			{
				var ox = player.X;
				var stayOnGround = false;

				foreach (var block in blocks) {

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
					/*const int cts = 12;

					var collideRight = PointInsideRect(player.X + 64 + cts, player.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					var collideLeft = PointInsideRect(player.X - cts, player.Y + 32, block.X, block.Y, block.X + 64, block.Y + 64);
					var collideBottom = PointInsideRect(player.X + 32, player.Y + 64 + cts, block.X, block.Y, block.X + 64, block.Y + 64);

					if (collideLeft && player.X <= block.X + 64) { player.X = block.X + 64; player.VectorX = 0; }
					if (collideRight && player.X + 64 >= block.X) { player.X = block.X - 64; player.VectorX = 0; }
					if (collideBottom && player.Y + 64 >= block.Y) { player.Y = block.Y - 64; player.VectorY = 0; }*/
		
					var result = new Vector2D(player.X, player.Y);
					var collided = CollideResponse(
						new Segment2D { Start = new Vector2D(block.X + 64, block.Y), End = new Vector2D(block.X, block.Y) },
						new Segment2D { Start = new Vector2D(player.X + 32, player.Y + 32), End = new Vector2D(player.X + 32 + player.VectorX, player.Y + 32 + 32 + player.VectorY) },
						ref result);

					if (collided) {
						player.VectorX = 0;
						stayOnGround = true;
						
						player.X = result.X - 32.0f;
						player.Y = result.Y - 64.0f - 1.0f;
					} 

				}
				if (!stayOnGround) { player.VectorY += 1; } else { player.VectorY = 0; }
				player.X += player.VectorX;
				player.Y += player.VectorY; 
			}

			
		}


		private bool CollideResponse(Segment2D seg1, Segment2D seg2, ref Vector2D result)
		{
			var x1 = seg1.Start.X;
			var x2 = seg1.End.X;
			var x3 = seg2.Start.X;
			var x4 = seg2.End.X;

			var y1 = seg1.Start.Y;
			var y2 = seg1.End.Y;
			var y3 = seg2.Start.Y;
			var y4 = seg2.End.Y;
			
			var d = (x1-x2)*(y3-y4) - (y1-y2)*(x3-x4);
			if (Math.Abs(d) < 0.1) return false;
		
			var xi = ((x3-x4)*(x1*y2-y1*x2)-(x1-x2)*(x3*y4-y3*x4))/d;
			var yi = ((y3-y4)*(x1*y2-y1*x2)-(y1-y2)*(x3*y4-y3*x4))/d;
		
			
			if (xi < Math.Min(x1,x2) || xi > Math.Max(x1,x2)) return false;
			if (xi < Math.Min(x3,x4) || xi > Math.Max(x3,x4)) return false;
			if (yi < Math.Min(y1,y2) || yi > Math.Max(y1,y2)) return false;
			if (yi < Math.Min(y3,y4) || yi > Math.Max(y3,y4)) return false;

			var ny = x2 - x1;
			var nx = -(y2 - y1);

			result.X = xi + Math.Sign(nx);
			result.Y = yi + Math.Sign(ny);
			return true;

			/*isl = ((x2 - x1)*(y3 - y1) - (y2 - y1)*(x3 - x1)) > 0;

			If isl Then
				gx4 = xi + sign(nx);gx4 + nx
				gy4 = yi + sign(ny);gy4 - ny
				Rect xi,yi,10,10
				Line xi,yi,xi+nx,yi+ny
		;	Else 
		;		gx4 = xi - sign(nx);gx4 + nx
		;		gy4 = yi - sign(ny);gy4 - ny
		;		Rect xi,yi,10,10
		;		Line xi,yi,xi-nx,yi-ny

			EndIf
			*/

			/*var d = (seg1.Start.X - seg1.End.X) * (seg2.Start.Y - seg2.End.Y) - (seg1.Start.Y - seg1.End.Y) * (seg2.Start.X - seg2.End.X);
			if (d == 0) return false;

			var xi = ((seg2.Start.X - seg2.End.X) * (seg1.Start.X * seg1.End.Y - seg1.Start.Y * seg1.End.X) - (seg1.Start.X - seg1.End.X) * (seg2.Start.X * seg2.End.Y - seg2.Start.Y * seg2.End.X)) / d;
			var yi = ((seg2.Start.Y - seg2.End.Y) * (seg1.Start.X * seg1.End.Y - seg1.Start.Y * seg1.End.X) - (seg1.Start.Y - seg1.End.Y) * (seg2.Start.X * seg2.End.Y - seg2.Start.Y * seg2.End.X)) / d;

			if (xi < Math.Min(seg1.Start.X, seg1.End.X) || xi > Math.Max(seg1.Start.X, seg1.End.X)) return false;
			if (xi < Math.Min(seg2.Start.X, seg2.End.X) || xi > Math.Max(seg2.Start.X, seg2.End.X)) return false;
			if (yi < Math.Min(seg1.Start.Y, seg1.End.Y) || yi > Math.Max(seg1.Start.Y, seg1.End.Y)) return false;
			if (yi < Math.Min(seg2.Start.Y, seg2.End.Y) || yi > Math.Max(seg2.Start.Y, seg2.End.Y)) return false;

			var ny = seg1.End.X - seg1.Start.X;
			var nx = -(seg1.End.Y - seg1.Start.Y);

			result.X = xi + Math.Sign(nx);
			result.Y = yi + Math.Sign(ny);
			return true;*/
		}

		public bool PointInsideRect(double pointX, double pointY, double rectLeft, double rectTop, double rectRight, double rectBottom) {
			return pointX >= rectLeft && pointX <= rectRight && pointY >= rectTop && pointY <= rectBottom;
		}
	}
}
