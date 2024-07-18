using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class HydustShrimp_Boss : Monster
{
    //bullet
    public BulletBossHydustShrimp bulletPrefab1;
    public Transform firePoint2;
    public Transform spawnFirePoint;

    [SerializeField] private ItemPrefab typeBullet;
    [SerializeField] private int _numberBack = 0;


    //zone
    public GameObject zonePrefab;
    private GameObject currentZone;
    public float initialZoneRadius; //size zone
    public float minimumZoneRadius;
    public float shrinkingSpeed; //time thu nho
    private float currentZoneRadius;
    private float damageTimer = 0f; //count time dame zone

    //sight
    public float sightRange = 10f;
    private bool isInSightRange = false;

    private Vector3 spawnPos { get; set; }

    //state atack
    private int _coolDownBack = 0;
    private int attackCounter = 0; // count so lan attack
    private bool isState1;
    private bool isState2;
    private bool isState3;

    private bool isCharging = false;
    private Vector3 chargeDirection;
    private float chargeSpeed = 15f;
    private float chargeDuration = 1f;
    private float chargeElapsedTime = 0f;

    //warning attack
    public GameObject warningPrefab;
    private GameObject warningInstance;
    private float warningDuration = 1.5f;
    private float warningElapsedTime = 0f;
    private bool isWarning = false;

    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        spawnPos = transform.position;
        IdleState();

        currentZone = Instantiate(zonePrefab, transform.position, Quaternion.identity);
        currentZone.transform.localScale = Vector3.one * initialZoneRadius * 2;
        currentZoneRadius = initialZoneRadius;
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

        if (isWarning)
        {
            warningElapsedTime += Time.deltaTime;

            if (warningElapsedTime >= warningDuration)
            {
                Destroy(warningInstance);
                isWarning = false;
                IdleState();
                StartCharging();
            }

            return;
        }

        if (isCharging)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            chargeElapsedTime += Time.deltaTime;

            CheckTouchThirdAttack();

            if (chargeElapsedTime >= chargeDuration)
            {
                isCharging = false;
            }

            return;
        }

        if (moveDirection.magnitude > 15)
        {
            isState1 = false;
            isState2 = false;

            IdleState();
            ThirdAttack();
            cdAttack.Restart(1 / model.attackSpeed);
            return;
        }

        //sizeAttack
        if (moveDirection.magnitude < sizeAttack)
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
                    animator.SetBool("Attack", false);
                    attackCounter = 0;
                    isState2 = true;
                    cdAttack.Restart(1 / model.attackSpeed);
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
                        cdAttack.Restart(2 / model.attackSpeed);
                        return;
                    }
                }
            }
        }
        else
        {
            MoveState();
            animator.SetBool("Attack", false);
            animator.SetBool("SummonAttack", false);
            attackCounter = 0;
        }

        SetAnimation(idleDirection);

    }

    public virtual bool IsSightRange()
    {
        return Vector3.Distance(transform.position, gameController.character.transform.position) <= sightRange;
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

    private void SecondAttack()
    {
        animator.SetBool("SummonAttack", true);
        SpawnBomb();
    }

    private void SpawnBomb()
    {
        int numMonster = 3;
        float range = 2f;

        for (int i = 0; i < numMonster; i++)
        {
            var mob = gameController.SpawnMonster("CrabBomber", 5, stat.attackDamage.BaseValue);

            Vector2 randomPosition = new Vector2(spawnFirePoint.transform.position.x + Random.Range(-range, range),
                                                 spawnFirePoint.transform.position.y + Random.Range(-range, range));

            mob.transform.position = randomPosition;
        }
    }

    private void ThirdAttack()
    {
        isWarning = true;
        warningElapsedTime = 0f;

        chargeDirection = (gameController.character.transform.position - transform.position).normalized;

        float warningOffsetDistance = 8f;
        Vector3 warningPosition = transform.position + chargeDirection * warningOffsetDistance;
        warningInstance = Instantiate(warningPrefab, warningPosition, Quaternion.identity);

        float angle = Mathf.Atan2(chargeDirection.y, chargeDirection.x) * Mathf.Rad2Deg;
        warningInstance.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void StartCharging()
    {
        isCharging = true;
        chargeElapsedTime = 0f;
    }

    private void CheckTouchThirdAttack()
    {
        var characterPosition = gameController.character.transform.position;
        var x = transform.position.x - characterPosition.x;
        var y = transform.position.y - characterPosition.y;
        float distance = x * x + y * y;
        var sizeTotal = size + gameController.character.sizeBase;

        if (distance <= sizeTotal * sizeTotal)
        {
            gameController.character.TakeDamage(20);

            isCharging = false;
            IdleState();
        }
    }


    private bool CheckBack()
    {
        return _coolDownBack >= _numberBack;
    }

}
