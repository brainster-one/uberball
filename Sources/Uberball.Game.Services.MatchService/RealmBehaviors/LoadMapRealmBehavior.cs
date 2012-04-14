
namespace Uberball.Game.Services.MatchService.RealmBehaviors {
	using Ardelme.Core;
	using Logic.Entities;

	/// <summary>Enter/Leave realm events handler.</summary>
	sealed class LoadMapRealmBehavior : RealmBehavior {
		/// <summary>Realm has been started.</summary>
		/// <param name="realm">Realm.</param>
		public override void Start(IRealm realm) {
			//_realm.AddEntity(new Decoration { X = 64 * 3, Y = 64 * 3 });
			//_realm.AddEntity(new Decoration { X = 64 * 6, Y = 64 * 3 });
			realm.AddEntity(Ground.CreateBlock(64 * 0, 64 * 10));
			realm.AddEntity(Ground.CreateBlock(64 * 1, 64 * 1));
			realm.AddEntity(Ground.CreateBlock(64 * 1, 64 * 5));
			realm.AddEntity(Ground.CreateBlock(64 * 2, 64 * 5));
			realm.AddEntity(Ground.CreateBlock(64 * 3, 64 * 5));
			realm.AddEntity(Ground.CreateBlock(64 * 3, 64 * 6));
			realm.AddEntity(Ground.CreateBlock(64 * 4, 64 * 6));
			realm.AddEntity(Ground.CreateBlock(64 * 5, 64 * 6));
			realm.AddEntity(Ground.CreateBlock(64 * 6, 64 * 8));
			realm.AddEntity(Ground.CreateBlock(64 * 7, 64 * 8));
			realm.AddEntity(Ground.CreateBlock(64 * 8, 64 * 8));
			realm.AddEntity(Ground.CreateBlock(64 * 6, 64 * 5));
			
			//_realm.AddEntity(new Ball { X = 64 * 1, Y = 64 * 3 });
			for (var i = 0; i < 20; ++i) {
				realm.AddEntity(Ground.CreateBlock(64 * i, 64 * 9));
				realm.AddEntity(Ground.CreateBlock(64 * 12, 64 * i));
			}
			//_realm.AddEntity(new Block { X = 64 * 1, Y = 64 * 2 });*/
			/*_realm.AddEntity(new Ground(new[] {
				new Point(10, 100), new Point(290, 250), new Point(420, 450),  new Point(580, 350), new Point(620, 750), new Point(30, 790)
			}));*/
		}
	}
}
