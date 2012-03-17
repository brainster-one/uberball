
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;

	/// <summary>Match designer view model.</summary>
	public class MatchDesignerViewModel {
		/// <summary>Gets IsBusy flag.</summary>
		public bool IsBusy { get { return false; } }

		/// <summary>Gets list of entity view models.</summary>
		public ObservableCollection<object> Entities { 
			get {
				return new ObservableCollection<object> {
					new PlayerViewModel() { Name = "Test", X = 100, Y = 100 }
				};
			} 
		}
	}
}
