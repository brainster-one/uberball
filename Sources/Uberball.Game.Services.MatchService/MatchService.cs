
namespace Uberball.Game.Services.MatchService {
	using System;
	using System.Linq;
	using System.Net;
	using System.Threading;
	using Ardelme.Core;
	using Khrussk;
	using Khrussk.NetworkRealm;
	using Logic.Entities;
	using NetworkProtocol;
	using RealmBehaviors;

	public class MatchService {
		public MatchService() {
			_service.UserConnectionStateChanged += OnUserConnectionStateChanged;
			_service.PacketReceived += _OnPacketReceived;

			_realm = new Realm(new IRealmBehavior[] {
				//new MovePlayersRealmBehavior(), 
				//new PlayerCollideWithBlockRealmBehavior(),
				//new MoveBallRealmBehavior(),
				//new BallCollideWithBlockRealmBehavior(),
				new Physics(),
				_sync
			});


			//_realm.AddEntity(new Decoration { X = 64 * 3, Y = 64 * 3 });
			//_realm.AddEntity(new Decoration { X = 64 * 6, Y = 64 * 3 });

			_realm.AddEntity(Ground.CreateBlock(64 * 0, 64 * 10));
			_realm.AddEntity(Ground.CreateBlock(64 * 1, 64 * 1));
			_realm.AddEntity(Ground.CreateBlock(64 * 1, 64 * 5));
			_realm.AddEntity(Ground.CreateBlock(64 * 2, 64 * 5));
			_realm.AddEntity(Ground.CreateBlock(64 * 3, 64 * 5));
			_realm.AddEntity(Ground.CreateBlock(64 * 3, 64 * 6));
			_realm.AddEntity(Ground.CreateBlock(64 * 4, 64 * 6));
			_realm.AddEntity(Ground.CreateBlock(64 * 5, 64 * 6));
			_realm.AddEntity(Ground.CreateBlock(64 * 6, 64 * 8));
			_realm.AddEntity(Ground.CreateBlock(64 * 7, 64 * 8));
			_realm.AddEntity(Ground.CreateBlock(64 * 8, 64 * 8));
			_realm.AddEntity(Ground.CreateBlock(64 * 6, 64 * 5));

			//_realm.AddEntity(new Ball { X = 64 * 1, Y = 64 * 3 });


			for (int i = 0; i < 20; ++i) {
				_realm.AddEntity(Ground.CreateBlock(64 * i, 64 * 9 ));
				_realm.AddEntity(Ground.CreateBlock(64 * 12, 64 * i ));
			}

			//_realm.AddEntity(new Block { X = 64 * 1, Y = 64 * 2 });*/
			/*_realm.AddEntity(new Ground(new[] {
				new Point(10, 100), new Point(290, 250), new Point(420, 450),  new Point(580, 350), new Point(620, 750), new Point(30, 790)
			}));*/
		}

		void _OnPacketReceived(object sender, PacketEventArgs e) {
			/* todo: сохранять пакеты для обработки. Обрабатывать перед обновлением игрового мира. */
			var player = e.User["player"] as Player;

			if (e.Packet is InputPacket) {
				var packet = e.Packet as InputPacket;
				player.VectorX = packet.IsRightPressed ? 20 : packet.IsLeftPressed ? -20 : 0;
				player.VectorY = packet.IsDownPressed ? 2 : packet.IsUpPressed ? -2 : 0;
				//_realm.Input(null, packet.IsUpPressed ? new[] {1} : null);
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


		void OnUserConnectionStateChanged(object sender, ConnectionEventArgs e) {
			e.User["player"] = new Player { Name = "Player_" + DateTime.Now.Millisecond, X = 64 * 3 };
			lock (_realm) {
				if (e.State == ConnectionState.Connected)
					_realm.AddEntity(e.User["player"]);
				else if (e.State == ConnectionState.Disconnected)
					_realm.RemoveEntity(e.User["player"]);
			}
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
			var nextUpdate = DateTime.Now;
			new Thread(() => {
				while (_working) {
					lock (_realm) {
						_realm.Update(0);


						// Update
						if (DateTime.Now > nextUpdate) {
							foreach (var ent in _sync.EntityStates) {
								if (ent.Value == EntityState.Added) _service.AddEntity(ent.Key);
								if (ent.Value == EntityState.Modified) _service.ModifyEntity(ent.Key);
								if (ent.Value == EntityState.Removed) _service.RemoveEntity(ent.Key);
							}
							_sync.Clear();
							nextUpdate = DateTime.Now.AddMilliseconds(100);
						}


					}

					Thread.Sleep(1);
				}
			}).Start();
		}

		public void Stop() {
			_working = false;
			_service.Stop();
		}

		readonly SyncEntitiesRealmBehavior _sync = new SyncEntitiesRealmBehavior();
		readonly Realm _realm;
		readonly RealmService _service = new RealmService(new UberballProtocol());
		bool _working = true;
	}
}
