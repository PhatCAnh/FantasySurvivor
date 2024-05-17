using _App.Scripts.Controllers;
using ArbanFramework;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
namespace FantasySurvivor
{
	public class RangedMonster : Monster
	{

		public BulletMonster bulletPrefab;

        private Vector3 spawnPos { get; set; }

        [SerializeField] private ItemPrefab typeBullet;

		[SerializeField] private int _numberBack = 0;

		private int _coolDownBack = 0;
        private GameController gameController => Singleton<GameController>.instance;

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
           
            // Tạo một instance mới của viên đạn từ bulletPrefab
           Singleton<PoolController>.instance.GetObject(typeBullet, firePoint.position).TryGetComponent(out BulletMonster bullet);
            bullet.transform.rotation = firePoint.rotation;
            // Khởi tạo viên đạn
            bullet.Init(this);

            _coolDownBack++;
			if(_numberBack != 0)
			{
				if(CheckBack())
				{
					moveTarget = spawnPos;
				}
			}
		}
	}
}