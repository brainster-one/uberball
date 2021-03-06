﻿
namespace Uberball.Game.Client.Core.Managers {
	using Views;
	using System.Windows;

	public static class ErrorManager {
		public static void Error(string message) {
			Deployment.Current.Dispatcher.BeginInvoke(() => {
				var errWindow = new ErrorWindow(message);
				errWindow.Show();
			});
		}
	}
}
