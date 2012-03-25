
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows;
	using System.Windows.Input;
	using Thersuli;
	using Thersuli.MarkupExtensions;
	using Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider;

	/// <summary>Key down command.</summary>
	public class MouseRightButtonDownCommand : Command {
		public MouseRightButtonDownCommand(MatchDataProvider matchDataProvider) {
			_matchDataProvider = matchDataProvider;
		}

		public override void Execute(object parameter) {
			var prm = parameter as InvokeCommandEventArgs;
			var mouse = prm.EventArgs as MouseButtonEventArgs;
			var point = mouse.GetPosition(prm.Sender as UIElement);
			mouse.Handled = true;

			_matchDataProvider.KickBall(point.X, point.Y);
		}

		private MatchDataProvider _matchDataProvider;
	}
}
