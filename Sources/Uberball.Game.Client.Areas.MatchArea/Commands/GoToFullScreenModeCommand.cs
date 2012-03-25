
namespace Uberball.Game.Client.Areas.MatchArea.Commands {
	using System.Windows;
	using Thersuli;

	public class GoToFullScreenModeCommand : Command {
		public override void Execute(object parameter) {
			Application.Current.Host.Content.IsFullScreen = true;
		}
	}
}
