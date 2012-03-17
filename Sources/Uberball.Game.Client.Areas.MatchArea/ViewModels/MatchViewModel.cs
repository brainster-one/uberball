
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;
	using Uberball.Game.Client.Core.Managers;
	using System.Windows;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			IsBusy = true;
			KeyPressCommand = new KeyPressCommand(_matchDataProvider);
			ConnectCommand = new ConnectCommand(_matchDataProvider, endpoint);
			ConnectCommand.Success += (x, y) => { IsBusy = false; };
			ConnectCommand.Failure += (x, y) => { IsBusy = false; ErrorManager.Error("Unable connect to " + endpoint.Address.ToString()); };

			_matchDataProvider.Disconnected += (x, y) => ErrorManager.Error("Connection lost");
		}

		/// <summary>Gets is busy flag.</summary>
		public bool IsBusy { 
			get { return _isBusy; } 
			private set { _isBusy = value; OnPropertyChanged("IsBusy"); } 
		}

		/// <summary>Gets list of entity view models.</summary>
		public ObservableCollection<object> Entities { get { return _matchDataProvider.Entities; } }

		/// <summary>Connect to remote service command.</summary>
		public ConnectCommand ConnectCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public KeyPressCommand KeyPressCommand { get; private set; }

		/// <summary>Busy flag.</summary>
		private bool _isBusy;
		
		/// <summary>Match data provider.</summary>
		private MatchDataProvider _matchDataProvider = new MatchDataProvider();
	}
}
