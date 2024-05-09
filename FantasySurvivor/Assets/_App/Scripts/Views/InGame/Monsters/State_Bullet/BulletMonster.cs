using System;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace FantasySurvivor
{
	public class BulletMonster : View<GameApp>
	{

		public SpriteRenderer skin;

		[SerializeField] protected float size;

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
			Vector3 directionToTarget = _directionToTarget.normalized;
			var position = transform.position;
			position = Vector2.MoveTowards(position, position + directionToTarget, _speedBullet * Time.deltaTime);
			transform.position = position;

			var position1 = _character.transform.position;
			var x = position.x - position1.x;
			var y = position.y - position1.y;
			float distance;
			distance = x * x + y * y;
			var sizeTotal = size + _character.sizeBase;

			if(distance <= sizeTotal * sizeTotal)
			{
				_character.TakeDamage(_origin.model.attackDamage);
				Touch();
			} else if(distance > 900)
			{
				Destroy(gameObject);
			}
		}

		protected void Touch()
		{
			if(deadEffect != null)
			{
				Instantiate(deadEffect, transform.position, transform.rotation);
			}

			Destroy(gameObject);
		}

		protected void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, size);
		}
	}
}