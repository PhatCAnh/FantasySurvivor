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
			_levelCharacter = 1;
			_expMax = 10;
			levelInGame = 1;
		}

		private float _timeInGame;
		
		private int _levelCharacter;
		
		private int _expCurrent;

		private int _expMax;

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

		public int ExpMax
		{
			get => _expMax;
			set {
				if(ExpMax == value) return;
				_expMax = value;
				RaiseDataChanged(nameof(ExpMax));
			}
		}
		
		public int ExpCurrent
		{
			get => _expCurrent;
			set {
				if(ExpCurrent == value) return;
				_expCurrent = value;
				RaiseDataChanged(nameof(ExpCurrent));
			}
		}
		
		public int LevelCharacter
		{
			get => _levelCharacter;
			set {
				if(LevelCharacter == value) return;
				_levelCharacter = value;
				RaiseDataChanged(nameof(LevelCharacter));
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