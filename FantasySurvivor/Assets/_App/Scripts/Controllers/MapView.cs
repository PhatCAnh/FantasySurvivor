using System.Collections.Generic;
using System.Linq;
using ArbanFramework.MVC;
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
		public SkinMonsterType skinMonsterType;
		public MonsterType monsterType;
		public int levelMonster;
		public int coinMonster;

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
		for(int i = 0; i < dataChapter.waves.Length; i++)
		{
			var wave = dataChapter.waves[i];
			var waveData = new WaveData
			{
				duration = wave.duration,
				coolDown = wave.coolDown,
				skinMonsterType = wave.skinMonsterType,
				monsterType = wave.monsterType,
				levelMonster = wave.levelMonster,
				coinMonster = wave.coinMonster,
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
				gameController.SpawnMonster(wave.skinMonsterType, wave.monsterType, wave.levelMonster, wave.coinMonster);
				wave.coolDownTime.Restart(wave.coolDown);
			}
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube(transform.position, _size);
	}
}
