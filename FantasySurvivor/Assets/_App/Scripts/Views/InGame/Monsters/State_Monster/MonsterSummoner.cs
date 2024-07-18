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

    private int numMonster = 1;
    private float range = 2f;

    protected override void HandlePhysicUpdate()
    {
        if (isDead) return;
        moveTarget = gameController.character.transform.position;
        moveDirection = moveTarget - transform.position;

        if (moveDirection.magnitude < sizeAttack) //|| !cdAttack.isFinished)
        {
            if (cdAttack.isFinished)
            {
                AttackState();
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
            animator.SetBool("Attack", false);
        }
        SetAnimation(idleDirection);
    }

    private void Summon1()
    {
        for (int i = 0; i < numMonster; i++)
        {
            var mon1 = gameController.SpawnMonster("LasirdBlue", 5, 10);

            Vector2 randomPosition1 = new Vector2(firePoint.position.x + Random.Range(-range, range),
                                                 firePoint.position.y + Random.Range(-range, range));

            mon1.transform.position = randomPosition1;
        }
    }

    private void Summon2()
    {
        for (int i = 0; i < numMonster; i++)
        {
            var mon2 = gameController.SpawnMonster("LasirdGreen", 5, 10);

            Vector2 randomPosition2 = new Vector2(firePoint.position.x + Random.Range(-range, range),
                                                 firePoint.position.y + Random.Range(-range, range));

            mon2.transform.position = randomPosition2;
        }
    }

    private void Summon3()
    {
        for (int i = 0; i < numMonster; i++)
        {
            var mon3 = gameController.SpawnMonster("LasirdOrange", 5, 10);

            Vector2 randomPosition3 = new Vector2(firePoint.position.x + Random.Range(-range, range),
                                                 firePoint.position.y + Random.Range(-range, range));

            mon3.transform.position = randomPosition3;
        }
    }
    public override void Attack()
    {
        animator.SetBool("Attack", true);

        Summon1();
        Summon2();
        Summon3();
    }

}
