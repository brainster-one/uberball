
namespace Uberball.Game.NetworkProtocol {
	using Khrussk.NetworkRealm.Protocol;
	using Uberball.Game.Logic.Entities;

	/// <summary>Uberball's network protocol.</summary>
	public class UberballProtocol : RealmProtocol {
		/// <summary>Initializes a new instance of the UberballProtocol class.</summary>
		public UberballProtocol() {
			RegisterPacketType(typeof(InputPacket), new InputPacketSrializer());
			RegisterEntityType(typeof(Player), new PlayerSerializer());
		}
	}
}
