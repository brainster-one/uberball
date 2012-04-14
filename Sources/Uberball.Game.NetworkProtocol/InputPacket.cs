
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.Peers;

	public class InputPacket {
		public bool IsLeftPressed { get; set; }
		public bool IsRightPressed { get; set; }
		public bool IsUpPressed { get; set; }
		public bool IsDownPressed { get; set; }
		public bool IsKickBallPressed { get; set; }
		public float AimAngle { get; set; }
	}

	public class InputPacketSrializer : IPacketSerializer<InputPacket> {
		public InputPacket Deserialize(BinaryReader reader) {
			return new InputPacket {
				IsUpPressed = reader.ReadBoolean(),
				IsRightPressed = reader.ReadBoolean(),
				IsDownPressed = reader.ReadBoolean(),
				IsLeftPressed = reader.ReadBoolean(),
				IsKickBallPressed = reader.ReadBoolean(),
				AimAngle = reader.ReadSingle() // todo Optimize
			};
		}

		public void Serialize(BinaryWriter writer, InputPacket packet) {
			writer.Write(packet.IsUpPressed);
			writer.Write(packet.IsRightPressed);
			writer.Write(packet.IsDownPressed);
			writer.Write(packet.IsLeftPressed);
			writer.Write(packet.IsKickBallPressed);
			writer.Write(packet.AimAngle);
		}
	}
}
