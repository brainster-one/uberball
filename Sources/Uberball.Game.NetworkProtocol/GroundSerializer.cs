
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class GroundSerializer : IEntitySerializer<Ground> {
		public void Deserialize(BinaryReader reader, ref Ground entity) {
			entity = entity ?? new Ground();
			var cnt = reader.ReadByte();
			entity.Type = (GroundType)reader.ReadByte();
			while (cnt > 0) {
				entity.Points.Add(new Point { X = reader.ReadSingle(), Y = reader.ReadSingle() });
				--cnt;
			}
		}

		public void Serialize(BinaryWriter writer, Ground entity) {
			writer.Write((byte)entity.Points.Count);
			writer.Write((byte)entity.Type);
			foreach (var point in entity.Points) {
				writer.Write((Single)point.X);
				writer.Write((Single)point.Y);
			}
		}
	}
}
