
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels.Mappers {
	using Entities;
	using Logic.Entities;

	public class DecorationMapper : IMapper {

		public void Map(object entity, ref object viewModel) {
			var rEntity = (Decoration)entity;
			var rViewModel = (DecorationViewModel)viewModel ?? new DecorationViewModel();

			rViewModel.X = rEntity.X;
			rViewModel.Y = rEntity.Y;

			viewModel = rViewModel;
		}
	}
}
