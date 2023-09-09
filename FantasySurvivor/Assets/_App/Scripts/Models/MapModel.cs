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
			coinInMap = 100;
		}
		
		

		private int _coinInMap;

		public int coinInMap
		{
			get => _coinInMap;
			set {
				if(coinInMap == value) return;
				_coinInMap = value;
				RaiseDataChanged(nameof(coinInMap));
			}
		}
	}
}