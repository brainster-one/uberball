
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Windows;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using System.Net;

	public class MatchViewModel : ViewModel {
		public MatchViewModel(IPEndPoint endpoint) {
			Client = new RealmClient();
			Client.EntityAdded += Client_EntityAdded;
			Client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			
			Connect = new ConnectCommand(Client, endpoint);
			Connect.Completed += Connect_Completed;
		}

		void Connect_Completed(object sender, System.EventArgs e) {
			ConnectionState = "Connected";
		}

		void Client_EntityAdded(object sender, RealmEventArgs e) {
			Deployment.Current.Dispatcher.BeginInvoke(() => MessageBox.Show(((Player)e.Entity).Name + " has been added."));
		}

		public string ConnectionState {
			get { return _connectionState; }
			set { _connectionState = value; OnPropertyChanged("ConnectionState"); }
		}

		public ConnectCommand Connect { get; private set; }

		public RealmClient Client { get; private set; }

		string _connectionState;
	}
}
