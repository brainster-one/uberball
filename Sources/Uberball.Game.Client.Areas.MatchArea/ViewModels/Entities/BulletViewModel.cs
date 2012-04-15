
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;
	
	public class BulletViewModel : ViewModel {
		public float X {
			get { return _x; }
			set { _x = value; OnPropertyChanged("X"); }
		}

		public float Y {
			get { return _y; }
			set { _y = value; OnPropertyChanged("Y"); }
		}

		/* looks like shit */
		public float NewX { get; set; }
		public float NewY { get; set; }

		private float _x = float.NaN;
		private float _y = float.NaN;
	}
}
