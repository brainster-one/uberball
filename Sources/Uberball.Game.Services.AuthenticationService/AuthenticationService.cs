
namespace Uberball.Game.Services.AuthenticationService {
	using System;

	public class AuthenticationService : IAuthenticationService {
		public Guid LogOn(string login, string password) {
			return Guid.NewGuid();
		}
	}
}
