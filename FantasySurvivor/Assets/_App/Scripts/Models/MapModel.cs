using ArbanFramework.MVC;
namespace FantasySurvivor
{
	public class MapModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase( nameof(MapModel) + ".dataChanged" );

		public MapModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}
		
		public MapModel() : base(dataChangedEvent)
		{
			expInGame = 100;
			timeInGame = 0;
			levelInGame = 1;
		}

		private float _timeInGame;

		private int _expInGame;

		private int _levelInGame;
		
		public float timeInGame
		{
			get => _timeInGame;
			set {
				if(timeInGame.Equals(value)) return;
				_timeInGame = value;
				RaiseDataChanged(nameof(timeInGame));
			}
		}

		public int expInGame
		{
			get => _expInGame;
			set {
				if(expInGame == value) return;
				_expInGame = value;
				RaiseDataChanged(nameof(expInGame));
			}
		}
		
		public int levelInGame
		{
			get => _levelInGame;
			set {
				if(levelInGame == value) return;
				_levelInGame = value;
				RaiseDataChanged(nameof(levelInGame));
			}
		}
	}
}