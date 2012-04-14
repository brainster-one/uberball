
namespace Uberball.Game.Services.MatchService {
	using System.Collections.Generic;
	using System.Net;
	using System.Threading;
	using Ardelme.Core;
	using Khrussk;
	using Khrussk.NetworkRealm;
	using NetworkProtocol;
	using RealmBehaviors;

	/// <summary>Match service.</summary>
	public class MatchService {
		/// <summary>Initializes a new instance of the MatchService class.</summary>
		public MatchService() {
			_sync = new SyncEntitiesRealmBehavior(_service);

			_service.UserConnectionStateChanged += OnUserConnectionStateChanged;
			_service.PacketReceived += OnPacketReceived;

			_realm = new Realm(new IRealmBehavior[] {
				new LoadMapRealmBehavior(),
				new EnterLeaveRealmBehavior(),
				new PhysicsRealmBehavior(),
				_sync
			});
		}

		/// <summary>User connection state changed.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		void OnUserConnectionStateChanged(object sender, ConnectionEventArgs e) {
			if (e.State == ConnectionState.Connected) {
				var user = new Ardelme.Core.User(e.User.Session);
				e.User["user"] = user;
				lock (_realm) { _realm.Enter(user); }
			} else if (e.State == ConnectionState.Disconnected)
				lock (_realm) { _realm.Leave((Ardelme.Core.User)e.User["user"]); }
		}

		/// <summary>On packet received.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		void OnPacketReceived(object sender, PacketEventArgs e) {
			// todo: сохранять пакеты для обработки. Обрабатывать перед обновлением игрового мира.
			if (e.Packet is InputPacket) {
				var packet = e.Packet as InputPacket;
				var user = (Ardelme.Core.User)e.User["user"];
				_realm.Input(user, new InputState(new Dictionary<string, object> {
					{ "up", packet.IsUpPressed },
					{ "right", packet.IsRightPressed },
					{ "down", packet.IsDownPressed },
					{ "left", packet.IsLeftPressed },
					{ "aimAngle", packet.AimAngle }
				}));
			} /*else {
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
			}*/
		}

		/// <summary>Starts match service.</summary>
		/// <param name="endpoint">End point to listen on.</param>
		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
			_realm.Start();

			new Thread(() => {
				while (_working) {
					lock (_realm) { _realm.Update(0.016d); }
					Thread.Sleep(1);
				}
			}).Start();
		}

		/// <summary>Stops service.</summary>
		public void Stop() {
			_working = false;
			_service.Stop();
		}

		/// <summary>Service.</summary>
		readonly RealmService _service = new RealmService(new UberballProtocol());

		/// <summary>Match realm.</summary>
		readonly Realm _realm;

		/// <summary>Sync entities behavior.</summary>
		readonly SyncEntitiesRealmBehavior _sync;

		/// <summary>Is service working?</summary>
		bool _working = true;
	}
}
