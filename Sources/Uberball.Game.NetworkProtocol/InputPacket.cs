
namespace Uberball.Game.NetworkProtocol {
	using Khrussk.Peers;
	using System.IO;

	public class InputPacket {
		public bool IsLeftPressed { get; set; }
		public bool IsRightPressed { get; set; }
		public bool IsUpPressed { get; set; }
		public bool IsDownPressed { get; set; }
	}

	public class InputPacketSrializer : IPacketSerializer {
		public object Deserialize(BinaryReader reader) {
			return new InputPacket {
				IsUpPressed = reader.ReadBoolean(),
				IsRightPressed = reader.ReadBoolean(),
				IsDownPressed = reader.ReadBoolean(),
				IsLeftPressed = reader.ReadBoolean()
			};
		}

		public void Serialize(BinaryWriter writer, object packet) {
			var p = (InputPacket)packet;
			writer.Write(p.IsUpPressed);
			writer.Write(p.IsRightPressed);
			writer.Write(p.IsDownPressed);
			writer.Write(p.IsLeftPressed);
		}
	}
}
