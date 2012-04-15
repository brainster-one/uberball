
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System.Collections.Generic;
	using System.Windows;
	using Khrussk.NetworkRealm;
	using Services;
	using ViewModels;

	/// <summary>Entity model state changed.</summary>
	public class EntityModelStateChangedBehavior : IBehavior {
		/// <summary>Initializes a new instance of the EntityModelStateChangedBehavior class.</summary>
		/// <param name="viewModel">View model.</param>
		public EntityModelStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
		}

		/// <summary>Handle.</summary>
		/// <param name="model">Changed model.</param>
		/// <param name="action">Network action.</param>
		public void Handle(object model, EntityState action) {
			

			lock (_viewModel) {
				Deployment.Current.Dispatcher.BeginInvoke(() => {
					var viewModel = GetViewModel(model);
																switch (action) {
																case EntityState.Added:
																	ServiceLocator.EntityMappingService.ToViewModel(model, ref viewModel);
																	_entities.Add(model, viewModel);
																	_viewModel.Entities.Add(viewModel);
																	break;
																case EntityState.Modified:
																	ServiceLocator.EntityMappingService.ToViewModel(model, ref viewModel);
																	break;
																case EntityState.Removed:
																	_entities.Remove(model);
																	_viewModel.Entities.Remove(viewModel);
																	break;
																}
															});
			}
		}

		/// <summary>Gets view model for specified model.</summary>
		/// <param name="model">Model to search view model by.</param>
		/// <returns>View model, or null.</returns>
		private object GetViewModel(object model) {
			object viewModel;
			_entities.TryGetValue(model, out viewModel);
			return viewModel;
		}

		/// <summary>Match view model.</summary>
		readonly MatchViewModel _viewModel;

		/// <summary>Entity model to view model map.</summary>
		readonly Dictionary<object /*model*/, object> _entities = new Dictionary<object, object>();
	}
}
