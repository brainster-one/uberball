
namespace Uberball.Game.Logic.Entities {
	using System.Collections.Generic;
	using System.Linq;



	public struct Point {
		public Point(float x, float y) {
			X = x;
			Y = y;
		}
		public float X;
		public float Y;
	}

	public enum GroundType {
		Block,
		Polygon
	}

	public class Ground {
		public Ground(IEnumerable<Point> points, GroundType type) {
			Points = new List<Point>();
			Points.AddRange(points);
			Type = type;
		}

		public Ground(IEnumerable<Point> points)
			: this(points, GroundType.Polygon) {
		}

		public Ground() {
			Points = new List<Point>();
			Type = GroundType.Polygon;
		}
	
		public static Ground CreateBlock(int x, int y) {
			return new Ground(
				new[] { new Point(x, y), new Point(x + 64, y), new Point(x + 64, y + 64), new Point(x, y + 64) },
				GroundType.Block);
		}

		public GroundType Type { get; set; }
		public List<Point> Points { get; private set; }

	}
}
