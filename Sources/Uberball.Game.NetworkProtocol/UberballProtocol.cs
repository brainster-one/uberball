﻿
namespace Uberball.Game.NetworkProtocol {
	using Khrussk.NetworkRealm.Protocol;

	/// <summary>Uberball's network protocol.</summary>
	public class UberballProtocol : RealmProtocol {
		/// <summary>Initializes a new instance of the UberballProtocol class.</summary>
		public UberballProtocol() {
			Register(new InputPacketSrializer());
			Register(new KickBallPacketSerializer());
			Register(new PlayerSerializer());
			Register(new BlockSerializer());
			Register(new BallSerializer());
		}
	}
}
