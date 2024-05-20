using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSummoner : Monster
{
   
    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    private int numMonster = 3;
    private float range = 2f;



    public override void Attack()
    {
        for (int i = 0; i < numMonster; i++)
        {
            var mob = gameController.SpawnMonster("Skeleton", 10, stat.attackDamage.BaseValue);

            Vector2 randomPosition = new Vector2(firePoint.position.x + Random.Range(-range, range),
                                                 firePoint.position.y + Random.Range(-range, range));

            mob.transform.position = randomPosition;
        }
    }
 
}
