
namespace Uberball.Game.Client {
	using System.Net;

	public static class Configuration {
		static Configuration() {
			MatchServiceEndPoint = new IPEndPoint(IPAddress.Loopback, 4530);
		}
		public static IPEndPoint MatchServiceEndPoint { get; set; }
	}
}
