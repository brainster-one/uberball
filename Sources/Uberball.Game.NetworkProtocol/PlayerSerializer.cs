
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	/// <summary>Player serializer.</summary>
	public class PlayerSerializer : IEntitySerializer<Player> {
		/// <summary>Deserializes ground from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Deserialize(BinaryReader reader, ref Player entity, SerializationInfo info) {
			entity = entity ?? new Player();
			if (info.State == EntityState.Added) {
				entity.ClientSessionId = Guid.Parse(reader.ReadString());
				entity.Name = reader.ReadString();
			}
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
			entity.AimAngle = reader.ReadSingle(); // todo OPTIIZE
		}

		/// <summary>Serializes entity into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Serialize(BinaryWriter writer, Player entity, SerializationInfo info) {
			if (info.State == EntityState.Added) {
				writer.Write(entity.ClientSessionId.ToString());
				writer.Write(entity.Name);
			}
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
			writer.Write(entity.AimAngle);
		}
	}
}
