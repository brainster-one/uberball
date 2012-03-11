
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Windows;
	using System.Windows.Input;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

	public class MatchViewModel : ViewModel {
		public MatchViewModel() {
			Client = new RealmClient();
			Client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());

			Connect = new ConnectCommand(this, Client);

			Client.EntityAdded += new System.EventHandler<RealmEventArgs>(Client_EntityAdded);
		}

		void Client_EntityAdded(object sender, RealmEventArgs e) {
			Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(((Player)e.Entity).Name + " has been added."));
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
