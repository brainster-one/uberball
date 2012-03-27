
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using ViewModels;

	public class UpdateRealmBehavior : IBehavior {
		public UpdateRealmBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		public void Handle() {
			_viewModel.Realm.Update(0);
		}

		readonly MatchViewModel _viewModel;
	}
}
