using System.Collections.Generic;
using System.Linq;
using ArbanFramework.MVC;
using DataConfig;
using FantasySurvivor;
using Unity.VisualScripting;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

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

	public MapModel model { get; private set; }

	[SerializeField] private Vector2 _size;

	private int _currentLevel = 1;

	private readonly List<WaveData> _listWaveData = new List<WaveData>();
	private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	public void Init()
	{
		model = new();
		StartLevel(_currentLevel);
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

			waveData.coolDownTime.Restart(wave.timeStart);
			_listWaveData.Add(waveData);
		}
	}

	private void Update()
	{
		if(gameController.isStop) return;

		model.timeInGame += Time.deltaTime;

		foreach(var wave in _listWaveData.ToList())
		{
			if(wave.number >= wave.maxNumber)
			{
				if(wave.monsterInWave.Count == 0)
				{
					_listWaveData.Remove(wave);
					if(_listWaveData.Count == 0)
					{
						_currentLevel++;
						StartLevel(_currentLevel);
					}
				}
				continue;
			}
			wave.coolDownTime.Update(Time.deltaTime);
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