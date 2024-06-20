using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSummoner : Monster
{

    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    private int numMonster = 3;
    private float range = 2f;



    protected override void HandlePhysicUpdate()
    {
        if (isDead) return;

        moveTarget = gameController.character.transform.position;
        moveDirection = moveTarget - transform.position;

        if (moveDirection.magnitude < sizeAttack || !cdAttack.isFinished)
        {
            if (cdAttack.isFinished)
            {
                Attack();
                cdAttack.Restart(1 / model.attackSpeed);
            }
            else
            {
                IdleState();
                animator.SetBool("Attack", false);
            }
        }
        else if (moveDirection.magnitude > 25)
        {
            transform.position = gameController.RandomPositionSpawnMonster(20);
        }
        else
        {
            MoveState();
        }
        SetAnimation(idleDirection);
    }

    public override void Attack()
    {
        animator.SetBool("Attack", true);
        for (int i = 0; i < numMonster; i++)
        {
            var mob = gameController.SpawnMonster("Wolf", 5, stat.attackDamage.BaseValue);

            Vector2 randomPosition = new Vector2(firePoint.position.x + Random.Range(-range, range),
                                                 firePoint.position.y + Random.Range(-range, range));

            mob.transform.position = randomPosition;
        }
    }

}
