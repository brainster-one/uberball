
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider {
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Windows;
	using Ardelme.Core;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers;
	using Uberball.Game.Logic.Entities;

	class EntityManager {
		public EntityManager(Realm realm, ObservableCollection<object> viewModels) {
			_realm = realm;
			_viewModels = viewModels;
			Entities = new ObservableCollection<EntityInfo>();
			Entities.CollectionChanged += EntitiesCollectionChanged;
			_mappers.Add(typeof(Player), new PlayerMapper());
		}

		public void Add(int id, object entity) {
			object viewModel = null;
			var mapper = _mappers[entity.GetType()];
			mapper.Map(entity, ref viewModel);
			Entities.Add(new EntityInfo { Id = id, Entity = entity, ViewModel = viewModel });
		}

		public void Remove(int p) {
			var ent = Entities.First(x => x.Id == p);
			Entities.Remove(ent);
		}

		public void ModifyEntity(int entityId, Khrussk.NetworkRealm.Protocol.EntityDiffData entityDiffData) {
			var ent = Entities.First(x => x.Id == entityId);
			var viewModel = ent.ViewModel;
			var mapper = _mappers[ent.Entity.GetType()];
			entityDiffData.ApplyChanges(ent.Entity);
			
			Deployment.Current.Dispatcher.BeginInvoke(() => mapper.Map(ent.Entity, ref viewModel));
		}

		public ObservableCollection<EntityInfo> Entities { get; private set; }

		void EntitiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (EntityInfo item in e.NewItems) {
					_realm.AddEntity(item.ViewModel);
					Deployment.Current.Dispatcher.BeginInvoke(() => _viewModels.Add(item.ViewModel));
				}
			} else if (e.Action == NotifyCollectionChangedAction.Remove) {
				foreach (EntityInfo item in e.OldItems) {
					_realm.RemoveEntity(item.ViewModel);
					Deployment.Current.Dispatcher.BeginInvoke(() => _viewModels.Remove(item.ViewModel));
				}
			}
		}

		private Realm _realm;
		private ObservableCollection<object> _viewModels = new ObservableCollection<object>();
		private Dictionary<Type, IMapper> _mappers = new Dictionary<Type, IMapper>();
	}
}
