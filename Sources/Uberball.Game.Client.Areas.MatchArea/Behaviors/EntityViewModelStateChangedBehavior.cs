
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System.Collections.Specialized;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;

	public class EntityViewModelStateChangedBehavior : IBehavior {
		public EntityViewModelStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		public void Handle(NotifyCollectionChangedEventArgs e) {
			if (e.NewItems != null)
				foreach (var vm in e.NewItems) { _viewModel.Realm.AddEntity(vm); }
			else
				foreach (var vm in e.OldItems) { _viewModel.Realm.RemoveEntity(vm); }
		}

		private MatchViewModel _viewModel;
	}
}
