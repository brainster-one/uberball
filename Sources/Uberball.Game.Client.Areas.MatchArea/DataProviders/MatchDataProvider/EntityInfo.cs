
namespace Uberball.Game.Client.Areas.MatchArea.DataProviders.MatchDataProvider {
	/// <summary>Entity metadata.</summary>
	public class EntityInfo {
		/// <summary>Gets or sets entity network Id.</summary>
		public int Id { get; set; }

		/// <summary>Entity itself.</summary>
		public object Entity { get; set; }

		/// <summary>Gets or sets entity view model.</summary>
		public object ViewModel { get; set; }
	}
}
