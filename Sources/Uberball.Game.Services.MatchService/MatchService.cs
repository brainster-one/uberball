
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Linq;
	using System.Net;
	using System.Timers;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;

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
			foreach (Player plr in _realm.Entities.Cast<Player>()) {
				plr.X += new Random(DateTime.Now.Millisecond).Next(50);
				plr.Y += new Random(DateTime.Now.Millisecond).Next(50);
				_service.ModifyEntity(plr);
			}
		}

		void _service_UserConnected(object sender, RealmEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			var plr = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_realm.AddEntity(plr);
			_service.AddEntity(plr);
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		Realm _realm = new Realm();
		RealmService _service;
	}
}
