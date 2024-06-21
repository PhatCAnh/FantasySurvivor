using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework.MVC;
using FantasySurvivor;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;
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

    public MapModel model { get; private set; }

    public int currentLevel = 0;

    [SerializeField] private Vector2 _size;

    private int _coinOfLevel = 0;

    private ControlWaveeConfig[] _dataLevelArr;

    private Cooldown _cdEndLevel = new Cooldown();

    private readonly List<WaveData> _listWaveData = new List<WaveData>();
    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    private Monster monster => ArbanFramework.Singleton<Monster>.instance;

    private bool _isBattleBossing = false;

    public void Init(int chapter, int level)
    {
        model = new();
        //fix it
        StartLevel(chapter, level);

    }



    public void StartLevel(int chapter, int level)
    {
        _dataLevelArr = app.configs.dataChapter.GetConfigLevel(chapter, level);
        UpdateLevel();
    }
    private void UpdateLevel()
    {
        var dataLevel = _dataLevelArr[model.WaveInGame - 1];
        var dataWave = dataLevel.waves;
        foreach (var wave in dataWave)
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
        _coinOfLevel = dataLevel.coin;
        _cdEndLevel.Restart(dataLevel.timeEnd);
    }

    private void Update()
    {
        if (gameController.isStop) return;
        if (_isBattleBossing) return;

        var deltaTime = Time.deltaTime;

        model.timeInGame += deltaTime;

        _cdEndLevel.Update(deltaTime);


        /*if (GetCurrentWave() == 21)
		{
            foreach (var item in gameController.listMonster.ToList())
            {
                item.Die(true);
            }
            _isBattleBossing = true;
			foreach (var item in gameController.listMonster.ToList())
			{
				item.Die(true);
			}
			gameController.SpawnMonster(wave);
            return;
        }*/

        if (_cdEndLevel.isFinished)
        {
            _listWaveData.Clear();
            gameController.AddReward(dictionaryReward, TypeItemReward.Coin, _coinOfLevel);
            model.WaveInGame++;
            UpdateLevel();
            Debug.Log("Round bat dau" + GetCurrentWave());

            if (GetCurrentWave() == 3)
            {
                _isBattleBossing = true;
                Debug.Log("Round" + GetCurrentWave());
                foreach (var wave in _listWaveData.ToList())
                {
                    ClearCurrentMonsters();
                    wave.monsterInWave.Add(gameController.SpawnBoss(wave));
                    wave.coolDownTime.Restart(wave.stepTime);
                    wave.number++;
                }
            }
            return;
        }

        foreach (var wave in _listWaveData.ToList())
        {
            if (wave.number >= wave.maxNumber)
            {
                if (wave.monsterInWave.Count == 0)
                {
                    _listWaveData.Remove(wave);
                    if (_listWaveData.Count == 0)
                    {
                        gameController.AddReward(dictionaryReward, TypeItemReward.Coin, _coinOfLevel);
                        model.WaveInGame++;
                        UpdateLevel();
                    }
                }
                continue;
            }

            wave.coolDownTime.Update(deltaTime);
            if (wave.coolDownTime.isFinished)
            {
                if (_isBattleBossing) return;
                wave.monsterInWave.Add(gameController.SpawnMonster(wave));
                wave.coolDownTime.Restart(wave.stepTime);
                wave.number++;
            }
        }
    }

    private void ClearCurrentMonsters()
    {
        var monstersToClear = gameController.listMonster.ToList();

        foreach (var mons in monstersToClear)
        {
            gameController.MonsterDie(mons);
        }

        _listWaveData.Clear();
    
    }

    public int GetCurrentWave()
    {
        return model.WaveInGame;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, _size);
    }
}
