
namespace Uberball.Game.NetworkProtocol {
	using Khrussk.Peers;
	using System.IO;

	public class KickBallPacket {
		public double Angle { get; set; }
	}

	public class KickBallPacketSerializer : IPacketSerializer<KickBallPacket> {
		public KickBallPacket Deserialize(BinaryReader reader) {
			return new KickBallPacket {
				Angle = reader.ReadDouble()
			};
		}

		public void Serialize(BinaryWriter writer, KickBallPacket packet) {
			writer.Write(packet.Angle);
		}
	}
}
