

namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using Views.Entities;
	using Logic.Entities;
	using ViewModels.Entities;

	public class GroundMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Logic.Entities.Ground)model;
			var rViewModel = (GroundViewModel)viewModel ?? new GroundViewModel();

			rViewModel.Type = rEntity.Type;

			// TODO: коллекция была именена
			foreach (var point in rEntity.Points.ToArray()) {
				rViewModel.Points.Add(new System.Windows.Point(point.X, point.Y));
			}

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			var rViewModel = (GroundViewModel)viewModel;

			if (rViewModel.Type == GroundType.Polygon)
				view = new Views.Entities.Ground { DataContext = viewModel };
			else
				view = new Views.Entities.Block { DataContext = viewModel };
		}
	}
}
