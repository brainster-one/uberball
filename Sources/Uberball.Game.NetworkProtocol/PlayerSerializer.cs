
namespace Uberball.Game.NetworkProtocol {
	using System;
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Uberball.Game.Logic.Entities;

	public class PlayerSerializer : IEntitySerializer {
		public void Deserialize(BinaryReader reader, ref object entity) {
			var player = (Player)entity ?? new Player();
			player.Name = reader.ReadString();
			player.X = reader.ReadInt16();
			player.Y = reader.ReadInt16();
			entity = player;
		}

		public void Serialize(BinaryWriter writer, object entity) {
			var player = (Player)entity;
			writer.Write(player.Name);
			writer.Write((Int16)player.X);
			writer.Write((Int16)player.Y);
		}
	}
}
