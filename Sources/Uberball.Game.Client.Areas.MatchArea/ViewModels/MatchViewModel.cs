
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System.Collections.ObjectModel;
	using System.Net;
	using System.Windows.Input;
	using System.Windows.Media;
	using Ardelme.Core;
	using Behaviors;
	using Commands;
	using Locators;
	using RealmBehaviors;
	using Thersuli;

	/// <summary>Match view model.</summary>
	public sealed class MatchViewModel : ViewModel {
		/// <summary>Initializes a new instance of the MatchViewModel class.</summary>
		/// <param name="endpoint">EndPoint to connect to.</param>
		public MatchViewModel(IPEndPoint endpoint) {
			var dp = DataProviderLocator.MatchDataProvider;

			// commands and behaviors
			Realm = new Realm(new IRealmBehavior[] {
				new UpdatePlayerPositionRealmBehavior(),
				new UpdateBallPositionRealmBehavior()
			});
			Entities = new ObservableCollection<object>();
			KeyPressCommand = new KeyPressCommand(dp);
			MouseRightButtonDownCommand = new MouseRightButtonDownCommand(dp);
			ConnectCommand = new ConnectCommand(dp, endpoint);
			ConnectionStateChangedBehavior = new ConnectionStateChangedBehavior(this);
			EntityModelStateChangedBehavior = new EntityModelStateChangedBehavior(this);
			EntityViewModelStateChangedBehavior = new EntityViewModelStateChangedBehavior(this);
			UpdateRealmBehavior = new UpdateRealmBehavior(this);

			// match data provider events
			dp.ConnectionStateChanged += (s, e) => ConnectionStateChangedBehavior.Handle(e.ConnectionState);
			dp.EntityStateChanged += (s, e) => EntityModelStateChangedBehavior.Handle(e.Entity, e.EntityState);
			Entities.CollectionChanged += (s, e) => { lock (Realm) { EntityViewModelStateChangedBehavior.Handle(e); } };
			CompositionTarget.Rendering += (s, e) => { lock (Realm) { UpdateRealmBehavior.Handle(); } };
		}

		/// <summary>Gets is busy flag.</summary>
		public bool IsBusy {
			get { return _isBusy; }
			set { _isBusy = value; OnPropertyChanged("IsBusy"); } 
		}

		/// <summary></summary>
		public Realm Realm { get; private set; }

		/// <summary>Gets list of view models.</summary>
		public ObservableCollection<object> Entities { get; private set; }

		/// <summary>1Connect to remote service command.</summary>
		public ICommand ConnectCommand { get; private set; }

		/// <summary>Key pressed command.</summary>
		public ICommand KeyPressCommand { get; private set; }

		/// <summary>Mouse right button clicked.</summary>
		public ICommand MouseRightButtonDownCommand { get; private set; }

		/// <summary>Gets connection state changed.</summary>
		private ConnectionStateChangedBehavior ConnectionStateChangedBehavior { get; set; }

		/// <summary>Gets entity state changed.</summary>
		private EntityModelStateChangedBehavior EntityModelStateChangedBehavior { get; set; }

		/// <summary>Gets entity state changed.</summary>
		private EntityViewModelStateChangedBehavior EntityViewModelStateChangedBehavior { get; set; }

		/// <summary>Gets handler.</summary>
		private UpdateRealmBehavior UpdateRealmBehavior { get; set; }

		/// <summary>IsBusy flag.</summary>
		private bool _isBusy;
	}
}
