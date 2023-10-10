using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using UnityEngine;

public class MapInfinityController : Controller<GameApp>
{
	private Vector3 _size;
	private Character _character => gameController.character;
	private Map[] arrMap;
	private Map mapCenter;
	private Rect myRect;
	private GameController gameController => Singleton<GameController>.instance;

	public void Init(int chapter)
	{
		var mapPrefab = app.resourceManager.GetMap((MapType) chapter);
		_size = mapPrefab.tilemap.size;
		arrMap = new Map[9];
		for(int i = 0; i < 3; i++)
		{
			var mapObjectRight = Instantiate(mapPrefab, transform);
			mapObjectRight.Init(MapVerticalType.Right, (MapHorizontalType) i);
			arrMap[i * 3] = mapObjectRight;

			var mapObjectMid = Instantiate(mapPrefab, transform);
			mapObjectMid.Init(MapVerticalType.Mid, (MapHorizontalType) i);
			arrMap[i * 3 + 1] = mapObjectMid;

			var mapObjectLeft = Instantiate(mapPrefab, transform);
			mapObjectLeft.Init(MapVerticalType.Left, (MapHorizontalType) i);
			arrMap[i * 3 + 2] = mapObjectLeft;
		}
		mapCenter = arrMap.First(map =>
			map.horizontalPos == MapHorizontalType.Mid
			&& map.verticalPos == MapVerticalType.Mid);
		UpdatePosMap();
	}

	private void LateUpdate()
	{
		//CheckCharacterPos();
	}

	private void CheckCharacterPos()
	{
		if(myRect.Contains(_character.transform.position))
			return;
		foreach(var map in arrMap)
		{
			var mapPosition = map.transform.position;
			var rect = new Rect(mapPosition.x - _size.x / 2, mapPosition.y - _size.x / 2, _size.x, _size.y);
			if(rect.Contains(_character.transform.position))
			{
				mapCenter = map;
				UpdateMap(map);
				break;
			}
		}
		UpdatePosMap();
	}

	private void UpdateMap(Map map)
	{
		switch (map.horizontalPos)
		{
			case MapHorizontalType.Top:
				foreach(var item in arrMap)
				{
					item.horizontalPos = (item.horizontalPos + 1);
					if(!Enum.IsDefined(typeof(MapHorizontalType), item.horizontalPos))
					{
						item.horizontalPos = 0;
					}
				}
				break;
			case MapHorizontalType.Mid:
				break;
			case MapHorizontalType.Bot:
				foreach(var item in arrMap)
				{
					item.horizontalPos = (item.horizontalPos - 1);
					if(!Enum.IsDefined(typeof(MapHorizontalType), item.horizontalPos))
					{
						item.horizontalPos = (MapHorizontalType) 2;
					}
				}
				break;
		}
		
		switch (map.verticalPos)
		{
			case MapVerticalType.Left:
				foreach(var item in arrMap)
				{
					item.verticalPos = (item.verticalPos + 1);
					if(!Enum.IsDefined(typeof(MapVerticalType), item.verticalPos))
					{
						item.verticalPos = 0;
					}
				}
				break;
			case MapVerticalType.Mid:
				break;
			case MapVerticalType.Right:
				foreach(var item in arrMap)
				{
					item.verticalPos = (item.verticalPos - 1);
					if(!Enum.IsDefined(typeof(MapVerticalType), item.verticalPos))
					{
						item.verticalPos = (MapVerticalType) 2;
					}
				}
				break;
		}
	}
	
	


	private void UpdatePosMap()
	{
		myRect = new Rect(mapCenter.transform.position.x - _size.x / 2, mapCenter.transform.position.y - _size.x / 2, _size.x, _size.y);
		foreach(var map in arrMap)
		{
			var position = map.transform.position;
			position.x = map.verticalPos switch
			{
				MapVerticalType.Right => _size.x,
				MapVerticalType.Mid => 0,
				MapVerticalType.Left => -_size.x,
			};

			position.y = map.horizontalPos switch
			{
				MapHorizontalType.Top => _size.y,
				MapHorizontalType.Mid => 0,
				MapHorizontalType.Bot => -_size.y,
			};
			map.transform.position = position + mapCenter.transform.position;
		}
	}
}