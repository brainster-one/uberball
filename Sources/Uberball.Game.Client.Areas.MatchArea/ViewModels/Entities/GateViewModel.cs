
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;

	public class GateViewModel : ViewModel {
		public double X {
			get { return _x; }
			set { _x = value; OnPropertyChanged("X"); }
		}

		public double Y {
			get { return _y; }
			set { _y = value; OnPropertyChanged("Y"); }
		}

		public int Score {
			get { return _score; }
			set { _score = value; OnPropertyChanged("Score"); }
		}

		private double _x;
		private double _y;
		private int _score;
	}
}
