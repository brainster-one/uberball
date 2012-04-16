
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	/// <summary>Ground serializer.</summary>
	public class GroundSerializer : IEntitySerializer<Ground> {
		/// <summary>Deserializes ground from stream.</summary>
		/// <param name="reader">Reader.</param>
		/// <param name="entity">Entity.</param>
		public void Deserialize(BinaryReader reader, ref Ground entity) {
			entity = entity ?? new Ground();
			var cnt = reader.ReadByte();
			entity.Type = (GroundType)reader.ReadByte();
			while (cnt > 0) {
				entity.Points.Add(new Point { X = reader.ReadSingle(), Y = reader.ReadSingle() });
				--cnt;
			}
		}

		/// <summary>Serializes entity into stream.</summary>
		/// <param name="writer">Writer.</param>
		/// <param name="entity">Entity.</param>
		public void Serialize(BinaryWriter writer, Ground entity) {
			writer.Write((byte)entity.Points.Count);
			writer.Write((byte)entity.Type);
			foreach (var point in entity.Points) {
				writer.Write(point.X);
				writer.Write(point.Y);
			}
		}
	}
}
