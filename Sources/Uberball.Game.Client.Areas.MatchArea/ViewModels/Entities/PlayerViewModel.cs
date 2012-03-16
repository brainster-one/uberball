
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities {
	using Thersuli;
	
	public class PlayerViewModel : ViewModel {
		public string Name {
			get { return _name; }
			set { _name = value; OnPropertyChanged("Name"); }
		}

		private string _name;
	}
}
