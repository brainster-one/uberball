
namespace Uberball.Game.Client.Areas.MatchArea.Locators {
	using DataProviders;
	
	public class DataProviderLocator {
		private static MatchDataProvider _matchDataProvider;

		public static MatchDataProvider MatchDataProvider {
			get { return _matchDataProvider ?? (_matchDataProvider = new MatchDataProvider()); }
		}
	}
}
