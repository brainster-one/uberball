
namespace Uberball.Game.Services.MatchService {
	using System;
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

			var tmr = new Timer { AutoReset = true, Interval = 100 };
			tmr.Elapsed += new ElapsedEventHandler(tmr_Elapsed);
			tmr.Start();
		}

		void tmr_Elapsed(object sender, ElapsedEventArgs e) {
			_realm.Update(e.SignalTime.Millisecond);
		}

		void _service_PacketReceived(object sender, RealmServiceEventArgs e) {
			/* todo: сохранять пакеты для обработки. Обрабатывать перед обновлением игрового мира. */
			var player = e.User["player"] as Player;

			var packet = e.Packet as InputPacket;
			player.VectorX = packet.IsRightPressed ? 20 : packet.IsLeftPressed ? -20 : 0;
			player.VectorY = packet.IsDownPressed ? 20 : packet.IsUpPressed ? -20 : 0;
		}


		void _service_UserConnected(object sender, RealmServiceEventArgs e) {
			e.User["player"] = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			_realm.AddEntity(e.User["player"]);
		}

		void _service_UserDisconnected(object sender, RealmServiceEventArgs e) {
			_realm.RemoveEntity(e.User["player"]);
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		Realm _realm = new Realm();
		RealmService _service = new RealmService(new UberballProtocol());
	}
}
