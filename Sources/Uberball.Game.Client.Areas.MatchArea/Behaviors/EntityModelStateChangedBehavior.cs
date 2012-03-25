
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using Khrussk.NetworkRealm;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers;
	using Uberball.Game.Logic.Entities;

	public class EntityModelStateChangedBehavior : IBehavior {
		public EntityModelStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
			_mappers.Add(typeof(Player), new PlayerMapper());
			_mappers.Add(typeof(Block), new BlockMapper());
			_mappers.Add(typeof(Ball), new BallMapper());
		}

		public void Handle(object model, EntityNetworkAction action) {
			object viewModel = null;
			var mapper = _mappers[model.GetType()];
			_entities.TryGetValue(model, out viewModel);

			if (action == EntityNetworkAction.Added) {
				mapper.Map(model, ref viewModel);
				_entities.Add(model, viewModel);
				Deployment.Current.Dispatcher.BeginInvoke(() => _viewModel.Entities.Add(viewModel));

			} else if (action == EntityNetworkAction.Modified) {
				Deployment.Current.Dispatcher.BeginInvoke(() => mapper.Map(model, ref viewModel));

			} else if (action == EntityNetworkAction.Removed) {
				_entities.Remove(model);
				Deployment.Current.Dispatcher.BeginInvoke(() => _viewModel.Entities.Remove(viewModel));
			}
		}

		private MatchViewModel _viewModel;
		private Dictionary<object /*model*/, object> _entities = new Dictionary<object,object>();
		private Dictionary<Type, IMapper> _mappers = new Dictionary<Type, IMapper>();
	}
}
