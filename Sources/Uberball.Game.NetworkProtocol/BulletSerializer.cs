
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	/// <summary>Bullet serializer.</summary>
	public class BulletSerializer : IEntitySerializer<Bullet> {
		/// <summary>Deserializes bullet from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <param name="entity">Entity.</param>
		public void Deserialize(BinaryReader reader, ref Bullet entity) {
			entity = entity ?? new Bullet();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		/// <summary>Serializes entity into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="entity">Entity.</param>
		public void Serialize(BinaryWriter writer, Bullet entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
