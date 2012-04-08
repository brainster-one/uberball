
namespace Uberball.Game.Client.Areas.MatchArea.Views.Entities {
	using System.Linq;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Media;
	using System.Windows.Shapes;


	public partial class Ground {
		public Ground() {
			InitializeComponent();
			//if (DataContext != null) PropertyChangedCallback(this, new DependencyPropertyChangedEventArgs { DataContext.});
		}

		public static readonly DependencyProperty PointsProperty =
			DependencyProperty.Register("Points", typeof(PointCollection), typeof(Ground), new PropertyMetadata(default(PointCollection), PropertyChangedCallback));

		private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs) {
			var me = (Ground)dependencyObject;
			var collection = new PointCollection();
			me.Points.OrderBy(x => x.Y).Take(4).OrderBy(x => x.X).ToList().ForEach(collection.Add);
			me.Points.OrderBy(x => x.Y).Take(4).OrderByDescending(x => x.X).ToList().ForEach(x => collection.Add(new Point(x.X, x.Y + 15)));

			(me.FindName("canvas") as Canvas).Children.Add(new Polygon {
				Points = collection, Fill = new SolidColorBrush(Colors.Green)
			});
		}

		public PointCollection Points {
			get { return (PointCollection)GetValue(PointsProperty); }
			set { SetValue(PointsProperty, value); }
		}
	}
}
