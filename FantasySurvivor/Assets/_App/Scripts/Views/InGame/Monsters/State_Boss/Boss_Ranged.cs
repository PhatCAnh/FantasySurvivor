using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.StateMachine;
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

        private int _coolDownBack = 0;

        private StateMachine _stateMachine;

        private BossIdle _idleState;

        private BossMove _moveState;

        private BossAttack _attackState;

        protected override void OnViewInit()
        {
            base.OnViewInit();
            spawnPos = transform.position;
        }

        private bool CheckBack()
        {
            return _coolDownBack >= _numberBack;
        }


        public override void Attack()
        {
            animator.SetBool("Attack", true);

            int bulletCount = 8; // Số lượng đạn trong vòng tròn
            float radius = 1f; // Bán kính của vòng tròn đạn

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount;
                Vector3 bulletPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                Vector3 spawnPosition = firePoint.position + bulletPosition;

                Singleton<PoolController>.instance.GetObject(typeBullet, spawnPosition).TryGetComponent(out BulletBossGatlingCrab bullet);
                bullet.transform.position = spawnPosition;
                bullet.Init(this);
            }

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * Mathf.PI * 2 / bulletCount;
                Vector3 bulletPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
                Vector3 spawnPosition = firePoint2.position + bulletPosition;

                Singleton<PoolController>.instance.GetObject(typeBullet2, spawnPosition).TryGetComponent(out BulletBossGatlingCrab bullet2);
                bullet2.transform.position = spawnPosition;
                bullet2.transform.rotation = Quaternion.Euler(10, 1, angle * Mathf.Rad2Deg); // Xoay viên đạn theo góc
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


        public virtual void Boss_IdleState()
        {
            if (isIdle) return;
            _stateMachine.ChangeState(_idleState);
        }

        public virtual void Boss_MoveState()
        {
            flip();
            if (isMove) return;
            _stateMachine.ChangeState(_moveState);
        }

        public virtual void Boss_AttackState()
        {
            flip();
            if (isAttack) return;
            _stateMachine.ChangeState(_attackState);
            IdleState();
        }

    }
}
