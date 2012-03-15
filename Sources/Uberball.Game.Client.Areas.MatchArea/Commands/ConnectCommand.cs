
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System;
	using System.Net;
	using Khrussk.NetworkRealm;
	using Thersuli;

	/// <summary>Connect to match service command.</summary>
	public class ConnectCommand : Command {
		/// <summary>Initializes a new instance of the ConnectCommand class.</summary>
		/// <param name="client">Realm client to connect to remote service.</param>
		/// <param name="endpoint">Endpoint to connect to.</param>
		public ConnectCommand(RealmClient client, IPEndPoint endpoint) {
			_endpoint = endpoint;
			_client = client;
			_client.Connected += OnConnected;
		}

		/// <summary>Executes command.</summary>
		/// <param name="parameter">Parameter.</param>
		public override void Execute(object parameter) {
			_client.Connect(_endpoint);
		}

		/// <summary>When execution completed.</summary>
		public event EventHandler Completed;

		/// <summary>Connection established.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		void OnConnected(object sender, RealmEventArgs e) {
			var evnt = Completed;
			if (evnt != null) evnt(this, new EventArgs());
		}

		/// <summary>Realm client.</summary>
		private RealmClient _client;

		/// <summary>Endpoint to connect to.</summary>
		private IPEndPoint _endpoint;
	}
}
