
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;
	using Uberball.Game.Logic.Entities;

	public class BallMapper : IMapper {

		public void Map(object entity, ref object viewModel) {
			var rEntity = (Ball)entity;
			var rViewModel = (BallViewModel)viewModel ?? new BallViewModel();

			rViewModel.NewX = rEntity.X;
			rViewModel.NewY = rEntity.Y;

			viewModel = rViewModel;
		}
	}
}
