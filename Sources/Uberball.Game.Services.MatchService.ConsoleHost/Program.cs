
namespace Uberball.Game.Services.MatchService.ConsoleHost {
	using System;
	using System.Net;
	using Khrussk.Extras;

	class Program {
		static void Main(string[] args) {
			PolicyServer srv = new PolicyServer("clientaccesspolicy.xml");
			Console.WriteLine("Silverlight policy service has been started.");

			MatchService service = new MatchService();
			service.Start(new IPEndPoint(IPAddress.Any, 4530));

			Console.WriteLine("Match service has been started. Press any key to quit.");
			Console.ReadKey();
		}
	}
}
