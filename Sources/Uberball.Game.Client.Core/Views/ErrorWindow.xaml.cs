
namespace Uberball.Game.Client.Core.Views {
	using System.Windows;

	public partial class ErrorWindow {
		public ErrorWindow(string message) {
			InitializeComponent();
			Description.Text = message;
		}

		private void OkButtonClicked(object sender, RoutedEventArgs e) {
			DialogResult = true;
		}
	}
}

