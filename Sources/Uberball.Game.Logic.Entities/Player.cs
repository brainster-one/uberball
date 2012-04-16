
namespace Uberball.Game.Logic.Entities {
	using System;

	/// <summary>Player.</summary>
	public class Player {
		/// <summary>Gets or sets session Id.</summary>
		public Guid ClientSessionId { get; set; }

		/// <summary>Gets or sets name.</summary>
		public string Name { get; set; }

		/// <summary>Gets or sets X coordinate.</summary>
		public float X { get; set; }

		/// <summary>Gets or sets Y coordinate.</summary>
		public float Y { get; set; }

		/// <summary>Gets or sets VecotrX.</summary>
		public float VectorX { get; set; }

		/// <summary>Gets or sets VecotrY.</summary>
		public float VectorY { get; set; }

		/// <summary>Gets or sets aiming angle.</summary>
		public float AimAngle { get; set; }
	}
}
