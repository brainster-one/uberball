
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
			_service.UserConnected += _service_UserConnected;
			_service.UserDisconnected += _service_UserDisconnected;
			_service.PacketReceived += _service_PacketReceived;

			_realm.AddBehavior(new MovePlayersRealmBehavior());
			_realm.AddBehavior(new SyncEntitiesRealmBehavior(_service));

			var tmr = new Timer(100) { AutoReset = true };
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}

		void tmr_Elapsed(object sender, ElapsedEventArgs e) {
			_realm.Update(e.SignalTime.Millisecond);
		}

		void _service_PacketReceived(object sender, RealmServiceEventArgs e) {
			/* todo: сохранять пакеты для обработки. Обрабатывать перед обновлением игрового мира. */
			var player = _userPlayer[e.User];

			var packet = e.Packet as InputPacket;
			player.VectorX = packet.IsRightPressed ? 20 : packet.IsLeftPressed ? -20 : 0;
			player.VectorY = packet.IsDownPressed ? 20 : packet.IsUpPressed ? -20 : 0;
		}


		void _service_UserConnected(object sender, RealmServiceEventArgs e) {
			Console.WriteLine("User connected: " + e.User);
			var plr = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_userPlayer[e.User] = plr;
			_realm.AddEntity(plr);
		}

		void _service_UserDisconnected(object sender, RealmServiceEventArgs e) {
			var entity = _userPlayer[e.User];
			_realm.RemoveEntity(entity);
			//_service.RemoveEntity(entity);
			Console.WriteLine("User disconnected: " + e.User);
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		Realm _realm = new Realm();
		RealmService _service = new RealmService(new UberballProtocol());
		Dictionary<Khrussk.NetworkRealm.User, Player> _userPlayer = new Dictionary<Khrussk.NetworkRealm.User, Player>();
	}
}
