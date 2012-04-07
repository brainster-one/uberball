﻿
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Linq;
	using System.Net;
	using System.Threading;
	using Ardelme.Core;
	using Khrussk.NetworkRealm;
	using Logic.Entities;
	using NetworkProtocol;
	using RealmBehaviors;

	public class MatchService {
		public MatchService() {
			_service.UserConnected += OnUserConnected;
			_service.UserDisconnected += OnUserDisconnected;
			_service.PacketReceived += _OnPacketReceived;

			_realm = new Realm(new IRealmBehavior[] {
				new MovePlayersRealmBehavior(), 
				new PlayerCollideWithBlockRealmBehavior(),
				//new MoveBallRealmBehavior(),
				//new BallCollideWithBlockRealmBehavior(),
				new SyncEntitiesRealmBehavior(_service)
			});

			
			_realm.AddEntity(new Decoration { X = 64 * 3, Y = 64 * 3 });
			_realm.AddEntity(new Decoration { X = 64 * 6, Y = 64 * 3 });

			_realm.AddEntity(new Block { X = 64 * 0, Y = 64 * 10 });
			_realm.AddEntity(new Block { X = 64 * 1, Y = 64 * 1 });

			_realm.AddEntity(new Block { X = 64 * 1, Y = 64 * 5 });
			_realm.AddEntity(new Block { X = 64 * 2, Y = 64 * 5 });
			_realm.AddEntity(new Block { X = 64 * 3, Y = 64 * 5 });

			_realm.AddEntity(new Block { X = 64 * 3, Y = 64 * 6 });
			_realm.AddEntity(new Block { X = 64 * 4, Y = 64 * 6 });
			_realm.AddEntity(new Block { X = 64 * 5, Y = 64 * 6 });

			_realm.AddEntity(new Block { X = 64 * 6, Y = 64 * 8 });
			_realm.AddEntity(new Block { X = 64 * 7, Y = 64 * 8 });
			_realm.AddEntity(new Block { X = 64 * 8, Y = 64 * 8 });

			_realm.AddEntity(new Block { X = 64 * 6, Y = 64 * 5 });

			_realm.AddEntity(new Ball { X = 64 * 1, Y = 64 * 3 });

			for (int i = 0; i < 20; ++i) {
				_realm.AddEntity(new Block { X = 64 * i, Y = 64 * 9 });
				_realm.AddEntity(new Block { X = 64 * 12, Y = 64 * i });
			}
			_realm.AddEntity(new Block { X = 0, Y = 64 * 1 });
		}

		void _OnPacketReceived(object sender, RealmServiceEventArgs e) {
			/* todo: сохранять пакеты для обработки. Обрабатывать перед обновлением игрового мира. */
			var player = e.User["player"] as Player;

			if (e.Packet is InputPacket) {
				var packet = e.Packet as InputPacket;
				player.VectorX = packet.IsRightPressed ? 40 : packet.IsLeftPressed ? -40 : 0;
				player.VectorY += packet.IsDownPressed ? 20 : packet.IsUpPressed ? -40 : 0;
			} else {
				var packet = e.Packet as KickBallPacket;
				lock (_realm) {
					foreach (var ball in _realm.Entities.OfType<Ball>()) {
						var distance = Math.Sqrt(Math.Pow(ball.X - player.X, 2) + Math.Pow(ball.Y - player.Y, 2));
						if (distance < 64) {
							ball.VectorX = -Math.Sin(packet.Angle) * 70;
							ball.VectorY = -Math.Cos(packet.Angle) * 70;
						}
					}
				}
			}
		}


		void OnUserConnected(object sender, RealmServiceEventArgs e) {
			e.User["player"] = new Player { Name = "Player_" + DateTime.Now.Millisecond };
			lock (_realm) { _realm.AddEntity(e.User["player"]); };
		}

		void OnUserDisconnected(object sender, RealmServiceEventArgs e) {
			lock (_realm) { _realm.RemoveEntity(e.User["player"]); };
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
			new Thread(() => {
				while (_working) { lock (_realm) { _realm.Update(0); }; Thread.Sleep(100); }
			}).Start();			
		}

		public void Stop() {
			_working = false;
			_service.Stop();
		}

		readonly Realm _realm;
		readonly RealmService _service = new RealmService(new UberballProtocol());
		bool _working = true;
	}
}
