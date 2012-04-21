
using System.Windows;
using System.Windows.Interop;

namespace Uberball.Game.Client.Areas.MatchArea.Views.Pages {
	public partial class MatchPage {
		public MatchPage() {
			InitializeComponent();
		}

		private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (Application.Current.InstallState == InstallState.Installed) {
				Application.Current.CheckAndDownloadUpdateAsync();
			} else
				Application.Current.Install();
		}

		private void Button_Click2(object sender, System.Windows.RoutedEventArgs e) {
			Application.Current.Host.Content.IsFullScreen = true;
			Application.Current.Host.Content.FullScreenOptions = FullScreenOptions.StaysFullScreenWhenUnfocused;
			Application.Current.Host.Settings.EnableCacheVisualization = true;
			Application.Current.Host.Settings.EnableFrameRateCounter = true;
		}
	}
}
