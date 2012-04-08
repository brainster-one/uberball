
namespace Uberball.Game.Client.Areas.MatchArea.Services.Mappers {
	public interface IEntityMapper {
		void ToViewModel(object model, ref object viewModel);
		void ToView(object viewModel, ref object view);
	}
}
