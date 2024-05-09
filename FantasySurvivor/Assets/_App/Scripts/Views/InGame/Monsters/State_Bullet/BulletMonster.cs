using System;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace FantasySurvivor
{
	public class BulletMonster : View<GameApp>
	{
		public SpriteRenderer skin;

		public Rigidbody2D rigidbody2d;
		
		[SerializeField] protected GameObject deadEffect;

		[SerializeField] private float _speedBullet = 15f;

		private Monster _origin;
		private Vector2 _directionToTarget;
		private Character _character => gameController.character;

		
		protected GameController gameController => Singleton<GameController>.instance;

		public void Init(Monster origin)
		{
            _origin = origin;
            var spawnPos = _origin.firePoint;
            transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);

             _directionToTarget = (origin.target.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        protected void FixedUpdate()
		{
			if(gameController.isStop) return;
            //transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _speedBullet * Time.deltaTime);
            var position = transform.position;
            position = Vector2.MoveTowards(position, position + _directionToTarget.normalized, _speedBullet * Time.deltaTime);
            transform.position = position;
		}

        protected void Touch()
		{
			if(deadEffect != null)
			{
				Instantiate(deadEffect, transform.position, transform.rotation);
			}
            
            Destroy(gameObject);
		}

        private void Update()
		{
			if(gameController.CheckTouchCharacter(transform.position, 0.25f + _character.sizeBase))
            {
                _character.TakeDamage(_origin.model.attackDamage);
                Touch();
            }
        }
	}
}