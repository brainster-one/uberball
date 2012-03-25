
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;

	public class UpdateRealmBehavior : IBehavior {
		public UpdateRealmBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		public void Handle() {
			_viewModel.Realm.Update(0);
		}

		private MatchViewModel _viewModel;
	}
}
