using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework.MVC;
using FantasySurvivor;
using JetBrains.Annotations;
using Unity.Mathematics;
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

    private int _countWavesSpawn = 3;
    public GameObject _spawnedSupportItem = null;
    private int _countWavesRemove;


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

    private void UpdateLevelBoss()
    {
        _isBattleBossing = true;
        foreach (var wave in _listWaveData.ToList())
        {
            ClearCurrentMonsters();
            wave.monsterInWave.Add(gameController.SpawnBoss(wave));
            wave.coolDownTime.Restart(wave.stepTime);
            wave.number++;
        }
    }

    private void UpdateWaveMonster()
    {
        if (_isBattleBossing == true) return;
        foreach (var wave in _listWaveData.ToList())
        {
            if (wave.coolDownTime.isFinished)
            {
                wave.monsterInWave.Add(gameController.SpawnMonster(wave));
                wave.coolDownTime.Restart(wave.stepTime);
                wave.number++;
            }
        }
    }

    private void Update()
    {
        if (gameController.isStop) return;

        var deltaTime = Time.deltaTime;

        model.timeInGame += deltaTime;

        _cdEndLevel.Update(deltaTime);

        if (_cdEndLevel.isFinished)
        {
            if (GetCurrentWave() == 16 && _isBattleBossing == false)
            {
                UpdateLevelBoss();
            }
            else
            {
                _listWaveData.Clear();
                gameController.AddReward(dictionaryReward, TypeItemReward.Coin, _coinOfLevel);
                model.WaveInGame++;

                /*_countWavesSpawn++;
                _countWavesRemove++;

                if (_countWavesSpawn % 2 == 0 && _spawnedSupportItem == null)
                {
                    _spawnedSupportItem = Instantiate(app.resourceManager.GetItemPrefab(ItemPrefab.SupportItem), Vector3.zero, quaternion.identity);
                    _countWavesRemove = 0;
                }

                if (_spawnedSupportItem != null)
                {
                    _countWavesRemove++;
                    if (_countWavesRemove >= 2)
                    {
                        Destroy(_spawnedSupportItem);
                        _spawnedSupportItem = null;
                    }
                }*/

                UpdateLevel();
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

                        _countWavesSpawn--;

                        if (_countWavesSpawn == 0 && _spawnedSupportItem == null)
                        {
                            if (_isBattleBossing) return;
                            _spawnedSupportItem = Instantiate(app.resourceManager.GetItemPrefab(ItemPrefab.SupportItem), Vector3.zero, quaternion.identity);
                            _countWavesRemove = 4;
                            _countWavesSpawn = 3;
                        }

                        if (_spawnedSupportItem != null)
                        {
                            _countWavesRemove--;
                            if (_countWavesRemove == 0)
                            {
                                Destroy(_spawnedSupportItem);
                                _spawnedSupportItem = null;
                                _countWavesSpawn = 3;
                            }
                        }

                        UpdateLevel();
                    }
                }
                continue;
            }

            if (GetCurrentWave() == 16)
            {
                wave.coolDownTime.Update(deltaTime);
                UpdateLevelBoss();
            }
            else
            {
                wave.coolDownTime.Update(deltaTime);
                UpdateWaveMonster();
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
