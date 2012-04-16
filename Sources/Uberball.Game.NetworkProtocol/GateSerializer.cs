
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	/// <summary>Gate serializer.</summary>
	public class GateSerializer : IEntitySerializer<Gate> {
		/// <summary>Deserializes gate from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Deserialize(BinaryReader reader, ref Gate entity, SerializationInfo info) {
			entity = entity ?? new Gate();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
			entity.Score = reader.ReadInt16();
		}

		/// <summary>Serializes entity into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="entity">Entity.</param>
		/// <param name="info">Serialization info.</param>
		public void Serialize(BinaryWriter writer, Gate entity, SerializationInfo info) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
			writer.Write((Int16)entity.Score);
		}
	}
}
