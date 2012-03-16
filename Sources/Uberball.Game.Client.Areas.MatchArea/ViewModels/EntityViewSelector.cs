
namespace Uberball.Game.Client.Areas.MatchArea.ViewModels {
	using System;
	using System.Windows;
	using Thersuli.ContentControls;

	public class EntityViewSelector : DataTemplateSelector {
		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			var className = item.GetType().Name;
			var templateName = className.Replace("ViewModel", "") + "Template";
			var templateExists = Application.Current.Resources.Contains(templateName);
			if (!templateExists) throw new InvalidOperationException(string.Format("Template is '{0}' not registered for '{1}' entity.", templateName, className));

			return Application.Current.Resources[templateName] as DataTemplate;
		}
	}
}
