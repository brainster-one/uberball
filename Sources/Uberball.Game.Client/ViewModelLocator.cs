
namespace Uberball.Game.Client {
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;

	public class ViewModelLocator {
		private static MatchViewModel _matchViewModel;

		public static MatchViewModel MatchViewModel {
			get { return _matchViewModel ?? (_matchViewModel = new MatchViewModel(Configuration.MatchServiceEndPoint)); }
		}
	}

	public class DesignerViewModelLocator {
		private static MatchDesignerViewModel _matchViewModel;

		public static MatchDesignerViewModel MatchViewModel {
			get { return _matchViewModel ?? (_matchViewModel = new MatchDesignerViewModel()); }
		}
	}
}
