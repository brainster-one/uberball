
namespace Uberball.Game.NetworkProtocol {
	using System.IO;
	using Khrussk.Peers;

	/// <summary>Input packet.</summary>
	public class InputPacket {
		/// <summary>Gets or sets left button state.</summary>
		public bool IsLeftPressed { get; set; }

		/// <summary>Gets or sets right button state.</summary>
		public bool IsRightPressed { get; set; }

		/// <summary>Gets or sets up button state.</summary>
		public bool IsUpPressed { get; set; }

		/// <summary>Gets or sets down button state.</summary>
		public bool IsDownPressed { get; set; }

		/// <summary>Gets or sets kick button state.</summary>
		public bool IsKickBallPressed { get; set; }

		/// <summary>Gets or sets fire button state.</summary>
		public bool IsFirePressed { get; set; }

		/// <summary>Gets or sets aiming angle.</summary>
		public float AimAngle { get; set; }
	}

	/// <summary>Input packet serializer.</summary>
	public class InputPacketSrializer : IPacketSerializer<InputPacket> {
		/// <summary>Deserializes packet from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <returns>Packet.</returns>
		public InputPacket Deserialize(BinaryReader reader) {
			return new InputPacket {
				IsUpPressed = reader.ReadBoolean(),
				IsRightPressed = reader.ReadBoolean(),
				IsDownPressed = reader.ReadBoolean(),
				IsLeftPressed = reader.ReadBoolean(),
				IsKickBallPressed = reader.ReadBoolean(),
				IsFirePressed = reader.ReadBoolean(),
				AimAngle = reader.ReadSingle() // todo Optimize
			};
		}

		/// <summary>Serializes packet into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="packet">Packet.</param>
		public void Serialize(BinaryWriter writer, InputPacket packet) {
			writer.Write(packet.IsUpPressed);
			writer.Write(packet.IsRightPressed);
			writer.Write(packet.IsDownPressed);
			writer.Write(packet.IsLeftPressed);
			writer.Write(packet.IsKickBallPressed);
			writer.Write(packet.IsFirePressed);
			writer.Write(packet.AimAngle);
		}
	}
}
