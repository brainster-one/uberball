
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			ConnectCommand = new ConnectCommand(_matchDataProvider, endpoint);
		}

		/// <summary>Gets list of entity view models.</summary>
		public ObservableCollection<object> Entities { get { return _matchDataProvider.Entities; } }

		/// <summary>connect to remote service command.</summary>
		public ConnectCommand ConnectCommand { get; private set; }
		
		/// <summary>Match data provider.</summary>
		private MatchDataProvider _matchDataProvider = new MatchDataProvider();
	}
}
