
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;
	using Uberball.Game.Logic.Entities;

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
