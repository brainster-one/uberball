
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Entities;
	using Logic.Entities;

	public class PlayerMapper : IMapper {

		public void Map(object entity, ref object viewModel) {
			var rEntity = (Player)entity;
			var rViewModel = (PlayerViewModel)viewModel ?? new PlayerViewModel();

			rViewModel.Name = rEntity.Name; 
			rViewModel.NewX = rEntity.X;
			rViewModel.NewY = rEntity.Y;

			viewModel = rViewModel;
		}
	}
}
