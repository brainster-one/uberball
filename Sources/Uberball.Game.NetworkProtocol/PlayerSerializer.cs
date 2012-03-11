
namespace Uberball.Game.NetworkProtocol {
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Uberball.Game.Logic.Entities;

	public class PlayerSerializer : IEntitySerializer {
		public void Deserialize(BinaryReader reader, ref object entity) {
			var player = (Player)entity ?? new Player();
			player.Name = reader.ReadString();
			entity = player;
		}

		public void Serialize(BinaryWriter writer, object entity) {
			var player = (Player)entity;
			writer.Write(player.Name);
		}
	}
}
