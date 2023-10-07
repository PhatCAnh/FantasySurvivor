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
using MonsterStat = FantasySurvivor.MonsterStat;

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

	private Vector3 _camSize;
	private float _width;
	private float _height;

	private void Awake()
	{
		Singleton<GameController>.Set(this);
	}

	private void Start()
	{
		listMonster = new List<Monster>();
		
		foreach(var skill in app.resourceManager.GetListSkill())
		{
			skill.Init(app.configs.dataLevelSkill.GetConfig(skill.name).data);
		}
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
		Singleton<PoolDropItem>.instance.ReturnAllObject();
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
	public void TestMethod()
	{
		character.AddHealth(0);
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
			Singleton<PoolDropItem>.instance.GetObjectFromPool(mons.transform.position, mons.stat.exp.BaseValue);
		}

		listMonster.Remove(mons);
		mons.wave.monsterInWave.Remove(mons);
		Destroy(mons.gameObject);
	}

	public Monster GetRandomMonster()
	{
		var characterPos = character.transform.position;
		Rect myRect = new Rect(characterPos.x - _width / 2, characterPos.y - _height / 2, _width, _height);
		var listMonsterInRect = new List<Monster>();
		foreach(var mons in listMonster)
		{
			if(myRect.Contains(mons.transform.position))
			{
				listMonsterInRect.Add(mons);
			}
		}
		return listMonsterInRect.Count != 0 ? listMonsterInRect[Random.Range(0, listMonsterInRect.Count)] : null;
	}

	public void CharacterDie(Character characterView)
	{
		LoseGame();
	}

	public GameObject SpawnBullet(GameObject prefab)
	{
		var bullet = Instantiate(prefab);
		return bullet;
	}

	// public (GameObject, Monster) UseSkill(SkillName name)
	// {
	// 	var mons = GetRandomMonster();
	// 	if(mons != null)
	// 	{
	// 		var skill = Instantiate(
	// 			app.resourceManager.GetSkill(name).skillPrefab,
	// 			new Vector3(mons.transform.position.x, mons.transform.position.y),
	// 			quaternion.identity
	// 		);
	// 		return (skill, mons);
	// 	}
	// 	return (null, null);
	// }

	// public void UseSkillHaveFlightRoute(SkillName name)
	// {
	// 	var mons = GetRandomMonster();
	// 	if(mons != null)
	// 	{
	// 		var skill = Instantiate(
	// 			app.resourceManager.GetSkill(name).skillPrefab,
	// 			character.transform.position,
	// 			quaternion.identity
	// 		);
	// 		skill.GetComponent<BulletView>().Init(mons, name);
	// 	}
	// }

	public void Collected(DropItem dropItem)
	{
		switch (dropItem.type)
		{
			case DropItemType.Exp:
				map.model.ExpCurrent += dropItem.value;
				if(map.model.ExpCurrent > map.model.ExpMax)
				{
					map.model.ExpCurrent -= map.model.ExpMax;
					map.model.LevelCharacter++;
					map.model.ExpMax += 50;
					app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
				}
				break;
			case DropItemType.Magnet:
				foreach(var item in Singleton<PoolDropItem>.instance.usedList)
				{
					item.GetComponent<DropItem>().Collect();
				}
				
				break;
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
		var characterPrefab = Instantiate(app.resourceManager.GetItem(Type.Character))
			.GetComponent<Character>();
		characterPrefab.transform.position = Vector2.zero;

		_healthBar = Instantiate(app.resourceManager.GetItem(Type.HealthBar), app.resourceManager.rootContainer)
			.GetComponent<HealthBar>();
		_healthBar.Init(characterPrefab);

		var stat = new CharacterStat(2.5f, 100, 5, 20);
		characterPrefab.Init(stat);

		return characterPrefab;
	}

	private void LoadMap(int chapter)
	{
		_camSize = Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0));
		_width = 1 / (_camSize.x - 0.5f);
		_height = 1 / (_camSize.y - 0.5f);

		map = app.resourceManager.ShowPopup(PopupType.MainInGame).GetComponent<MapView>();
		map.Init();
		Instantiate(app.resourceManager.GetMap((MapType) chapter));
		character = SpawnChacter();
		listMonster.Clear();
		app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
		//app.analytics.TrackPlay(LevelResult.Start, map.model.levelInGame);
	}
}