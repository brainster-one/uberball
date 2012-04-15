
namespace Uberball.Game.Client.Areas.MatchArea.Views.Controls {
	using System;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using Services;
	using ViewModels.Entities;

	/// <summary>Realm presentation control.</summary>
	public partial class Realm {
		/// <summary>Initializes a new instance of the Realm class.</summary>
		public Realm() {
			InitializeComponent();
		}

		/// <summary>On entity property changed.</summary>
		/// <param name="d">Event sender.</param>
		/// <param name="e">Event args.</param>
		static void EntitiesPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			var me = (Realm)d;

			// Unsubscrives from previous collection events
			if (e.OldValue != null)
				((ObservableCollection<object>)e.OldValue).CollectionChanged -= me.EntitiesCollectionChanged;

			// Checks for errors
			if (!(e.NewValue is ObservableCollection<object>))
				throw new InvalidOperationException("Entities is not ObservableCollection<object>");

			// Subscribes for collection change events
			var collection = (ObservableCollection<object>)e.NewValue;
			collection.CollectionChanged += me.EntitiesCollectionChanged;
		}

		/// <summary>Entities collection changed.</summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		void EntitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.NewItems != null) {
				foreach (var itm in e.NewItems) {
					object view = null;
					ServiceLocator.EntityMappingService.ToView(itm, ref view);
					Root.Children.Add((UserControl)view);
				}
			}

			if (e.OldItems != null) {
				foreach (var itm in e.OldItems.OfType<PlayerViewModel>()) {
					Root.Children.Where(x => ((UserControl)x).DataContext == itm).ToList().ForEach(y => Root.Children.Remove(y));
				}
				foreach (var itm in e.OldItems.OfType<BulletViewModel>()) {
					Root.Children.Where(x => ((UserControl)x).DataContext == itm).ToList().ForEach(y => Root.Children.Remove(y));
				}

			}
		}

		/// <summary>Gets or sets list of entities.</summary>
		public ObservableCollection<object> Entities {
			get { return (ObservableCollection<object>)GetValue(EntitiesProperty); }
			set { SetValue(EntitiesProperty, value); }
		}

		/// <summary>Entities dependency property.</summary>
		public static readonly DependencyProperty EntitiesProperty =
			DependencyProperty.Register("Entities", typeof(ObservableCollection<object>), typeof(Realm), new PropertyMetadata(null, EntitiesPropertyChanged));
	}
}
