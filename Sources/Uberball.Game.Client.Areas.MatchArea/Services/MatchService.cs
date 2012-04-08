
namespace Uberball.Game.Client.Areas.MatchArea.Services {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net;
	using Khrussk;
	using Khrussk.NetworkRealm;
	using Logic.Entities;
	using NetworkProtocol;

	/// <summary>Match data provider.</summary>
	public sealed class MatchService {
		/// <summary>Initializes a new instance of the MatchService class.</summary>
		public MatchService() {
			_client.ConnectionStateChanged += OnConnectionStateChanged;
			_client.EntityStateChanged += OnEntityStateChanged;
		}

		/// <summary>Connects to remote host.</summary>
		/// <param name="endpoint">Endpoint to connect to.</param>
		public void Connect(IPEndPoint endpoint) {
			_client.Connect(endpoint);
		}

		/// <summary>Input.</summary>
		/// <param name="u">Is Up key pressed?</param>
		/// <param name="r">Is Right key pressed?</param>
		/// <param name="d">Is Down key pressed?</param>
		/// <param name="l">Is Left key pressed?</param>
		public void Input(bool u, bool r, bool d, bool l) {
			_client.Send(new InputPacket {
				IsUpPressed = u, IsRightPressed = r, IsDownPressed = d, IsLeftPressed = l
			});
		}

		public void KickBall(double x, double y) {
			var plr = _entities.OfType<Player>().First();
			var angle = Math.Atan2(plr.X - x, plr.Y - y);
			_client.Send(new KickBallPacket { Angle = angle });
		}

		/// <summary>Connection state changed.</summary>
		public event EventHandler<MatchDataProviderEventArgs> ConnectionStateChanged;

		/// <summary>Entity state changed.</summary>
		public event EventHandler<MatchDataProviderEventArgs> EntityStateChanged;

		/// <summary>Connection state changed.</summary>
		/// <param name="sender">Event sener.</param>
		/// <param name="e">Event args.</param>
		void OnConnectionStateChanged(object sender, ConnectionEventArgs e) {
			var evnt = ConnectionStateChanged;
			if (evnt != null) evnt(this, new MatchDataProviderEventArgs(e.State));
		}

		/// <summary>On entity state changed.</summary>
		/// <param name="sender">Entity sanded.</param>
		/// <param name="e">Event args.</param>
		void OnEntityStateChanged(object sender, EntityEventArgs e) {
			if (e.State == EntityState.Added)
				_entities.Add(e.Entity);
			else if (e.State == EntityState.Removed)
				_entities.Remove(e.Entity);

			var evnt = EntityStateChanged;
			if (evnt != null) evnt(this, new MatchDataProviderEventArgs(e.Entity, e.State));
		}

		/// <summary>Client interface to connect to remote service.</summary>
		readonly RealmClient _client = new RealmClient(new UberballProtocol());

		/// <summary>Id to entity map.</summary>
		readonly List<object> _entities = new List<object>();
	}

	/// <summary>Match data provider event args.</summary>
	public class MatchDataProviderEventArgs : EventArgs {
		/// <summary>Initializes a new instance of the MatchViewModel class using specified connection state.</summary>
		/// <param name="connectionState">Connection state</param>
		public MatchDataProviderEventArgs(ConnectionState connectionState) {
			ConnectionState = connectionState;
		}

		/// <summary>Initializes a new instance of the MatchViewModel class using specified entity and action.</summary>
		/// <param name="entity">Entity.</param>
		/// <param name="state">State.</param>
		public MatchDataProviderEventArgs(object entity, EntityState state) {
			Entity = entity;
			EntityState = state;
		}

		/// <summary>Gets connection state.</summary>
		public ConnectionState ConnectionState { get; private set; }

		/// <summary>Gets entity action.</summary>
		public EntityState EntityState { get; private set; }

		/// <summary>Gets entity.</summary>
		public object Entity { get; private set; }
	}
}
