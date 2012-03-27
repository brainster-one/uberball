
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows.Input;
	using Thersuli;
	using Thersuli.MarkupExtensions;
	using DataProviders;

	/// <summary>Key down command.</summary>
	public class KeyPressCommand : Command {
		public KeyPressCommand(MatchDataProvider matchDataProvider) {
			_matchDataProvider = matchDataProvider;
		}

		public override void Execute(object parameter) {
			var stateChanged = false;
			var evnt = parameter as InvokeCommandEventArgs;
			var state = evnt.Parameter.ToString() == "True";
			var key = (evnt.EventArgs as KeyEventArgs).Key;

			int idx = 0;
			foreach (var chk in new[] { Key.Up, Key.Right, Key.Down, Key.Left }) {
				var prevValue = _state[idx];
				_state[idx] = key == chk ? state : _state[idx];
				if (!stateChanged && prevValue != _state[idx]) stateChanged = true;
				++idx;
			}

			if (stateChanged)
				_matchDataProvider.Input(_state[0], _state[1], _state[2], _state[3]);
		}


		readonly bool[] _state = new bool[4];
		readonly MatchDataProvider _matchDataProvider;
	}
}
