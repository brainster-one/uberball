
namespace Uberball.Game.Services.SilverlightPolicyServiceConsoleHost {
	using System;
	using Khrussk.Extras;

	class Program {
		static void Main(string[] args) {
			PolicyServer srv = new PolicyServer("clientaccesspolicy.xml");
			Console.WriteLine("Silverlight policy service has been started.");
			Console.ReadKey();
		}
	}
}
