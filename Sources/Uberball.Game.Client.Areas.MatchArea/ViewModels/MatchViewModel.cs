
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Windows.Input;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;

	public class MatchViewModel : ViewModel {
		public MatchViewModel() {
			Client = new RealmClient();
			Connect = new ConnectCommand(this, Client);
		}

		public string ConnectionState {
			get { return _connectionState; }
			set { _connectionState = value; OnPropertyChanged("ConnectionState"); }
		}

		public ICommand Connect { get; private set; }

		public RealmClient Client { get; private set; }

		string _connectionState;
	}
}
