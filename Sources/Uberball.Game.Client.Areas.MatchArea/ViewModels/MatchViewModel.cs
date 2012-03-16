
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		public MatchViewModel(IPEndPoint endpoint) {
			ConnectCommand = new ConnectCommand(_service, endpoint);
		}

		public ObservableCollection<object> Entities { get { return _service.Entities; } }

		public ConnectCommand ConnectCommand { get; private set; }
		
		private MatchDataProvider _service = new MatchDataProvider();
	}
}
