

namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using ViewModels.Entities;

	public class GateMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Logic.Entities.Gate)model;
			var rViewModel = (GateViewModel)viewModel ?? new GateViewModel();

			rViewModel.X = rEntity.X;
			rViewModel.Y = rEntity.Y;
			rViewModel.Score = rEntity.Score;

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			view = new Views.Entities.Gate { DataContext = viewModel };
		}
	}
}
