
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Client.Areas.MatchArea.Locators;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			KeyPressCommand = new KeyPressCommand(DataProviderLocator.MatchDataProvider);
			MouseRightButtonDownCommand = new MouseRightButtonDownCommand(DataProviderLocator.MatchDataProvider);
			ConnectCommand = new ConnectCommand(DataProviderLocator.MatchDataProvider, endpoint);
			DataProviderLocator.MatchDataProvider.ConnectionStateChanged += (s, e) => OnPropertyChanged("IsBusy");
		}

		/// <summary>Gets is busy flag.</summary>
		public bool IsBusy {
			get { return !DataProviderLocator.MatchDataProvider.IsConnected; } 
		}

		/// <summary>Gets list of entity view models.</summary>
		public ObservableCollection<object> Entities { get { return DataProviderLocator.MatchDataProvider.Entities; } }

		/// <summary>Connect to remote service command.</summary>
		public ConnectCommand ConnectCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public KeyPressCommand KeyPressCommand { get; private set; }

		public MouseRightButtonDownCommand MouseRightButtonDownCommand { get; private set; }
	}
}
