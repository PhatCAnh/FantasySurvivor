using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;

public class LevelTutorial : View<GameApp>
{
    public GameObject hand;

    private int _numberHand = 0;
    
    private GameController gameController => Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        Singleton<PoolGemExp>.instance.onSpawnGemExp += OnMonsterDie;
    }

    private void OnMonsterDie(GemExp gemExp)
    {
        Instantiate(hand, gemExp.transform.position, Quaternion.identity);
        _numberHand++;
        if(_numberHand >= 10)
        {
            Singleton<PoolGemExp>.instance.onSpawnGemExp -= OnMonsterDie;
        }
    }
}
