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
	private class WaveData
	{
		public int duration;
		public int coolDown;
		public MonsterType monsterType;
		public int adMonster;
		public int healthMonster;
		public int coinMonster;
		public int expMonster;

		public readonly Cooldown coolDownTime = new Cooldown();
		public readonly Cooldown cooldownEnd = new Cooldown();
	}
	
	public MapModel model { get; private set; }
	
	[SerializeField] private Vector2 _size;

	private readonly List<WaveData> _listWaveData = new List<WaveData>();
	private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	public void Init(int chapter)
	{
		model = new();
		
		var dataChapter = app.configs.dataChapterConfigTable.GetConfig(chapter);
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
		
			waveData.coolDownTime.Restart(wave.timeStart);
			if(waveData.duration != -1)
			{
				waveData.cooldownEnd.Restart(wave.duration);
			}
			_listWaveData.Add(waveData);
		}
	}
	
	private void Update()
	{
		if(gameController.isStop) return;

		model.timeInGame += Time.deltaTime;
		
		foreach(var wave in _listWaveData.ToList())
		{
			if(wave.duration != -1)
			{
				wave.cooldownEnd.Update(Time.deltaTime);
				if(wave.cooldownEnd.isFinished)
				{
					_listWaveData.Remove(wave);
					continue;
				}
			}
			
			wave.coolDownTime.Update(Time.deltaTime);
			if(wave.coolDownTime.isFinished)
			{
				gameController.SpawnMonster(wave.monsterType,  wave.healthMonster, wave.adMonster, wave.coinMonster, wave.expMonster);
				wave.coolDownTime.Restart(wave.coolDown);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}
