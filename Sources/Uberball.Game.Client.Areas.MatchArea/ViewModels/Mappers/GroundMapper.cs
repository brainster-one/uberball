
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Entities;
	using Logic.Entities;

	public class GroundMapper : IMapper {
		// todo: пришло обновление объкта но объект ещё не добавлен в списк вьюмоделей
		public void Map(object entity, ref object viewModel) {
			var rEntity = (Ground)entity;
			var rViewModel = (GroundViewModel)viewModel ?? new GroundViewModel();

			rViewModel.Type = rEntity.Type;

			// TODO: коллекция была именена
			foreach (var point in rEntity.Points.ToArray()) {
				rViewModel.Points.Add(new System.Windows.Point(point.X, point.Y));
			}

			viewModel = rViewModel;
		}
	}
}
