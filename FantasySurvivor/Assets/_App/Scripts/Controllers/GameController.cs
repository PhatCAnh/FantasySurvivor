using ArbanFramework;
using ArbanFramework.MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MR;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using UnityEngine.Serialization;

public class GameController : Controller<GameApp>
{
	[SerializeField] private Character _characterPrefab;
	// [SerializeField] private DropItem _keyPrefab;
	// [SerializeField] private DropItem _oilPrefab;
	// [SerializeField] private Door _doorPrefab;
	//
	// [HideInInspector] public MapView map;

	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public Character character;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}

	public void ShowChoiceMap()
	{
		app.resourceManager.ShowPopup(PopupType.ChoiceMap);
	}

	// public void StartGame()
	// {
	// 	var scene = SceneManager.LoadSceneAsync("scn_Game", LoadSceneMode.Single);
	// 	scene.completed += operation =>
	// 	{
	// 		LoadGame(1);
	//
	// 		character = SpawnCharacter();
	// 		SpawnDoor(new Vector2(9, 15));
	// 		app.resourceManager.ShowPopup(PopupType.Main, (gameObj) =>
	// 		{
	// 			var mapView = gameObj.GetComponent<MapView>();
	// 			mapView.Init(new MapModel(1));
	// 			map = mapView;
	// 		});
	// 		for(int i = 0; i < map.model.totalKey; i++)
	// 		{
	// 			var posX = Random.Range(3, 15);
	// 			var posY = Random.Range(5, 15);
	// 			var pos = new Vector2(posX, posY);
	// 			SpawnDropItem(ItemType.Key, pos);
	// 		}
	// 		
	// 		var posOilX = Random.Range(3, 15);
	// 		var posOilY = Random.Range(7, 15);
	// 		var posOil = new Vector2(posOilX, posOilY);
	// 		SpawnDropItem(ItemType.Oil, posOil);
	// 		isEndGame = false;
	// 	};
	// }

	// public void WinGame()
	// {
	// 	isEndGame = true;
	// 	app.resourceManager.ShowPopup(PopupType.WinGame);
	// }
	//
	// public void LoseGame()
	// {
	// 	isEndGame = true;
	// 	app.resourceManager.ShowPopup(PopupType.LoseGame);
	// }
	//
	// public void RestartGame()
	// {
	// 	var scene = SceneManager.LoadSceneAsync("scn_Main", LoadSceneMode.Single);
	// 	scene.completed += operation =>
	// 	{
	// 		app.resourceManager.CloseAllPopup();
	// 		Singleton<GameController>.instance.ShowChoiceMap();
	// 	};
	// }
	//
	// public void CollectedItem(ItemType type)
	// {
	// 	switch (type)
	// 	{
	// 		case ItemType.Key:
	// 			map.model.collectedKey += 1;
	// 			break;
	// 		case ItemType.Oil:
	// 			character.model.currentOil += 5f;
	// 			break;
	// 	}
	// }
	//
	// protected override void OnDestroy()
	// {
	// 	base.OnDestroy();
	// 	Singleton<GameController>.Unset(this);
	// }
	//
	// public void LoadGame(int level)
	// {
	// 	SpawnMap(level);
	// }
	//
	// private void SpawnMap(int level)
	// {
	// 	var cfg = app.configs.mapConfigs.GetConfig(level);
	// 	var map = Instantiate(app.resourceManager.mapPrefab);
	// 	map.SpawnMap(cfg.mapConfig);
	// }
	//
	// private Character SpawnCharacter()
	// {
	// 	//create character
	// 	return Instantiate(_characterPrefab, new Vector2(9, 5), quaternion.identity);
	// }
	//
	// private DropItem SpawnDropItem(ItemType type, Vector2 spawnPoint)
	// {
	// 	var item = type switch
	// 	{
	// 		ItemType.Key => _keyPrefab,
	// 		ItemType.Oil => _oilPrefab,
	// 		_ => null
	// 	};
	// 	Instantiate(item, spawnPoint, quaternion.identity);
	// 	return item;
	// }
	//
	// private Door SpawnDoor(Vector2 spawnPoint)
	// {
	// 	var door = Instantiate(_doorPrefab, spawnPoint, quaternion.identity);
	// 	return door;
	// }
}