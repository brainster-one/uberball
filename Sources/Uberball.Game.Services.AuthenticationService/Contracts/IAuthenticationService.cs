
namespace Uberball.Game.Services.AuthenticationService {
	using System;
	using System.ServiceModel;

	[ServiceContract] public interface IAuthenticationService {
		[OperationContract] 
		Guid LogOn(string login, string password);
	}
}
