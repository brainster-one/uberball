

namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using Views.Entities;
	using Logic.Entities;
	using ViewModels.Entities;

	public class BallMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Logic.Entities.Ball)model;
			var rViewModel = (BallViewModel)viewModel ?? new BallViewModel();

			rViewModel.NewX = rEntity.X;
			rViewModel.NewY = rEntity.Y;

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			view = new Views.Entities.Ball { DataContext = viewModel };
		}
	}
}
