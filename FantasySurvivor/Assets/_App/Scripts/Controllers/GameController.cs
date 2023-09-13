using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using MonsterStat = Stat.MonsterStat;

public class GameController : Controller<GameApp>
{
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public TowerView tower;

	public MapView map;

	public List<Monster> listMonster;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}

	private void Start()
	{
		listMonster = new List<Monster>();
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

	public void StartGame(int chapter)
	{
		map = app.resourceManager.ShowPopup(PopupType.MainInGame).GetComponent<MapView>();
		map.Init(chapter);
		Instantiate(app.resourceManager.GetMap((MapType) chapter));
		tower = SpawnTower();
		listMonster.Clear();
	}

	public void WinGame()
	{
	}

	public void LoseGame()
	{
		isEndGame = true;
		app.resourceManager.ShowPopup(PopupType.LoseGame);
	}

	public void ChangeSceneHome()
	{
		var load = SceneManager.LoadSceneAsync("scn_Main", LoadSceneMode.Single);
		load.completed += o =>
		{
			app.resourceManager.CloseAllPopup();
			ShowChoiceMap();
		};
	}

	public void RestartGame()
	{
	}

	public void CollectedItem(ItemType type)
	{

	}

	[Button]
	public void TestConfig()
	{
		tower.AddExp(10);
	}


	public Monster SpawnMonster(MonsterType monsterType,int health, int adMonster, int coin, int exp)
	{
		var statMonster = app.configs.dataStatMonsterConfigTable.GetConfig(monsterType);
		var monsterStat = new MonsterStat(statMonster.moveSpeed, health, adMonster, statMonster.attackSpeed, statMonster.attackRange, exp);

		var monsterIns = Instantiate(app.resourceManager.GetMonster(monsterType)).GetComponent<Monster>();
		
		monsterIns.transform.position = RandomPositionSpawnMonster();
		
		monsterIns.Init(monsterStat, coin);
		
		listMonster.Add(monsterIns);
		
		return monsterIns;
	}

	public void MonsterDie(Monster mons, bool canDestroy = true)
	{
		var gemExp = app.resourceManager.GetItem(ItemType.GemExp).GetComponent<GemExp>();
		gemExp.Init(mons.stat.exp.BaseValue);

		var position = mons.transform.position;
		Instantiate(gemExp, position, quaternion.identity);
		
		map.model.coinInGame += mons.model.coin;
		Singleton<PoolTextPopup>.instance.GetObjectFromPool(position, mons.model.coin.ToString(), TextPopupType.GoldCoin);
		listMonster.Remove(mons);

		if(canDestroy) Destroy(mons.gameObject);
	}

	public Monster GetFirstMonster()
	{
		var nearestMons = listMonster.FirstOrDefault(monster => Vector2.Distance(monster.transform.position, tower.transform.position) < tower.model.attackRange + monster.size);
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

	public void TowerDie(TowerView towerView)
	{
		LoseGame();
	}

	private Vector2 RandomPositionSpawnMonster()
	{
		int posX;
		int posY;
		int randomTopDown = Random.Range(0, 2);
		if(randomTopDown == 0)
		{
			posX = Random.Range(-21, 21);
			posY = (Random.Range(0, 2) * 2 - 1) * 20;
		}
		else
		{
			posX = (Random.Range(0, 2) * 2 - 1) * 20;
			posY = Random.Range(-21, 21);
		}
		return new Vector2(posX, posY);
	}

	private TowerView SpawnTower()
	{
		var towerPrefab = Instantiate(app.resourceManager.GetItem(ItemType.Tower))
			.GetComponent<TowerView>();
		towerPrefab.transform.position = Vector2.zero;

		var healthBar = Instantiate(app.resourceManager.GetItem(ItemType.HealthBar), app.resourceManager.rootContainer)
			.GetComponent<HealthBar>();
		healthBar.Init(towerPrefab);

		towerPrefab.Init(healthBar);

		return towerPrefab;
	}

}