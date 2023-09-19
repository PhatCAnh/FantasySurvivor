﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using JetBrains.Annotations;
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

	public void ShowMainHome()
	{
		app.resourceManager.ShowPopup(PopupType.MainUI);
	}

	public void StartGame(int chapter)
	{
		ChangeScene("scn_Game",  () => LoadMap(chapter));
	}
	
	public void WinGame()
	{
		Debug.Log("Wingame r ne");
	}

	public void LoseGame()
	{
		isEndGame = true;
		app.resourceManager.ShowPopup(PopupType.LoseGame);
	}

	public void ChangeScene(string nameScene, [CanBeNull] Action callback)
	{
		var load = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
		load.completed += o =>
		{
			callback?.Invoke();
		};
	}

	public void ChangeSceneHome()
	{
		var load = SceneManager.LoadSceneAsync("scn_Main", LoadSceneMode.Single);
		load.completed += o =>
		{
			app.resourceManager.CloseAllPopup();
			ShowMainHome();
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
		app.configs.dataUpBaseStatTower.GetConfig(1);
	}


	public Monster SpawnMonster(MapView.WaveData wave)
	{
		var statMonster = app.configs.dataStatMonster.GetConfig(wave.monsterType);
		var monsterStat = new MonsterStat(statMonster.moveSpeed, wave.healthMonster, wave.adMonster, statMonster.attackSpeed, statMonster.attackRange, wave.expMonster);

		var monsterIns = Instantiate(app.resourceManager.GetMonster(wave.monsterType)).GetComponent<Monster>();
		
		monsterIns.transform.position = RandomPositionSpawnMonster();
		
		monsterIns.Init(monsterStat, wave);
		
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
		mons.wave.monsterInWave.Remove(mons);

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

		var model = new TowerModel(
			 (int)GetStatTower(app.models.dataPlayerModel.levelHealth, TypeStatTower.Health),
			 GetStatTower(app.models.dataPlayerModel.levelAs, TypeStatTower.AttackSpeed),
			(int) GetStatTower(app.models.dataPlayerModel.levelAd, TypeStatTower.AttackDamage),
			 GetStatTower(app.models.dataPlayerModel.levelAr, TypeStatTower.AttackRange)
			);
		towerPrefab.Init(model ,healthBar);

		return towerPrefab;
	}

	private float GetStatTower(int level, TypeStatTower type)
	{
		float value = 0;
		float baseValue = 0;
		var dataLevel = app.configs.dataUpBaseStatTower.GetConfig(level);
		var dataStatBase = app.configs.dataStatTower.GetConfig(TowerType.Basic);
		switch (type)
		{
			case TypeStatTower.AttackDamage:
				value = dataLevel.dataAttackDamage.value;
				baseValue = dataStatBase.attackDamage;
				break;
			case TypeStatTower.AttackRange:
				value = dataLevel.dataAttackRange.value;
				baseValue = dataStatBase.attackRange;
				break;
			case TypeStatTower.AttackSpeed:
				value = dataLevel.dataAttackSpeed.value;
				baseValue = dataStatBase.attackSpeed;
				break;
			case TypeStatTower.Health:
				value = dataLevel.dataHealth.value;
				baseValue = dataStatBase.health;
				break;
		}
		return value + baseValue;
	}

	private void LoadMap(int chapter)
	{
		map = app.resourceManager.ShowPopup(PopupType.MainInGame).GetComponent<MapView>();
		map.Init(chapter);
		Instantiate(app.resourceManager.GetMap((MapType) chapter));
		tower = SpawnTower();
		listMonster.Clear();
	}
}