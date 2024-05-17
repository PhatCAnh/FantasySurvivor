using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummoner : Monster
{

    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    public override void Attack()
    {
        var mob = gameController.SpawnMonster("M1", model.attackDamage, Mathf.RoundToInt(model.maxHealthPoint), 0);
        mob.transform.position = transform.position;
    
    
    }

}
