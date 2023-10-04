using System.Collections.Generic;
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
			expInGame = GameConst.gemStartGame;
			levelInGame = 1;
		}

		private float _timeInGame;

		private int _expInGame;

		private int _levelInGame;

		private int _monsterKilled;

		private int _goldCoinCollected;
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
		
		public int monsterKilled
		{
			get => _monsterKilled;
			set {
				if(monsterKilled == value) return;
				_monsterKilled = value;
				RaiseDataChanged(nameof(monsterKilled));
			}
		}
		
		public int goldCoinCollected
		{
			get => _goldCoinCollected;
			set {
				if(goldCoinCollected == value) return;
				_goldCoinCollected = value;
			}
		}
	}
}