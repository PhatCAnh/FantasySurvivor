using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using UnityEngine.Serialization;

public class GameController : Controller<GameApp>
{
	[SerializeField] private Character _characterPrefab;

	[SerializeField] private HealthBar _healthBarPrefab;

	public Monster monsterPrefab;

	[SerializeField] private Transform _test;


	// [SerializeField] private DropItem _keyPrefab;
	// [SerializeField] private DropItem _oilPrefab;
	// [SerializeField] private Door _doorPrefab;
	//
	// [HideInInspector] public MapView map;


	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public TowerView tower;
	public Monster monster;

	public List<Monster> listMonster = new List<Monster>();

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}
	private void Start()
	{
		StartGame();
	}

	private void Update()
	{
		// var test = Instantiate(_test);
		// Vector2 random = RandomPointOnCircleEdge(5);
		// Vector2 charPos = character.transform.position;
		// test.position = new Vector2(charPos.x, charPos.y) + random;
	}

	Vector2 RandomPointOnCircleEdge(float radius)
	{
		float angle = Random.Range(0, 2 * Mathf.PI); // Random angle in radians
		float x = radius * Mathf.Cos(angle);
		float y = radius * Mathf.Sin(angle);
		return new Vector2(x, y);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<GameController>.Unset(this);
	}

	public void ShowChoiceMap()
	{
		app.resourceManager.ShowPopup(PopupType.ChoiceMap);
	}

	public void StartGame()
	{
		// character = SpawnCharacter(Vector2.zero);
		//monster = SpawnMonster(new Vector2(10, 10));

	}

	public void WinGame()
	{
	}

	public void LoseGame()
	{
	}

	public void RestartGame()
	{
	}

	public void CollectedItem(ItemType type)
	{

	}


	public Monster SpawnMonster(Vector2 spawnPos)
	{
		Monster monsterIns = Instantiate(monsterPrefab);

		monsterIns.transform.position = spawnPos;

		monsterIns.Init(new MonsterModel(0.25f, 20));

		listMonster.Add(monsterIns);

		return monsterIns;
	}

	public void MonsterDie(Monster mons, bool canDestroy = true)
	{
		listMonster.Remove(mons);
		if(canDestroy) Destroy(mons.gameObject);
	}

	public Monster GetMonsterNearest()
	{
		var nearestMons = listMonster.FirstOrDefault();
		var towerPos = tower.transform.position;
		if(nearestMons == null) return nearestMons;
		var nearestDistance = Vector2.Distance(nearestMons.transform.position, towerPos);
		for(int i = 1; i < listMonster.Count; i++)
		{
			var distance = Vector2.Distance(listMonster[i].transform.position, towerPos);
			if(distance < nearestDistance)
			{
				nearestMons = listMonster[i];
				nearestDistance = distance;
			}
		}
		return nearestMons;
	}

	private Character SpawnCharacter(Vector2 spawnPos)
	{
		//create character
		Character characterIns = Instantiate(_characterPrefab);
		var hpIns = Instantiate(_healthBarPrefab, app.resourceManager.rootContainer);

		characterIns.Init(new CharacterModel(1f, 10));
		characterIns.transform.position = spawnPos;

		hpIns.Init(characterIns);

		return characterIns;


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
}