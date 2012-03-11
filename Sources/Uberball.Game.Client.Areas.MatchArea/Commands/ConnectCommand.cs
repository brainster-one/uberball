
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System;
	using System.Net;
	using Khrussk.NetworkRealm;
	using Thersuli;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;
	using System.Windows;

	// TODO _viewModel.ConnectionState replace to event - ConnectionState changed
	public class ConnectCommand : Command {
		public ConnectCommand(MatchViewModel viewModel, RealmClient client) {
			_viewModel = viewModel;
			_client = client;
			_client.Connected += new EventHandler<RealmEventArgs>(_client_Connected);
		}

		void _client_Connected(object sender, RealmEventArgs e) {
			Deployment.Current.Dispatcher.BeginInvoke(() => _viewModel.ConnectionState = "connected");
		}

		public override void Execute(object parameter) {
			try {
				_viewModel.ConnectionState = "Connecting";
				_client.Connect(new IPEndPoint(IPAddress.Loopback, 4530));
			} catch (Exception ex) {
				_viewModel.ConnectionState = ex.Message;
			}
		}

		private MatchViewModel _viewModel;
		private RealmClient _client;
	}
}
