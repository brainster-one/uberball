
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Collections.Generic;
	using System.Net;
	using System.Timers;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Logic.Entities;
	using Uberball.Game.NetworkProtocol;
	using Uberball.Game.Services.MatchService.RealmBehaviors;

	public class MatchService {
		public MatchService() {
			_service = new RealmService();
			_service.Protocol.RegisterPacketType(typeof(InputPacket), new InputPacketSrializer());
			_service.Protocol.RegisterEntityType(typeof(Player), new PlayerSerializer());
			_service.UserConnected += new System.EventHandler<RealmEventArgs>(_service_UserConnected);
			_service.PacketReceived += new EventHandler<RealmEventArgs>(_service_PacketReceived);

			_realm.AddBehavior(new MovePlayersRealmBehavior());
			_realm.AddBehavior(new SyncEntitiesRealmBehavior(_service));

			var tmr = new Timer(100) { AutoReset = true };
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}

		void tmr_Elapsed(object sender, ElapsedEventArgs e) {
			_realm.Update(e.SignalTime.Millisecond);
		}

		void _service_PacketReceived(object sender, RealmEventArgs e) {
			var player = _userPlayer[e.User];

			var packet = e.Packet as InputPacket;
			player.VectorX = packet.IsRightPressed ? 20 : packet.IsLeftPressed ? -20 : 0;
			player.VectorY = packet.IsDownPressed ? 20 : packet.IsUpPressed ? -20 : 0;
		}


		void _service_UserConnected(object sender, RealmEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			var plr = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_userPlayer[e.User] = plr;
			_realm.AddEntity(plr);
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
