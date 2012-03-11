
namespace Uberball.Game.Services.MatchService {
	using Khrussk.NetworkRealm;
	using System.Net;
	using System;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

	public class MatchService {
		public MatchService() {
			_service = new RealmService();
			_service.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			_service.UserConnected += new System.EventHandler<RealmEventArgs>(_service_UserConnected);
		}

		void _service_UserConnected(object sender, RealmEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			_service.AddEntity(new Player { Name = "Player_" + DateTime.Now.Millisecond });
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		RealmService _service;
	}
}
