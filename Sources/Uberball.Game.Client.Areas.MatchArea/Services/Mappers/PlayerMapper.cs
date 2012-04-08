
namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using Logic.Entities;
	using ViewModels.Entities;

	public class PlayerMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Player)model;
			var rViewModel = (PlayerViewModel)viewModel ?? new PlayerViewModel();

			rViewModel.Name = rEntity.Name;
			rViewModel.NewX = rEntity.X;
			rViewModel.NewY = rEntity.Y;

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			view = new Views.Entities.Player { DataContext = viewModel };
		}
	}
}
