
namespace Uberball.Game.Services.MatchService.ConsoleHost {
	using System;
	using System.Net;

	class Program {
		static void Main(string[] args) {
			MatchService service = new MatchService();
			service.Start(new IPEndPoint(IPAddress.Any, 4530));

			Console.WriteLine("Match service has been started. Press any key to quit.");
			Console.ReadKey();
		}
	}
}
