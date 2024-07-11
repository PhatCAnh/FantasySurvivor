using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HydustShrimp_Boss : Boss_Ranged
{
    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
    }

    protected override void HandlePhysicUpdate()
    {
        if (isDead) return;

        moveTarget = gameController.character.transform.position;
        moveDirection = moveTarget - transform.position;

        UpdateZone();

        if (!isInSightRange)
        {
            if (IsSightRange())
            {
                isInSightRange = true;
            }
            else
            {
                IdleState();
                return;
            }
        }

        //sizeAttack
        if (moveDirection.magnitude < sizeAttack && !isState3)
        {
            if (cdAttack.isFinished)
            {
                if (attackCounter < 5)
                {
                    if (!isState2)
                    {
                        isState1 = true;
                        AttackState();
                        MoveState();
                        attackCounter++;
                        cdAttack.Restart(1 / model.attackSpeed);
                    }
                }
                else
                {
                    attackCounter = 0;
                    isState2 = true;
                    cdAttack.Restart(2 / model.attackSpeed);
                    IdleState();
                }

                if (isState2 && cdAttack.isFinished)
                {

                    if (attackCounter < 3)
                    {
                        isState2 = true;
                        SecondAttack();
                        attackCounter++;
                        cdAttack.Restart(1 / model.attackSpeed);
                    }
                    else
                    {
                        isState2 = false;
                        attackCounter = 0;
                        cdAttack.Restart(1 / model.attackSpeed);
                        return;
                    }
                }
            }
        }
        else
        {
            MoveState();
            animator.SetBool("Attack", false);
            attackCounter = 0;
        }



      

        SetAnimation(idleDirection);

    }

    public override void Attack()
    {

    }

    public override void SecondAttack()
    {

    }


}
