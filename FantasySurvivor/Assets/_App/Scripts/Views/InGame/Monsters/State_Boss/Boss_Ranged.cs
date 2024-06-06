using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;
namespace FantasySurvivor
{
    public class Boss_Ranged : Monster
    {
        public Transform firePoint2;

        public BulletMonster bulletPrefab;

        public BulletMonster bulletPrefab2;
        private Vector3 spawnPos { get; set; }

        [SerializeField] private ItemPrefab typeBullet;

        [SerializeField] private ItemPrefab typeBullet2;

        [SerializeField] private int _numberBack = 0;

        private int _coolDownBack = 0;

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
            Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position).TryGetComponent(out BulletMonster bullet);
            bullet.transform.rotation = firePoint.rotation;
            bullet.Init(this);

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
