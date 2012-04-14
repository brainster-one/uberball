
namespace Uberball.Game.Logic.Entities {
	using System;

	public class Player {
		public Guid ClientSessionId { get; set; }
		public string Name { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		
		public float AimAngle { get; set; }
	}
}
