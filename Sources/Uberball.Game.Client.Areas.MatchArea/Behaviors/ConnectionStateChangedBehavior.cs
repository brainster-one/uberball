
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using Core.Managers;
	using Khrussk;
	using ViewModels;

	/// <summary>Connection state changed.</summary>
	sealed class ConnectionStateChangedBehavior : IBehavior {
		/// <summary>Initializes a new instance of the ConnectionStateChangedBehavior class.</summary>
		/// <param name="viewModel">View model to control.</param>
		public ConnectionStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		/// <summary>Executes behavior.</summary>
		/// <param name="state">Connection state.</param>
		public void Handle(ConnectionState state) {
			if (state == ConnectionState.Connected)
				_viewModel.IsBusy = false;
			else
				ErrorManager.Error(state.ToString());
		}

		/// <summary>View model.</summary>
		readonly MatchViewModel _viewModel;
	}
}
