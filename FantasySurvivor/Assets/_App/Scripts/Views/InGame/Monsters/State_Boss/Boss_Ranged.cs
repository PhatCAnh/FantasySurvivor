using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.StateMachine;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.Timeline;
using UnityEngine;
namespace FantasySurvivor
{
    public class Boss_Ranged : Monster
    {
        public BulletBossGatlingCrab bulletPrefab;

        public BulletBossGatlingCrab bulletPrefab2;

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

        private bool isStateAttack = false;

        protected override void OnViewInit()
        {
            base.OnViewInit();
            spawnPos = transform.position;
        }

        private bool CheckBack()
        {
            return _coolDownBack >= _numberBack;
        }

        protected override void HandlePhysicUpdate()
        {
            if (isAlive) return;

            moveTarget = gameController.character.transform.position;
            moveDirection = moveTarget - transform.position;

            //tele
            if (moveDirection.magnitude > 20)
            {
                transform.position = moveTarget;
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
                        cdAttack.Restart(3 / model.attackSpeed);
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

                var bulletObject = Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position);

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

        private void SecondAttack()
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

            //logic bullet new
            /*animator.SetBool("Attack", true);
            int bulletCount = 8;
            float radius = 1.5f; 
            Vector2 directionToCharacter = (gameController.character.transform.position - transform.position).normalized;
            float baseAngle = Mathf.Atan2(directionToCharacter.y, directionToCharacter.x) * Mathf.Rad2Deg;

           for (int i = 0; i < bulletCount; i++)
            {
                
                float angle = baseAngle + i * 360f / bulletCount;

                float bulDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float bulDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector3 bulletPosition = firePoint.position + new Vector3(bulDirX * radius, bulDirY * radius, 0f);

                var bulletObject = Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position);

                if (bulletObject.TryGetComponent(out BulletBossGatlingCrab bullet))
                {
                    bullet.transform.position = bulletPosition;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                    bullet.Init(this);

                    Vector2 bulMoveDirection = new Vector2(bulDirX, bulDirY).normalized;
                    bullet.SetDirection(bulMoveDirection);
                }
            }
            }*/

            _coolDownBack++;
            if (_numberBack != 0)
            {
                if (CheckBack())
                {
                    moveTarget = spawnPos;
                }
            }
        }
    }
}
