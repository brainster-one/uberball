
namespace Uberball.Game.Client {
	using System;
	using System.Windows;
	using Areas.MatchArea.Views.Pages;

	public partial class App {

		public App() {
			Startup += OnApplicationStartup;
			Exit += OnApplicationExit;
			UnhandledException += OnApplicationUnhandledException;

			InitializeComponent();
		}

		private void OnApplicationStartup(object sender, StartupEventArgs e) {
			RootVisual = new MatchPage();
		}

		private void OnApplicationExit(object sender, EventArgs e) {

		}

		private void OnApplicationUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e) {
			// If the app is running outside of the debugger then report the exception using
			// the browser's exception mechanism. On IE this will display it a yellow alert 
			// icon in the status bar and Firefox will display a script error.
			if (!System.Diagnostics.Debugger.IsAttached) {

				// NOTE: This will allow the application to continue running after an exception has been thrown
				// but not handled. 
				// For production applications this error handling should be replaced with something that will 
				// report the error to the website and stop the application.
				e.Handled = true;
				Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDom(e));
			}
		}

		private void ReportErrorToDom(ApplicationUnhandledExceptionEventArgs e) {
			try {
				string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

				System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
			} catch (Exception) {
			}
		}
	}
}
