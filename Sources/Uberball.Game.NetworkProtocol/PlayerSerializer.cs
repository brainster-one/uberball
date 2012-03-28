
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class PlayerSerializer : IEntitySerializer<Player> {
		public void Deserialize(BinaryReader reader, ref Player entity) {
			entity = entity ?? new Player();
			entity.Name = reader.ReadString();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Player entity) {
			writer.Write(entity.Name);
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
