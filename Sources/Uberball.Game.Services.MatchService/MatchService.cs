
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Net;
	using System.Threading;
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

			_realm = new Realm(new IRealmBehavior[] {
				new MovePlayersRealmBehavior(), 
				new PlayerCollideWithBlockRealmBehavior(),
				new SyncEntitiesRealmBehavior(_service),
			});

			_realm.AddEntity(new Block() { X = 64 * 4, Y = 64 * 5 });
			_realm.AddEntity(new Block() { X = 64 * 5, Y = 64 * 5 });
			_realm.AddEntity(new Block() { X = 64 * 6, Y = 64 * 5 });
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
			lock (_realm) { _realm.AddEntity(e.User["player"]); };
		}

		void _service_UserDisconnected(object sender, RealmServiceEventArgs e) {
			lock (_realm) { _realm.RemoveEntity(e.User["player"]); };
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
			new Thread(() => {
				while (_working) { lock (_realm) { _realm.Update(0); }; System.Threading.Thread.Sleep(100); }
			}).Start();			
		}

		public void Stop() {
			_working = false;
			_service.Stop();
		}

		Realm _realm;
		RealmService _service = new RealmService(new UberballProtocol());
		bool _working = true;
	}
}
