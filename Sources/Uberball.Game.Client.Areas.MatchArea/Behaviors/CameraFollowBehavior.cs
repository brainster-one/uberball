
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using ViewModels;
	using Logic.Entities;

	public class CameraFollowBehavior : IBehavior {

		public CameraFollowBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		public void Handle(Player player) {
			if (player == null) return;
			_x = -player.X + 400;
			_y = -player.Y + 400;
			_viewModel.CameraX += (_x - _viewModel.CameraX) * .05;
			_viewModel.CameraY += (_y - _viewModel.CameraY) * .05;

			//_viewModel.CameraX = -player.X + 400;
			//_viewModel.CameraY = -player.Y + 400;
		}

		private double _x;
		private double _y;
		readonly MatchViewModel _viewModel;
	}
}
