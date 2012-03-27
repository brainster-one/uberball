
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;
	
	public class PlayerViewModel : ViewModel {
		public PlayerViewModel() {
			State = "";
		}

		public string Name {
			get { return _name; }
			set { _name = value; OnPropertyChanged("Name"); }
		}

		public double X {
			get { return _x; }
			set { _x = value; OnPropertyChanged("X"); }
		}

		public double Y {
			get { return _y; }
			set { _y = value; OnPropertyChanged("Y"); }
		}

		public double NewX { get; set; }
		public double NewY { get; set; }

		private string _name;
		private double _x;
		private double _y;

		public string State { get { return _state; } set { _state = value; OnPropertyChanged("State"); } }
		private string _state;
	}
}
