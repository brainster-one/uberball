﻿
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using System.Windows.Input;
	using System.Windows.Media;
	using Ardelme.Core;
	using Behaviors;
	using Commands;
	using RealmBehaviors;
	using Services;
	using Thersuli;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			var matchService = ServiceLocator.MatchService;

			// realm
			Entities = new ObservableCollection<object>();
			Realm = new Realm(new IRealmBehavior[] {
				new UpdatePlayerPositionRealmBehavior(),
				new UpdateBallPositionRealmBehavior(),
				new UpdateBulletPositionRealmBehavior()
			});

			// Commands
			KeyPressCommand = new KeyPressCommand(matchService);
			MouseMoveCommand = new MouseMoveCommand(matchService);
			MouseLeftButtonDownCommand = new MouseLeftButtonDownCommand(matchService);
			MouseRightButtonDownCommand = new MouseRightButtonDownCommand(matchService);
			ConnectCommand = new ConnectCommand(matchService, endpoint);

			// Behaviors
			ConnectionStateChangedBehavior = new ConnectionStateChangedBehavior(this);
			EntityModelStateChangedBehavior = new EntityModelStateChangedBehavior(this);
			EntityViewModelStateChangedBehavior = new EntityViewModelStateChangedBehavior(this);
			CameraFollowBehavior = new CameraFollowBehavior(this);
			UpdateRealmBehavior = new UpdateRealmBehavior(this);

			// MatchService events
			matchService.ConnectionStateChanged += (s, e) => ConnectionStateChangedBehavior.Handle(e.ConnectionState);
			matchService.EntityStateChanged += (s, e) => EntityModelStateChangedBehavior.Handle(e.Entity, e.EntityState);
			Entities.CollectionChanged += (s, e) => { lock (Realm) { EntityViewModelStateChangedBehavior.Handle(e); } };
			CompositionTarget.Rendering += (s, e) => { lock (Realm) { UpdateRealmBehavior.Handle(); } };
			CompositionTarget.Rendering += (s, e) => CameraFollowBehavior.Handle(matchService.GetMyPlayer());
		}

		/// <summary>Gets is busy flag.</summary>
		public bool IsBusy {
			get { return _isBusy; }
			set { _isBusy = value; OnPropertyChanged("IsBusy"); }
		}

		public double CameraX {
			get { return _cameraX; }
			set { _cameraX = value; OnPropertyChanged("CameraX"); }
		}

		public double CameraY {
			get { return _cameraY; }
			set { _cameraY = value; OnPropertyChanged("CameraY"); }
		}

		/// <summary></summary>
		public Realm Realm { get; private set; }

		/// <summary>Gets list of view models.</summary>
		public ObservableCollection<object> Entities { get; private set; }

		/// <summary>1Connect to remote service command.</summary>
		public ICommand ConnectCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public ICommand KeyPressCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public ICommand MouseMoveCommand { get; private set; }

		/// <summary>Mouse left button clicked.</summary>
		public ICommand MouseLeftButtonDownCommand { get; private set; }

		/// <summary>Mouse right button clicked.</summary>
		public ICommand MouseRightButtonDownCommand { get; private set; }

		/// <summary>Gets connection state changed.</summary>
		private ConnectionStateChangedBehavior ConnectionStateChangedBehavior { get; set; }

		/// <summary>Gets entity state changed.</summary>
		EntityModelStateChangedBehavior EntityModelStateChangedBehavior { get; set; }

		/// <summary>Gets entity state changed.</summary>
		EntityViewModelStateChangedBehavior EntityViewModelStateChangedBehavior { get; set; }

		/// <summary>Gets handler.</summary>
		UpdateRealmBehavior UpdateRealmBehavior { get; set; }

		/// <summary>Gets behavior.</summary>
		CameraFollowBehavior CameraFollowBehavior { get; set; }

		/// <summary>IsBusy flag.</summary>
		bool _isBusy;

		double _cameraX;
		double _cameraY;
	}
}
