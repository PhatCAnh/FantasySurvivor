using _App.Scripts.Controllers;
using ArbanFramework;

using ArbanFramework.StateMachine;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
namespace FantasySurvivor
{
    public class Boss_Ranged : Monster
    {
        public BulletBossGatlingCrab bulletPrefab;
        public BulletBossGatlingCrab bulletPrefab2;
        public BulletBossGatlingCrab bulletPrefab3;
        public Transform firePoint2;
        public Transform firePoint3;
        public GameObject telegraphEffectPrefab;
        private GameObject currentTelegraphEffect;

        private Vector3 spawnPos { get; set; }
        private Vector3 markedPosition;

        [SerializeField] private ItemPrefab typeBullet;
        [SerializeField] private ItemPrefab typeBullet2;
        [SerializeField] private int _numberBack = 0;

        private GameController gameController => Singleton<GameController>.instance;

        private int _coolDownBack = 0;
        private int attackCounter = 0; // count so lan attack
        private bool isState1 = false;
        private bool isState2 = false;
        private bool isState3 = false;
        private bool isTelegraphing = false;
        private float telegraphTimer = 0f;
        private float telegraphDuration = 1.5f;

        public float sightRange = 10f;
        public bool isInSightRange = false;


        public GameObject zonePrefab;
        private GameObject currentZone;
        public float initialZoneRadius; //size zone
        public float minimumZoneRadius;
        public float shrinkingSpeed; //time thu nho
        private float currentZoneRadius;
        private float damageTimer = 0f; //count time dame zone
        private Vector3 initialZonePosition;


        protected override void OnViewInit()
        {
            base.OnViewInit();
            spawnPos = transform.position;
            IdleState();
            //InitializeZone();
        }

        public virtual bool IsSightRange()
        {
            return Vector3.Distance(transform.position, gameController.character.transform.position) <= sightRange;
        }

        public virtual bool CheckBack()
        {
            return _coolDownBack >= _numberBack;
        }

        protected override void HandlePhysicUpdate()
        {
            if (isDead) return;

            spawnPos = transform.position;
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
                    isInSightRange = false;
                    IdleState();
                    return;
                }
            }

            if (isTelegraphing)
            {
                isState3 = true;
                IdleState();
                telegraphTimer -= Time.deltaTime;

                if (telegraphTimer <= 0f)
                {
                    IdleState();
                    transform.position = markedPosition;


                    if (currentTelegraphEffect != null)
                    {
                        Destroy(currentTelegraphEffect);
                        currentTelegraphEffect = null;
                    }

                    TelegraphingAttack();
                    cdAttack.Restart(2 / model.attackSpeed);
                    isTelegraphing = false;
                    isState3 = false;
                    isState2 = false;
                }
                return;
            }

            if (moveDirection.magnitude > 20 && !isTelegraphing)
            {
                isState1 = false;
                isState2 = false;
                markedPosition = gameController.character.transform.position;
                Vector3 telegraphPosition = gameController.character.transform.position;

                if (telegraphEffectPrefab != null)
                {

                    if (currentTelegraphEffect != null)
                    {
                        Destroy(currentTelegraphEffect);
                    }

                    currentTelegraphEffect = Instantiate(telegraphEffectPrefab, telegraphPosition, Quaternion.identity);

                }

                telegraphTimer = telegraphDuration;
                isTelegraphing = true;
                return;
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

        private void InitializeZone()
        {
            if (currentZone != null)
            {
                Destroy(currentZone);
            }

            if (zonePrefab != null)
            {
                currentZone = Instantiate(zonePrefab, transform.position, Quaternion.identity);
                initialZonePosition = transform.position;
                currentZone.transform.localScale = Vector3.one * initialZoneRadius * 2;
                currentZoneRadius = initialZoneRadius;
            }
        }

        private void UpdateZone()
        {
            if (isDead) return;

            if (currentZone == null)
            {
                InitializeZone();
            }
            else
            {

                if (currentZoneRadius > 0)
                {
                    currentZoneRadius -= shrinkingSpeed * Time.deltaTime;
                    currentZoneRadius = Mathf.Max(currentZoneRadius, minimumZoneRadius);

                    currentZone.transform.position = initialZonePosition;
                    currentZone.transform.localScale = Vector3.one * currentZoneRadius * 2;
                }

                damageTimer += Time.deltaTime;

                if (damageTimer >= 0.5f)
                {
                    float distanceToPlayer = Vector3.Distance(initialZonePosition, gameController.character.transform.position);
                    if (distanceToPlayer > currentZoneRadius)
                    {
                        gameController.character.TakeDamage(10);
                    }
                    damageTimer = 0f;
                }
            }

        }


        public override void Attack()
        {
            animator.SetBool("Attack", true);

            float coneAngle = 70f;
            int bulletsAmount = 7;
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
                var bulletObject = Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position);

                if (bulletObject.TryGetComponent(out BulletBossGatlingCrab bullet))
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

                var bulletObject = Singleton<PoolController>.instance.GetObject(typeBullet2, firePoint.position);

                if (bulletObject.TryGetComponent(out BulletBossGatlingCrab bullet2))
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


        private void AroundAttack(float damageDistance)
        {
            float distanceToCharacter = Vector3.Distance(transform.position, gameController.character.transform.position);

            if (distanceToCharacter <= damageDistance)
            {
                gameController.character.TakeDamage(model.attackDamage);
            }
        }

        private void TelegraphingAttack()
        {
            animator.SetBool("Attack", true);
            int bulletCount = 20;
            float radius = 1f;
            Vector2 directionToCharacter = (gameController.character.transform.position - transform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToCharacter.y, directionToCharacter.x) * Mathf.Rad2Deg;

            for (int i = 0; i < bulletCount; i++)
            {

                float angle = baseAngle + i * 360f / bulletCount;

                float bulDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector3 bulletPosition = firePoint3.position + new Vector3(bulDirX * radius, bulDirY * radius, 0f);

                var bulletObject = Singleton<PoolController>.instance.GetObject(typeBullet, firePoint3.position);

                if (bulletObject.TryGetComponent(out BulletBossGatlingCrab bullet3))
                {
                    bullet3.transform.position = bulletPosition;
                    bullet3.transform.rotation = Quaternion.Euler(0, 0, angle);
                    bullet3.Init(this);

                    Vector2 bulMoveDirection = new Vector2(bulDirX, bulDirY).normalized;
                    bullet3.SetDirection(bulMoveDirection);
                }
            }

            AroundAttack(5);

            _coolDownBack++;
            if (_numberBack != 0)
            {
                if (CheckBack())
                {
                    moveTarget = spawnPos;
                }
            }
        }


        public virtual void SecondAttack()
        {
            animator.SetBool("Attack", true);
            int bulletCount = 10;
            float radius = 1.5f;

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount;
                Vector3 bulletPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                Vector3 spawnPosition = firePoint.position + bulletPosition;

                Singleton<PoolController>.instance.GetObject(typeBullet, spawnPosition).TryGetComponent(out BulletBossGatlingCrab bullet);
                bullet.transform.position = spawnPosition;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

                bullet.Init(this);
            }

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount;
                Vector3 bulletPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                Vector3 spawnPosition = firePoint2.position + bulletPosition;

                Singleton<PoolController>.instance.GetObject(typeBullet, spawnPosition).TryGetComponent(out BulletBossGatlingCrab bullet2);
                bullet2.transform.position = spawnPosition;
                bullet2.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

                bullet2.Init(this);
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

        public override void Die(bool selfDie = true)
        {
            if (currentZone != null)
            {
                Destroy(currentZone);
                currentZone = null;
            }

            model.currentHealthPoint = 0;
            listStatusEffect.Clear();
            animator.SetBool("Dead", true);
            gameController.BossDie(this);

            isInSightRange = false;
        }

    }
}
