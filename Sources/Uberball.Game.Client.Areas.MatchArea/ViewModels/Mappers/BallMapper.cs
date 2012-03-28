
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Entities;
	using Logic.Entities;

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
