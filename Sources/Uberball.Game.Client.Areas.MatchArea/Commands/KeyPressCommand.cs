
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using Thersuli;
	using Thersuli.MarkupExtensions;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders;
	using System.Windows;
	using System.Diagnostics;
	using System.Windows.Input;

	/// <summary>Key down command.</summary>
	public class KeyPressCommand : Command {
		public KeyPressCommand(MatchDataProvider matchDataProvider) {
			_matchDataProvider = matchDataProvider;
		}

		public override void Execute(object parameter) {
			var evnt = parameter as InvokeCommandEventArgs;
			var key = (evnt.EventArgs as KeyEventArgs).Key;
			/*if ((string)evnt.Parameter == "False")
				MessageBox.Show("ddfd");*/
			//_matchDataProvider.Input();
			//MessageBox.Show(evnt.Parameter.ToString());
			_matchDataProvider.Input(key == Key.Up, key == Key.Right, key == Key.Down, key == Key.Left);
		}

		
		private MatchDataProvider _matchDataProvider;
	}
}
