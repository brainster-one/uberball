
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Logic.Entities;

	public class BallSerializer : IEntitySerializer<Ball> {
		public void Deserialize(BinaryReader reader, ref Ball entity) {
			entity = entity ?? new Ball();
			entity.X = reader.ReadInt16();
			entity.Y = reader.ReadInt16();
		}

		public void Serialize(BinaryWriter writer, Ball entity) {
			writer.Write((Int16)entity.X);
			writer.Write((Int16)entity.Y);
		}
	}
}
