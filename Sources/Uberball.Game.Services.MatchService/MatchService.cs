
namespace Uberball.Game.Services.MatchService {
	using Khrussk.NetworkRealm;
	using System.Net;
	using System;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using System.Timers;

	public class MatchService {
		public MatchService() {
			_service = new RealmService();
			_service.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			_service.UserConnected += new System.EventHandler<RealmEventArgs>(_service_UserConnected);

			var tmr = new Timer(1000) { AutoReset = true };
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}

		void tmr_Elapsed(object sender, ElapsedEventArgs e) {
			if (_plr == null) return;
			_plr.Name = "Player_" + DateTime.Now.ToString();
			_service.ModifyEntity(_plr);
		}

		void _service_UserConnected(object sender, RealmEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			_plr = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_service.AddEntity(_plr);
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		RealmService _service;
		Player _plr;
	}
}
