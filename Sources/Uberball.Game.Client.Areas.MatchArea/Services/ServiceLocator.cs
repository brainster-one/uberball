
namespace Uberball.Game.Client.Areas.MatchArea.Services {
	public class ServiceLocator {
		private static MatchService _matchService;
		private static EntityMappingService _entityMappingService;

		public static MatchService MatchService {
			get { return _matchService ?? (_matchService = new MatchService()); }
		}

		public static EntityMappingService EntityMappingService {
			get { return _entityMappingService ?? (_entityMappingService = new EntityMappingService()); }
		}
	}
}
