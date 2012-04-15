
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class GateSerializer : IEntitySerializer<Gate> {
		public void Deserialize(BinaryReader reader, ref Gate entity) {
			entity = entity ?? new Gate();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
			entity.Score = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Gate entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
			writer.Write((Int16)entity.Score);
		}
	}
}
