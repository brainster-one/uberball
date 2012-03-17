
namespace Uberball.Game.Client.Core.Views {
	using System.Windows;
	using System.Windows.Controls;

	public partial class ErrorWindow : ChildWindow {
		public ErrorWindow(string message) {
			InitializeComponent();
			Description.Text = message;
		}

		private void OKButton_Click(object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}
	}
}

