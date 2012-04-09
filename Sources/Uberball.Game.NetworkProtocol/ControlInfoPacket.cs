
namespace Uberball.Game.NetworkProtocol {
	using System.IO;
	using Khrussk.Peers;

	public class ControlInfoPacket {
		public string Id { get; set; }
	}

	public class ControlInfoPacketSrializer : IPacketSerializer<ControlInfoPacket> {
		public ControlInfoPacket Deserialize(BinaryReader reader) {
			return new ControlInfoPacket {
				Id = reader.ReadString()
			};
		}

		public void Serialize(BinaryWriter writer, ControlInfoPacket packet) {
			writer.Write(packet.Id);
		}
	}
}
