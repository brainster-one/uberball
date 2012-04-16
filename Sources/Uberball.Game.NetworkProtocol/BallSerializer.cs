
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	/// <summary>Ball serializer.</summary>
	public class BallSerializer : IEntitySerializer<Ball> {
		/// <summary>Deserializes ball from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Deserialize(BinaryReader reader, ref Ball entity, SerializationInfo info) {
			entity = entity ?? new Ball();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		/// <summary>Serializes entity into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Serialize(BinaryWriter writer, Ball entity, SerializationInfo info) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
