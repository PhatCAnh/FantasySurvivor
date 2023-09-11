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
			coinInGame = 100;
			timeInGame = 0;
		}
		
		

		private int _coinInGame;

		private float _timeInGame;

		public int coinInGame
		{
			get => _coinInGame;
			set {
				if(coinInGame == value) return;
				_coinInGame = value;
				RaiseDataChanged(nameof(coinInGame));
			}
		}
		
		public float timeInGame
		{
			get => _timeInGame;
			set {
				if(timeInGame.Equals(value)) return;
				_timeInGame = value;
				RaiseDataChanged(nameof(timeInGame));
			}
		}
	}
}