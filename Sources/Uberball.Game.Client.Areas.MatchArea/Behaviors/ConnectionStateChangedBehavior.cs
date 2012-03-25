
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System;
	using Khrussk;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;
	using Uberball.Game.Client.Core.Managers;

	/// <summary>Connection state changed.</summary>
	sealed class ConnectionStateChangedBehavior : IBehavior {
		/// <summary>Initializes a new instance of the ConnectionStateChangedBehavior class.</summary>
		/// <param name="viewModel">View model to control.</param>
		public ConnectionStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		/// <summary>Executes behavior.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		public void Handle(ConnectionState state) {
			if (state == ConnectionState.Connected)
				_viewModel.IsBusy = false;
			else
				ErrorManager.Error(state.ToString());
		}

		/// <summary>View model.</summary>
		private MatchViewModel _viewModel;
	}
}
