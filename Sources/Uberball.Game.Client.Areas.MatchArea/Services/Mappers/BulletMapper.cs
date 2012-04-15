
namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	using ViewModels.Entities;

	public class BulletMapper : IEntityMapper {
		public void ToViewModel(object model, ref object viewModel) {
			var rEntity = (Logic.Entities.Bullet)model;
			var rViewModel = (BulletViewModel)viewModel ?? new BulletViewModel();

			if (float.IsNaN(rViewModel.X)) rViewModel.X = rEntity.X;
			if (float.IsNaN(rViewModel.Y)) rViewModel.Y = rEntity.Y;

			rViewModel.NewX = rEntity.X;
			rViewModel.NewY = rEntity.Y;

			viewModel = rViewModel;
		}

		public void ToView(object viewModel, ref object view) {
			view = new Views.Entities.Bullet { DataContext = viewModel };
		}
	}
}
