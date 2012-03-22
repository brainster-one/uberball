
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Uberball.Game.Logic.Entities;

	public class BlockSerializer : IEntitySerializer<Block> {
		public void Deserialize(BinaryReader reader, ref Block entity) {
			entity = entity ?? new Block();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Block entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
