using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Khrussk.NetworkRealm;

namespace Uberball.Game.Client {
	public partial class MainPage : UserControl {
		public MainPage() {
			InitializeComponent();

			RealmClient client = new RealmClient();
			client.Connected += new EventHandler<RealmEventArgs>(client_Connected);
			client.Connect(new IPEndPoint(IPAddress.Loopback, 4530));

		}

		void client_Connected(object sender, RealmEventArgs e) {
			Dispatcher.BeginInvoke(() => MessageBox.Show("Conencted"));
		}
	}
}
