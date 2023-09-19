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
		public StateWave state;
		public int duration;
		public int coolDown;
		public MonsterType monsterType;
		public int adMonster;
		public int healthMonster;
		public int coinMonster;
		public int expMonster;

		public readonly Cooldown coolDownTime = new Cooldown();
		public readonly Cooldown cooldownEnd = new Cooldown();

		public List<Monster> monsterInWave;

		public enum StateWave
		{
			Waiting,
			Running,
			Ended,
		}
	}

	public MapModel model { get; private set; }

	[SerializeField] private Vector2 _size;

	private readonly List<WaveData> _listWaveData = new List<WaveData>();
	private readonly List<WaveData> _listWaveDataInfinity = new List<WaveData>();
	private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	public void Init(int chapter)
	{
		model = new();

		var dataChapter = app.configs.dataChapter.GetConfig(chapter);
		foreach(var wave in dataChapter.waves)
		{
			var waveData = new WaveData
			{
				duration = wave.duration,
				coolDown = wave.coolDown,
				monsterType = wave.monsterType,
				adMonster = wave.adMonster,
				healthMonster = wave.healthMonster,
				coinMonster = wave.coinMonster,
				expMonster = wave.expMonster,
			};

			waveData.monsterInWave = new List<Monster>();
			waveData.state = WaveData.StateWave.Waiting;
			waveData.coolDownTime.Restart(wave.timeStart);
			waveData.cooldownEnd.Restart(wave.duration);
			if(waveData.duration != -1)
			{

				_listWaveData.Add(waveData);
			}
			else
			{
				_listWaveDataInfinity.Add(waveData);
			}

		}
	}

	private void Update()
	{
		if(gameController.isStop) return;

		model.timeInGame += Time.deltaTime;
		
		foreach(var wave in _listWaveDataInfinity.ToList())
		{
			wave.coolDownTime.Update(Time.deltaTime);
			if(wave.coolDownTime.isFinished)
			{
				SpawnMonsterInWave(wave);

			}
		}

		foreach(var wave in _listWaveData.ToList())
		{
			if(wave.state == WaveData.StateWave.Ended) continue;
				wave.cooldownEnd.Update(Time.deltaTime);
			if(wave.cooldownEnd.isFinished)
			{
				if(wave.monsterInWave.Count == 0)
				{
					wave.state = WaveData.StateWave.Ended;
					if(_listWaveData.All(data => data.state == WaveData.StateWave.Ended))
					{
						gameController.WinGame();
					}
				}
				continue;
			}

			wave.coolDownTime.Update(Time.deltaTime);
			if(wave.coolDownTime.isFinished)
			{
				SpawnMonsterInWave(wave);
			}
		}
	}

	private void SpawnMonsterInWave(WaveData wave)
	{
		wave.state = WaveData.StateWave.Running;
		wave.monsterInWave.Add(gameController.SpawnMonster(wave));
		wave.coolDownTime.Restart(wave.coolDown);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}