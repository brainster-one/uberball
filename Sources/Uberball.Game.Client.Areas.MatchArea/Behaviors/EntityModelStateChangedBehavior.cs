
namespace Uberball.Game.Client.Areas.MatchArea.Behaviors {
	using System;
	using System.Collections.Generic;
	using System.Windows;
	using Khrussk.NetworkRealm;
	using Logic.Entities;
	using ViewModels;
	using ViewModels.Mappers;

	/// <summary>Entity model state changed.</summary>
	public class EntityModelStateChangedBehavior : IBehavior {
		/// <summary>Initializes a new instance of the EntityModelStateChangedBehavior class.</summary>
		/// <param name="viewModel">View model.</param>
		public EntityModelStateChangedBehavior(MatchViewModel viewModel) {
			_viewModel = viewModel;
			_mappers.Add(typeof(Player), new PlayerMapper());
			_mappers.Add(typeof(Block), new BlockMapper());
			_mappers.Add(typeof(Ball), new BallMapper());
			_mappers.Add(typeof(Decoration), new DecorationMapper());
		}

		/// <summary>Handle.</summary>
		/// <param name="model">Changed model.</param>
		/// <param name="action">Network action.</param>
		public void Handle(object model, EntityState action) {
			var viewModel = GetViewModel(model);
			var mapper = GetMapper(model.GetType());

			Deployment.Current.Dispatcher.BeginInvoke(() => {
				switch (action) {
				case EntityState.Added:
					mapper.Map(model, ref viewModel);
					_entities.Add(model, viewModel);
					_viewModel.Entities.Add(viewModel);
					break;
				case EntityState.Modified:
					mapper.Map(model, ref viewModel);
					break;
				case EntityState.Removed:
					_entities.Remove(model);
					_viewModel.Entities.Remove(viewModel);
					break;
				}
			});
		}

		/// <summary>Gets view model for specified model.</summary>
		/// <param name="model">Model to search view model by.</param>
		/// <returns>View model, or null.</returns>
		private object GetViewModel(object model) {
			object viewModel;
			_entities.TryGetValue(model, out viewModel);
			return viewModel;
		}

		/// <summary>Gets mapper.</summary>
		/// <param name="type">Type.</param>
		/// <returns>Mapper.</returns>
		private IMapper GetMapper(Type type) {
			IMapper mapper;
			_mappers.TryGetValue(type, out mapper);
			if (mapper == null) throw new InvalidOperationException("Can not find entity mapper for " + type);
			return mapper;
		}

		/// <summary>Match view model.</summary>
		readonly MatchViewModel _viewModel;

		/// <summary>Entity model to view model map.</summary>
		readonly Dictionary<object /*model*/, object> _entities = new Dictionary<object, object>();

		/// <summary>Mappers.</summary>
		readonly Dictionary<Type, IMapper> _mappers = new Dictionary<Type, IMapper>();
	}
}
