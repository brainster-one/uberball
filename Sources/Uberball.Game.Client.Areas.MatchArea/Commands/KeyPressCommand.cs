
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows.Input;
	using Services;
	using Thersuli;
	using Thersuli.MarkupExtensions;

	/// <summary>Key down command.</summary>
	public class KeyPressCommand : Command {
		public KeyPressCommand(MatchService matchService) {
			_matchService = matchService;
		}

		public override void Execute(object parameter) {
			var evnt = parameter as InvokeCommandEventArgs;
			var state = evnt.Parameter.ToString() == "True";
			var key = (evnt.EventArgs as KeyEventArgs).Key;

			_matchService.Input(new InputState {
				Up = key == Key.Up ? state : (bool?)null,
				Right = key == Key.Right ? state : (bool?)null,
				Down = key == Key.Down ? state : (bool?)null,
				Left = key == Key.Left ? state : (bool?)null,
			});
		}
		readonly MatchService _matchService;
	}
}
