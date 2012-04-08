

namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using Views.Entities;
	using Logic.Entities;
	using ViewModels.Entities;

	public class DecorationMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Logic.Entities.Decoration)model;
			var rViewModel = (DecorationViewModel)viewModel ?? new DecorationViewModel();

			rViewModel.X = rEntity.X;
			rViewModel.Y = rEntity.Y;

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			view = new Views.Entities.Decoration { DataContext = viewModel };
		}
	}
}
