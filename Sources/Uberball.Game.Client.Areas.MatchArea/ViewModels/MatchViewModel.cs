
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using Khrussk;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider;
	using Uberball.Game.Client.Areas.MatchArea.Locators;
	using Uberball.Game.Client.Core.Managers;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			IsBusy = true;
			KeyPressCommand = new KeyPressCommand(DataProviderLocator.MatchDataProvider);
			ConnectCommand = new ConnectCommand(DataProviderLocator.MatchDataProvider, endpoint);
			DataProviderLocator.MatchDataProvider.ConnectionStateChanged += MatchDataProvider_ConnectionStateChanged;
		}

		void MatchDataProvider_ConnectionStateChanged(object sender, MatchDataProviderEventArgs e) {
			IsBusy = false;
			if (e.ConnectionState == ConnectionState.Disconnected) ErrorManager.Error("Connection lost");
		}

		/// <summary>Gets is busy flag.</summary>
		public bool IsBusy { 
			get { return _isBusy; } 
			private set { _isBusy = value; OnPropertyChanged("IsBusy"); } 
		}

		/// <summary>Gets list of entity view models.</summary>
		public ObservableCollection<object> Entities { get { return DataProviderLocator.MatchDataProvider.Entities; } }

		/// <summary>Connect to remote service command.</summary>
		public ConnectCommand ConnectCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public KeyPressCommand KeyPressCommand { get; private set; }

		/// <summary>Busy flag.</summary>
		private bool _isBusy;
	}
}
