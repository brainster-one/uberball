
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using Services;
	using System.Windows.Input;
	using Thersuli;
	using Thersuli.MarkupExtensions;

	/// <summary>Key down command.</summary>
	public class KeyPressCommand : Command {
		public KeyPressCommand(MatchService matchService) {
			_matchService = matchService;
		}

		public override void Execute(object parameter) {
			var stateChanged = false;
			var evnt = parameter as InvokeCommandEventArgs;
			var state = evnt.Parameter.ToString() == "True";
			var key = (evnt.EventArgs as KeyEventArgs).Key;

			int idx = 0;
			foreach (var chk in new[] { Key.W, Key.D, Key.S, Key.A }) {
				var prevValue = _state[idx];
				_state[idx] = key == chk ? state : _state[idx];
				if (!stateChanged && prevValue != _state[idx]) stateChanged = true;
				++idx;
			}

			if (stateChanged)
				_matchService.Input(_state[0], _state[1], _state[2], _state[3]);
		}


		readonly bool[] _state = new bool[4];
		readonly MatchService _matchService;
	}
}
