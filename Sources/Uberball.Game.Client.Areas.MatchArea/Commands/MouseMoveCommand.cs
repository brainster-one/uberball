
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows.Input;
	using Services;
	using Thersuli;
	using Thersuli.MarkupExtensions;

	/// <summary>Key down command.</summary>
	public class MouseMoveCommand : Command {
		public MouseMoveCommand(MatchService matchService) {
			_matchService = matchService;
		}

		public override void Execute(object parameter) {
			var evnt = parameter as InvokeCommandEventArgs;
			var position = (evnt.EventArgs as MouseEventArgs).GetPosition(null);

			_matchService.Input(new InputState { AimX = position.X, AimY = position.Y });
		}
		readonly MatchService _matchService;
	}
}
