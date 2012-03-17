
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Linq;
	using System.Net;
	using System.Timers;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using System.Diagnostics;
using System.Collections.Generic;

	public class MatchService {
		public MatchService() {
			_service = new RealmService();
			_service.Protocol.RegisterPacketType(typeof(InputPacket), new InputPacketSrializer());
			_service.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			_service.UserConnected += new System.EventHandler<RealmEventArgs>(_service_UserConnected);
			_service.PacketReceived += new EventHandler<RealmEventArgs>(_service_PacketReceived);

			var tmr = new Timer(1000) { AutoReset = true };
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}

		void _service_PacketReceived(object sender, RealmEventArgs e) {
			Console.WriteLine("PACKET! : " + e.User);

			var player = _userPlayer[e.User];
			Console.WriteLine(player);

			var packet = e.Packet as InputPacket;
			if (packet.IsUpPressed) player.Y -= 20;
			if (packet.IsDownPressed) player.Y += 20;
			if (packet.IsLeftPressed) player.X -= 20;
			if (packet.IsRightPressed) player.X += 20;
			_service.ModifyEntity(player);
			Console.WriteLine(e.Packet.ToString() + " " + player);
		}

		void tmr_Elapsed(object sender, ElapsedEventArgs e) {
			/*foreach (Player plr in _realm.Entities.Cast<Player>()) {
				plr.X += new Random(DateTime.Now.Millisecond).Next(50);
				plr.Y += new Random(DateTime.Now.Millisecond).Next(50);
				_service.ModifyEntity(plr);
			}*/
		}

		void _service_UserConnected(object sender, RealmEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			var plr = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_userPlayer[e.User] = plr;
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
		Dictionary<Khrussk.NetworkRealm.User, Player> _userPlayer = new Dictionary<Khrussk.NetworkRealm.User, Player>();
	}
}
