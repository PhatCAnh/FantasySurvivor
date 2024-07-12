using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class HydustShrimp_Boss : Boss_Ranged
{

    [SerializeField] private ItemPrefab typeBullet;
    [SerializeField] private ItemPrefab typeBullet2;
    [SerializeField] private int _numberBack = 0;


    private Vector3 spawnPos { get; set; }

    private int _coolDownBack = 0;
    private int attackCounter = 0; // count so lan attack
    private bool isState1 = false;
    private bool isState2 = false;
    private bool isState3 = false;



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
                        SecondAttack();
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
        animator.SetBool("Attack", true);

        float coneAngle = 40f;
        int bulletsAmount = 4;
        float angleStep = coneAngle / (bulletsAmount - 1);

        Vector2 directionToCharacter = (gameController.character.transform.position - transform.position).normalized;

        float baseAngle = Mathf.Atan2(directionToCharacter.y, directionToCharacter.x) * Mathf.Rad2Deg;
        float startAngle = baseAngle - (coneAngle / 2);

        for (int i = 0; i < bulletsAmount; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            float bulDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float bulDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 bulMoveDirection = new Vector2(bulDirX, bulDirY).normalized;
            var bulletObject = ArbanFramework.Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position);

            if (bulletObject.TryGetComponent(out BulletBossHydustShrimp bullet))
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                bullet.Init(this);
                bullet.SetDirection(bulMoveDirection);
            }
        }

        for (int i = 0; i < bulletsAmount; i++)
        {
            float currentAngle = startAngle + (i * angleStep);
            float bulDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);
            float bulDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 bulMoveDirection = new Vector2(bulDirX, bulDirY).normalized;

            var bulletObject = ArbanFramework.Singleton<PoolController>.instance.GetObject(typeBullet, firePoint2.position);

            if (bulletObject.TryGetComponent(out BulletBossHydustShrimp bullet2))
            {
                bullet2.transform.position = firePoint2.position;
                bullet2.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
                bullet2.Init(this);
                bullet2.SetDirection(bulMoveDirection);
            }
        }

        _coolDownBack++;
        if (_numberBack != 0)
        {
            if (CheckBack())
            {
                moveTarget = spawnPos;
            }
        }
    }

    public override void SecondAttack()
    {
       
    }

    public virtual void ThirdAttack()
    {

    }


}
