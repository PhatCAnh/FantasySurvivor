using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using Random = UnityEngine.Random;
using FantasySurvivor;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class GameController : Controller<GameApp>
{
	public bool isStop => isEndGame || isStopGame;

	public bool isStopGame;

	public bool isEndGame;

	public Character character;

	public MapView map;

	public List<Monster> listMonster;

	public Action<Monster> onMonsterDie;

    public int numberLimitChoiceSkill = 4;// limit skill out game
    private HealthBar _healthBar;
    private int _limitSkill = 2;// limit skill in game
    private int currentSkill = 0;// điếm số skill in game
    public int currentNumberSkill = 0;// điếm số skill outr game
    private Vector3 _camSize;
	private float _width;
	private float _height;

	private Vector3 charPos => character.transform.position;
   public List<SkillData> _listSkill = new List<SkillData>();

    private readonly Dictionary<DropItemType, float> _percentDropItem = new Dictionary<DropItemType, float>();

	private PoolController poolController => Singleton<PoolController>.instance;

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

		var data = app.resourceManager.GetDicDropItem();
		float value = 0;
		foreach(var item in data)
		{
			value += item.Value;
			_percentDropItem.Add(item.Key, value);
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
		ChangeScene(GameConst.nameScene_Game, () => LoadMap(chapter));
	}

	public void WinGame()
	{
		Debug.Log("Wingame r ne");
	}

	public void LoseGame()
	{
		isEndGame = true;
		Singleton<PoolController>.instance.RemoveAllObject(ItemPrefab.GemExp);
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
	// check exit skill in game 
	public void CheckExistSkill()
	{
		currentSkill++;
		if(currentSkill == _limitSkill)
		{
			map.listSkill = character.listSkillDataCurrents;
		}
	}

	public void ChangeSceneHome()
	{
		var load = SceneManager.LoadSceneAsync(GameConst.nameScene_Main, LoadSceneMode.Single);
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
	
	

	public void ResetPool()
	{
		Singleton<PoolController>.instance.RemoveAllPool();
	}


	public Monster SpawnMonster(MapView.WaveData wave)
	{
		var statMonster = app.configs.dataStatMonster.GetConfig(wave.idMonster);

		var monsterStat = new MonsterStat(statMonster.moveSpeed, wave.healthMonster, wave.adMonster, statMonster.attackSpeed, statMonster.attackRange, wave.expMonster);

		var type = (ItemPrefab) Enum.Parse(typeof(ItemPrefab), statMonster.monsterType);

		Singleton<PoolController>.instance.GetObject(type, RandomPositionSpawnMonster(20)).TryGetComponent(out Monster monster);

		//var monsterIns = Instantiate(app.resourceManager.GetMonster(wave.idMonster)).GetComponent<Monster>();
		monster.Init(monsterStat, wave, type);

		listMonster.Add(monster);

		return monster;
	}
	public void MonsterDie(Monster mons, bool selfDie = false)
	{
		if(!selfDie)
		{
			map.model.monsterKilled++;
			var gem = poolController.GetObject(ItemPrefab.GemExp, mons.transform.position);
			gem.GetComponent<DropItem>().Init(mons.stat.exp.BaseValue, RandomDropItem());
		}

		listMonster.Remove(mons);
		mons.wave.monsterInWave.Remove(mons);
		Singleton<PoolController>.instance.ReturnObject(mons.type, mons.gameObject);
		//Destroy(mons.gameObject);
	}

	public void KillAllMonster()
	{
		foreach(var mob in listMonster.ToList())
		{
			MonsterDie(mob, true);
		}
	}

	public List<Monster> GetAllMonsterInAttackRange()
	{
		var characterPos = character.transform.position;
		//Rect myRect = new Rect(characterPos.x - _width / 2, characterPos.y - _height / 2, _width, _height);
		var listMonsterInRect = new List<Monster>();
		foreach(var mons in listMonster)
		{
			if(CheckTouchCharacter(mons.transform.position, character.model.attackRange))
			{
				listMonsterInRect.Add(mons);
			}
			// if(myRect.Contains(mons.transform.position))
			// {
			// 	listMonsterInRect.Add(mons);
			// }
		}
		return listMonsterInRect;
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

	// ReSharper disable Unity.PerformanceAnalysis
	public void Collected(DropItem dropItem)
	{
		if(dropItem.type == DropItemType.Exp)
		{
			map.model.ExpCurrent += dropItem.value;
			if(map.model.ExpCurrent > map.model.ExpMax)
			{
				map.model.ExpCurrent -= map.model.ExpMax;
				map.model.LevelCharacter++;
				map.model.ExpMax += 50;
				app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
			}
		}
		else
		{
			CollectedItemSpecial(dropItem.type);
		}
	}

	public void CollectedItemSpecial(DropItemType type)
	{
		switch (type)
		{
			case DropItemType.Magnet:
				foreach(var item in poolController.GetPool(ItemPrefab.GemExp).usedList)
				{
					if(item.TryGetComponent(out DropItem dropItemType) && dropItemType.type == DropItemType.Exp)
					{
						dropItemType.Collect();
					}
				}
				break;
			case DropItemType.Food:
				character.AddHealth(character.model.maxHealthPoint * 20 / 100);
				break;
			case DropItemType.Bomb:
				var characterPos = character.transform.position;
				Rect myRect = new Rect(characterPos.x - _width / 2, characterPos.y - _height / 2, _width, _height);
				foreach(var mons in listMonster.ToList())
				{
					if(myRect.Contains(mons.transform.position))
					{
						MonsterDie(mons);
					}
				}
				break;
		}
	}

	public DropItemType RandomDropItem()
	{
		var randomNumber = Random.Range(0f, 100f);
		foreach(var item in _percentDropItem)
		{
			if(randomNumber < item.Value)
			{
				return item.Key;
			}
		}
		return DropItemType.Exp;
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

	private Character SpawnCharacter()
	{
		var characterPrefab = Instantiate(app.resourceManager.GetItemPrefab(ItemPrefab.Character))
			.GetComponent<Character>();
		characterPrefab.transform.position = Vector2.zero;

		_healthBar = Instantiate(app.resourceManager.GetItemPrefab(ItemPrefab.HealthBar), app.resourceManager.rootContainer)
			.GetComponent<HealthBar>();
		_healthBar.Init(characterPrefab);

		var stat = new CharacterStat(100f, 3, 20, 1,7, 50);
		characterPrefab.Init(stat);

		return characterPrefab;
	}

	public bool CheckTouchCharacter(Vector3 trans, float number)
	{
		var x = trans.x - charPos.x;
		var y = trans.y - charPos.y;
		return x * x + y * y <= number * number;
	}

	public bool CheckTouch(Vector3 a, Vector3 b, float number)
	{
		var x = a.x - b.x;
		var y = a.y - b.y;
		return x * x + y * y <= number * number;
	}

    
    private void LoadMap(int chapter)
	{
		_camSize = Camera.main.WorldToViewportPoint(new Vector3(1, 1, 0));
		_width = 1 / (_camSize.x - 0.5f);
		_height = 1 / (_camSize.y - 0.5f);
		Instantiate(app.resourceManager.mapInfinity, Vector3.zero, quaternion.identity).Init(chapter);

		map = app.resourceManager.ShowPopup(PopupType.MainInGame).GetComponent<MapView>();
		map.Init();
		character = SpawnCharacter();
		listMonster.Clear();
		Instantiate(app.resourceManager.GetItemPrefab(ItemPrefab.SupportItem), Vector3.zero, quaternion.identity);
		app.resourceManager.ShowPopup(PopupType.ChoiceSkill);
		//app.analytics.TrackPlay(LevelResult.Start, map.model.levelInGame);
	}

}