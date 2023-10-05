using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Stat;
using Unity.Mathematics;
using UnityEngine.SceneManagement;
using MonsterStat = Stat.MonsterStat;

public class GameController : Controller<GameApp>
{
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public Character character;

	public MapView map;

	public List<Monster> listMonster;

	public Action<Monster> onMonsterDie;

	private HealthBar _healthBar;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}

	private void Start()
	{
		listMonster = new List<Monster>();
		//character = SpawnChacter();
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
		ChangeScene("scn_Game", () => LoadMap(chapter));
	}

	public void WinGame()
	{
		Debug.Log("Wingame r ne");
	}

	public void LoseGame()
	{
		isEndGame = true;
		Singleton<PoolGemExp>.instance.ReturnAllObject();
		app.resourceManager.ShowPopup(PopupType.LoseGame);
		//app.analytics.TrackPlay(LevelResult.Failure, map.model.levelInGame);
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
			if(_healthBar != null) Destroy(_healthBar.gameObject);
			ShowMainHome();
		};
	}

	public void AddReward(Dictionary<TypeItemReward, int> listReward, TypeItemReward type, int value)
	{
		if(listReward.ContainsKey(type))
		{
			listReward[type] += value;
		}
		else
		{
			listReward.Add(type, value);
		}
	}

	public ItemReward SpawnItemReward(TypeItemReward type, int value, Transform parent)
	{
		var rewardPrefab = app.resourceManager.GetItemReward(type);
		var reward = Instantiate(rewardPrefab, parent);
		reward.Init(value);
		return reward;
	}

	public void RestartGame()
	{
	}

	public void ClaimReward(TypeItemReward type, int value)
	{
		switch (type)
		{
			case TypeItemReward.Coin:
				app.models.dataPlayerModel.Coin += value;
				break;
		}
	}

	[Button]
	public void TestConfig()
	{
		app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
	}


	public Monster SpawnMonster(MapView.WaveData wave)
	{
		var statMonster = app.configs.dataStatMonster.GetConfig(wave.idMonster);

		var monsterStat = new MonsterStat(statMonster.moveSpeed, wave.healthMonster, wave.adMonster, statMonster.attackSpeed, statMonster.attackRange, wave.expMonster);

		var monsterIns = Instantiate(app.resourceManager.GetMonster(wave.idMonster)).GetComponent<Monster>();

		monsterIns.transform.position = RandomPositionSpawnMonster(20, monsterIns.justSpawnVertical);

		monsterIns.Init(monsterStat, wave);

		listMonster.Add(monsterIns);

		return monsterIns;
	}
	public void MonsterDie(Monster mons, bool selfDie = false)
	{
		if(!selfDie)
		{
			map.model.monsterKilled++;
			Singleton<PoolGemExp>.instance.GetObjectFromPool(mons.transform.position, mons.stat.exp.BaseValue);
		}

		listMonster.Remove(mons);
		mons.wave.monsterInWave.Remove(mons);
		Destroy(mons.gameObject);
	}

	public Monster GetFirstMonster(float attackRange)
	{
		var nearestMons = listMonster.FirstOrDefault(monster => Vector2.Distance(monster.transform.position, character.transform.position) < attackRange + monster.size);
		var towerPos = character.transform.position;
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
	
	public Monster GetStrongMonster(float attackRange)
	{
		var monsInRange = listMonster.Where(monster => Vector2.Distance(monster.transform.position, character.transform.position) < attackRange + monster.size).ToList();
		if(monsInRange.Count == 0) return null;
		var strongestMons = monsInRange.First();
		foreach(var mons in monsInRange)
		{
			if(mons.model.currentHealthPoint > strongestMons.model.currentHealthPoint)
			{
				strongestMons = mons;
			}
		}
		return strongestMons;
	}

	public void CharacterDie(Character characterView)
	{
		LoseGame();
	}

	public void Collected(GemExp exp)
	{
		map.model.ExpCurrent += exp.valueExp;
		if(map.model.ExpCurrent > map.model.ExpMax)
		{
			map.model.ExpCurrent -= map.model.ExpMax;
			map.model.LevelCharacter++;
			map.model.ExpMax += 50;
			app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
		}
	}
	

	public Vector2 RandomPositionSpawnMonster(float radius, bool justVertical = false)
	{
		float angle = Random.Range(0, 2 * Mathf.PI);
		float x = radius * Mathf.Cos(angle);
		float y = radius * Mathf.Sin(angle);
		return new Vector2(x + character.transform.position.x, y + character.transform.position.y);


		// int posX;
		// int posY;
		// int randomTopDown = Random.Range(0, 2);
		// if(randomTopDown == 0 || justVertical)
		// {
		// 	posX = Random.Range(-21, 21);
		// 	posY = (Random.Range(0, 2) * 2 - 1) * 20;
		// }
		// else
		// {
		// 	posX = (Random.Range(0, 2) * 2 - 1) * 20;
		// 	posY = Random.Range(-21, 21);
		// }
		// return new Vector2(posX, posY);
	}
	
	private Character SpawnChacter()
	{
		var characterPrefab = Instantiate(app.resourceManager.GetItem(ItemType.Character))
			.GetComponent<Character>();
		characterPrefab.transform.position = Vector2.zero;

		_healthBar = Instantiate(app.resourceManager.GetItem(ItemType.HealthBar), app.resourceManager.rootContainer)
			.GetComponent<HealthBar>();
		_healthBar.Init(characterPrefab);

		var stat = new CharacterStat(2.5f, 100, 5, 20);
		characterPrefab.Init(stat);

		return characterPrefab;
	}

	private void LoadMap(int chapter)
	{
		map = app.resourceManager.ShowPopup(PopupType.MainInGame).GetComponent<MapView>();
		map.Init();
		Instantiate(app.resourceManager.GetMap((MapType) chapter));
		character = SpawnChacter();
		listMonster.Clear();
		app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
		//app.analytics.TrackPlay(LevelResult.Start, map.model.levelInGame);
	}
}