
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class DecorationSerializer : IEntitySerializer<Decoration> {
		public void Deserialize(BinaryReader reader, ref Decoration entity) {
			entity = entity ?? new Decoration();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Decoration entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
