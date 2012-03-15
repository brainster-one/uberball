
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Net;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.Commands;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using System.Collections.Generic;

	public sealed class MatchViewModel : ViewModel {
		public MatchViewModel(IPEndPoint endpoint) {
			_client.EntityAdded += Client_EntityAdded;
			_client.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			
			Connect = new ConnectCommand(_client, endpoint);
			Connect.Completed += Connect_Completed;
		}

		public string ConnectionState {
			get { return _connectionState; }
			set { _connectionState = value; OnPropertyChanged("ConnectionState"); }
		}

		public IEnumerable<object> Entities {
			get { return _realm.Entities; /* TODO: return list of entity presentation models */ }
		}

		public ConnectCommand Connect { get; private set; }

		void Connect_Completed(object sender, System.EventArgs e) {
			ConnectionState = "Connected";
		}

		void Client_EntityAdded(object sender, RealmEventArgs e) {
			_realm.AddEntity(e.Entity);
			OnPropertyChanged("Entities");
		}

		private RealmClient _client = new RealmClient();
		private Realm _realm = new Realm();

		string _connectionState;
	}
}
