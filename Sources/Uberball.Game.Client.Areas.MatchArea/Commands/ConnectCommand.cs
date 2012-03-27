
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Net;
	using Thersuli;
	using DataProviders;

	/// <summary>Connect to match service command.</summary>
	public class ConnectCommand : Command {
		/// <summary>Initializes a new instance of the ConnectCommand class.</summary>
		/// <param name="provider">Realm client to connect to remote service.</param>
		/// <param name="endpoint">Endpoint to connect to.</param>
		public ConnectCommand(MatchDataProvider provider, IPEndPoint endpoint) {
			_endpoint = endpoint;
			_provider = provider;
		}

		/// <summary>Executes command.</summary>
		/// <param name="parameter">Parameter.</param>
		public override void Execute(object parameter) {
			_provider.Connect(_endpoint);
		}

		/// <summary>Match data provider.</summary>
		readonly MatchDataProvider _provider;

		/// <summary>Endpoint to connect to.</summary>
		readonly IPEndPoint _endpoint;
	}
}
