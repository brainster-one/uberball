
namespace Uberball.Game.Logic.Entities {
	using System.Collections.Generic;

	/// <summary>Ground. </summary>
	public class Ground {
		/// <summary>Initializes a new instance of the Ground class using specified points and type.</summary>
		/// <param name="points">Points.</param>
		/// <param name="type">Type.</param>
		public Ground(IEnumerable<Point> points, GroundType type) {
			Points = new List<Point>();
			Points.AddRange(points);
			Type = type;
		}

		/// <summary>Initializes a new instance of the Ground class using specified points.</summary>
		/// <param name="points"></param>
		public Ground(IEnumerable<Point> points)
			: this(points, GroundType.Polygon) {
		}

		/// <summary>Initializes a new instance of the Ground class.</summary>
		public Ground() {
			Points = new List<Point>();
			Type = GroundType.Polygon;
		}

		/// <summary>Initializes a new instance of the Ground class using specified position.</summary>
		public static Ground CreateBlock(int x, int y) {
			return new Ground(
				new[] { new Point(x, y), new Point(x + 64, y), new Point(x + 64, y + 64), new Point(x, y + 64) },
				GroundType.Block);
		}

		/// <summary>Gets or sets ground's type.</summary>
		public GroundType Type { get; set; }

		/// <summary>Gets or sets list of points</summary>
		public List<Point> Points { get; private set; }
	}

	/// <summary>Ground type.</summary>
	public enum GroundType { Block, Polygon }

	/// <summary>Point.</summary>
	public struct Point {
		/// <summary>Initializes a new instance of the Point class using specified position.</summary>
		public Point(float x, float y) { X = x; Y = y; }
		
		/// <summary>Gets or sets X-coordinate.</summary>
		public float X;

		/// <summary>Gets or sets Y-coordinate.</summary>
		public float Y;
	}
}
