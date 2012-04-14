
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class BulletSerializer : IEntitySerializer<Bullet> {
		public void Deserialize(BinaryReader reader, ref Bullet entity) {
			entity = entity ?? new Bullet();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Bullet entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
