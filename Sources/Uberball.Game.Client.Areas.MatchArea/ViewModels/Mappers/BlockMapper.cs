﻿
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Entities;
	using Logic.Entities;

	public class BlockMapper : IMapper {

		public void Map(object entity, ref object viewModel) {
			var rEntity = (Block)entity;
			var rViewModel = (BlockViewModel)viewModel ?? new BlockViewModel();

			rViewModel.X = rEntity.X;
			rViewModel.Y = rEntity.Y;

			viewModel = rViewModel;
		}
	}
}
