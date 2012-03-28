
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;
	
	public class DecorationViewModel : ViewModel {
		public double X {
			get { return _x; }
			set { _x = value; OnPropertyChanged("X"); }
		}

		public double Y {
			get { return _y; }
			set { _y = value; OnPropertyChanged("Y"); }
		}

		private double _x;
		private double _y;
	}
}
