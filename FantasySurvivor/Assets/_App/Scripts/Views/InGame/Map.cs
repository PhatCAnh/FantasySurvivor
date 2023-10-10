using ArbanFramework.MVC;
using UnityEngine.Tilemaps;
namespace FantasySurvivor
{
	public class Map : View<GameApp>
	{
		public Tilemap tilemap;
		public MapHorizontalType horizontalPos; //chieu ngang
		public MapVerticalType verticalPos; //chieu doc

		public void Init(MapVerticalType vertical, MapHorizontalType horizontal)
		{
			this.horizontalPos = horizontal;
			this.verticalPos = vertical;
		}
	}
}