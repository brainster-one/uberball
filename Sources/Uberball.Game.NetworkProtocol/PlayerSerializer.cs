
namespace Uberball.Game.NetworkProtocol {
	using System.IO;
	using Khrussk.NetworkRealm.Protocol;
	using Uberball.Game.Logic.Entities;

	class PlayerSerializer : IEntitySerializer {
		public void Deserialize(BinaryReader reader, ref IEntity entity) {
			throw new System.NotImplementedException();
		}

		public void Serialize(BinaryWriter writer, IEntity entity) {
			var player = (Player)entity;
			writer.Write(player.Name);
		}
	}
}
