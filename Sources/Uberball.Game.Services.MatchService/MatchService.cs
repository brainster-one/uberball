
namespace Uberball.Game.Services.MatchService {
	using Khrussk.NetworkRealm;
	using System.Net;

	public class MatchService {
		public MatchService() {
			_service = new RealmService();
		}

		public void Start(IPEndPoint endpoint) {
			_service.Start(endpoint);
		}

		public void Stop() {
			_service.Stop();
		}

		RealmService _service;
	}
}
