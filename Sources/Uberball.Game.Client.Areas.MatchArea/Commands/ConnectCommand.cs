
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System;
	using System.Net;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;

	/// <summary>Connect to match service command.</summary>
	public class ConnectCommand : Command {
		/// <summary>Initializes a new instance of the ConnectCommand class.</summary>
		/// <param name="provider">Realm client to connect to remote service.</param>
		/// <param name="endpoint">Endpoint to connect to.</param>
		public ConnectCommand(MatchDataProvider provider, IPEndPoint endpoint) {
			_endpoint = endpoint;
			_provider = provider;
			_provider.Connected += OnConnected;
		}

		/// <summary>Executes command.</summary>
		/// <param name="parameter">Parameter.</param>
		public override void Execute(object parameter) {
			_provider.Connect(_endpoint);
		}

		/// <summary>When execution completed.</summary>
		public event EventHandler Completed;

		/// <summary>Connection established.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		void OnConnected(object sender, EventArgs e) {
			var evnt = Completed;
			if (evnt != null) evnt(this, new EventArgs());
		}

		/// <summary>Match data provider.</summary>
		private MatchDataProvider _provider;

		/// <summary>Endpoint to connect to.</summary>
		private IPEndPoint _endpoint;
	}
}
