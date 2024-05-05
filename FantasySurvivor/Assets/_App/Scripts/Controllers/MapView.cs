using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework.MVC;
using FantasySurvivor;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Cooldown = ArbanFramework.Cooldown;
using Random = UnityEngine.Random;

public class MapView : View<GameApp>
{
	public class WaveData
	{
		public string idMonster;
		public int stepTime;
		public int number = 0;
		public int maxNumber;
		public int adMonster;
		public int healthMonster;
		public int expMonster;

		public readonly Cooldown coolDownTime = new Cooldown();

		public List<Monster> monsterInWave = new List<Monster>();
	}

	public Dictionary<TypeItemReward, int> dictionaryReward = new Dictionary<TypeItemReward, int>();

	public List<SkillData> listSkill;

	public MapModel model { get; private set; }

	[SerializeField] private Vector2 _size;

	private int _coinOfLevel = 0;

	private Cooldown _cdEndLevel = new Cooldown();

	private readonly List<WaveData> _listWaveData = new List<WaveData>();
	private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	public void Init()
	{
		model = new();
		listSkill = app.resourceManager.GetListSkill().Where(p => p.canAppear).ToList();
		StartLevel(model.levelInGame);
	}

	public List<SkillData> GetRandomSkill()
	{
		int count = 3;
		List<SkillData> newList = new List<SkillData>();
		if(listSkill.Count < 3)
		{
			count = listSkill.Count;
		}
		for(int i = 0; i < count; i++)
		{
			SkillData skill;
			do
			{
				skill = listSkill[Random.Range(0, listSkill.Count)];
			} while(newList.Contains(skill));
			newList.Add(skill);
		}

		if(listSkill.Count == 0)
		{
			newList.Add(app.resourceManager.GetSkill(SkillName.Food));
		}

		return newList;
	}

	public void RemoveSkill(SkillName skillName)
	{
		foreach(var skill in listSkill.ToList())
		{
			if(skill.name.Equals(skillName))
			{
				listSkill.Remove(skill);
			}
		}
	}

	private void StartLevel(int level)
	{
		var dataChapter = app.configs.dataChapter.GetConfig(level);
		foreach(var wave in dataChapter.waves)
		{
			var waveData = new WaveData
			{
				idMonster = wave.idMonster,
				stepTime = wave.stepTime,
				maxNumber = wave.number,
				adMonster = wave.adMonster,
				healthMonster = wave.healthMonster,
				expMonster = wave.expMonster,
			};

			waveData.coolDownTime.Restart(0);
			_listWaveData.Add(waveData);
		}
		_coinOfLevel = dataChapter.coin;
		_cdEndLevel.Restart(dataChapter.timeEnd);
	}

	private void Update()
	{
		if(gameController.isStop) return;

		var deltaTime = Time.deltaTime;
		
		model.timeInGame += deltaTime;
		
		_cdEndLevel.Update(deltaTime);

		if(_cdEndLevel.isFinished)
		{
			_listWaveData.Clear();
			gameController.AddReward(dictionaryReward, TypeItemReward.Coin, _coinOfLevel);
			model.levelInGame++;
			StartLevel(model.levelInGame);
			return;
		}

		foreach(var wave in _listWaveData.ToList())
		{
			if(wave.number >= wave.maxNumber)
			{
				if(wave.monsterInWave.Count == 0)
				{
					_listWaveData.Remove(wave);
					if(_listWaveData.Count == 0)
					{
						gameController.AddReward(dictionaryReward, TypeItemReward.Coin, _coinOfLevel);
						model.levelInGame++;
						StartLevel(model.levelInGame);
					}
				}
				continue;
			}
			wave.coolDownTime.Update(deltaTime);
			if(wave.coolDownTime.isFinished)
			{
				wave.monsterInWave.Add(gameController.SpawnMonster(wave));
				wave.coolDownTime.Restart(wave.stepTime);
				wave.number++;
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}