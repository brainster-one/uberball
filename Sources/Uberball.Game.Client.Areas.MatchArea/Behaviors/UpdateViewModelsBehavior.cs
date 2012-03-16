
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Windows;
	using Ardelme.Core;
	using Uberball.Game.Client.Areas.MatchArea.ViewModels.Entities;
	using Uberball.Game.Logic.Entities;

	public class UpdateViewModelsBehavior : RealmBehavior {
		public UpdateViewModelsBehavior(ObservableCollection<object> list) {
			_viewModels = list;
		}

		public override void AddEntity(IRealm realm, object entity) {
			var viewModel = CreateViewModel(entity);
			_viewModelsMap.Add(entity, viewModel);

			Deployment.Current.Dispatcher.BeginInvoke(() => _viewModels.Add(viewModel));
		}

		public override void RemoveEntity(IRealm realm, object entity) {
//			_viewModels.Remove(entity); /* TODO: find viewModel and remove */
		}

		public override void ModifyEntity(IRealm realm, object entity) {	
			var viewModel = _viewModelsMap[entity];
			UpdateViewModel(viewModel, entity);
		}

		private object CreateViewModel(object entity) {
			return new PlayerViewModel(); /* TODO: convert to viewModel correct */
		}

		private void UpdateViewModel(object viewModel, object entity) {
			var playerVm = (PlayerViewModel)viewModel;
			var playerEn = (Player)entity;

			playerVm.Name = playerEn.Name;
		}

		private Dictionary<object /*entity*/, object/*viewmodel*/> _viewModelsMap = new Dictionary<object,object>();
		private ObservableCollection<object> _viewModels;
	}
}
