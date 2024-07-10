using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydustShrimp_Boss : Monster
{
    public BulletBossHydustShrimp bulletPrefab;
    public Transform firePoint2;
    private Vector3 spawnPos { get; set; }

    [SerializeField] private ItemPrefab typeBullet;
    [SerializeField] private ItemPrefab typeBullet2;
    [SerializeField] private int _numberBack = 0;

    private GameController gameController => Singleton<GameController>.instance;

    private int _coolDownBack = 0;
    private int attackCounter = 0; // count so lan attack
    private bool isState1 = false;
    private bool isState2 = false;
    private bool isState3 = false;

    private float sightRange = 10f;
    private bool isInSightRange = false;


    public GameObject zonePrefab;
    private GameObject currentZone;
    public float initialZoneRadius; //size zone
    public float minimumZoneRadius;
    public float shrinkingSpeed; //time thu nho
    private float currentZoneRadius;
    private float damageTimer = 0f; //count time dame zone


    protected override void OnViewInit()
    {
        base.OnViewInit();
        spawnPos = transform.position;
        IdleState();

        currentZone = Instantiate(zonePrefab, transform.position, Quaternion.identity);
        currentZone.transform.localScale = Vector3.one * initialZoneRadius * 2;
        currentZoneRadius = initialZoneRadius;

    }

    private bool IsSightRange()
    {
        return Vector3.Distance(transform.position, gameController.character.transform.position) <= sightRange;
    }

    private bool CheckBack()
    {
        return _coolDownBack >= _numberBack;
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
                        AttackState();
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

    private void UpdateZone()
    {
        if (currentZoneRadius > 0)
        {
            currentZoneRadius -= shrinkingSpeed * Time.deltaTime;
            currentZoneRadius = Mathf.Max(currentZoneRadius, minimumZoneRadius);

            currentZone.transform.position = spawnPos;
            currentZone.transform.localScale = Vector3.one * currentZoneRadius * 2;


        }

        damageTimer += Time.deltaTime;

        if (damageTimer >= 0.5f)
        {
            float distanceToPlayer = Vector3.Distance(spawnPos, gameController.character.transform.position);
            if (distanceToPlayer > currentZoneRadius)
            {
                gameController.character.TakeDamage(10);
            }
            damageTimer = 0f;
        }
    }

    public override void Attack()
    {

    }

    private void SecondAttack()
    {

    }


}
