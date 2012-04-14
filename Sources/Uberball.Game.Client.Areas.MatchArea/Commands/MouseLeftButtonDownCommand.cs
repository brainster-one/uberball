
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows;
	using System.Windows.Input;
	using Services;
	using Thersuli;
	using Thersuli.MarkupExtensions;

	/// <summary>Key down command.</summary>
	public class MouseLeftButtonDownCommand : Command {
		public MouseLeftButtonDownCommand(MatchService matchService) {
			_matchService = matchService;
		}

		public override void Execute(object parameter) {
			var prm = parameter as InvokeCommandEventArgs;
			var mouse = prm.EventArgs as MouseButtonEventArgs;
			var point = mouse.GetPosition(prm.Sender as UIElement);
			mouse.Handled = true;

			_matchService.Input(new InputState { AimX = (float)point.X, AimY = (float)point.Y, Fire = true });
		}

		readonly MatchService _matchService;
	}
}
