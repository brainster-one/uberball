

namespace Uberball.Game.Client.Areas.MatchArea.Services {
	using System;
	using System.Collections.Generic;
	using Mappers;

	public class EntityMappingService {
		public EntityMappingService() {
			_mappers.Add(typeof(Logic.Entities.Player), new PlayerMapper());
			_mappers.Add(typeof(ViewModels.Entities.PlayerViewModel), new PlayerMapper());

			_mappers.Add(typeof(Logic.Entities.Ball), new BallMapper());
			_mappers.Add(typeof(ViewModels.Entities.BallViewModel), new BallMapper());

			_mappers.Add(typeof(Logic.Entities.Ground), new GroundMapper());
			_mappers.Add(typeof(ViewModels.Entities.GroundViewModel), new GroundMapper());

			_mappers.Add(typeof(Logic.Entities.Decoration), new DecorationMapper());
			_mappers.Add(typeof(ViewModels.Entities.DecorationViewModel), new DecorationMapper());
		}

		public void ToViewModel(object model, ref object viewModel) {
			_mappers[model.GetType()].ToViewModel(model, ref viewModel);
		}
		public void ToView(object viewModel, ref object view) {
			_mappers[viewModel.GetType()].ToView(viewModel, ref view);
		}

		readonly Dictionary<Type, IEntityMapper> _mappers = new Dictionary<Type, IEntityMapper>();
	}
}
