using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Uberball.Game.Client.Areas.MatchArea {
	public class StateHelper {
		public static void SetState(UIElement element, string value) {
			element.SetValue(StateProperty, value);
			VisualStateManager.GoToState((Control)element, value, true);
			//ExtendedVisualStateManager
		}
		public static string GetState(UIElement element) {
			return (string)element.GetValue(StateProperty);
		}


		public static readonly DependencyProperty StateProperty = DependencyProperty.RegisterAttached(
			"State",
			typeof(String),
			typeof(StateHelper),
			new PropertyMetadata(null, StateChanged));

		internal static void StateChanged(DependencyObject target, DependencyPropertyChangedEventArgs args) {
			// todo^ args.NewValue  может быть не строкой
			// DependencyObject может быть не Control
			if (args.NewValue != null)
				VisualStateManager.GoToState((Control)target, (string)args.NewValue, true);
		}
	}
}
