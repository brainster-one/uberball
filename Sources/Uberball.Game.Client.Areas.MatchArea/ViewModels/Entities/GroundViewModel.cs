
using System.Linq;

namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;
	using System.Windows.Media;
	using Uberball.Game.Logic.Entities;

	public class GroundViewModel : ViewModel {
		public GroundViewModel() {
			Points = new PointCollection();
		}

		public double X { get { return Points.Min(p => p.X); } }
		public double Y { get { return Points.Min(p => p.Y); } }

		public GroundType Type { get; set; }
		public PointCollection Points { get; private set; }
	}
}
