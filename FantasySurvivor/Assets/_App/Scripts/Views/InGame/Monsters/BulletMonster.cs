using System;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace MR.CharacterState.Unit
{
	public class BulletMonster : View<GameApp>
	{
		public SpriteRenderer skin;

		public Rigidbody2D rigidbody2d;

		private Monster _origin;

		private TowerView _target;

		public void Init(Monster origin, float speed)
		{
			_origin = origin;
			_target = origin.target;
			var spawnPos = _origin.firePoint;
			transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
			rigidbody2d.velocity = spawnPos.up * speed;
			Destroy(gameObject, 3f);
		}

		private void TouchUnit()
		{
			Destroy(gameObject);
		}

		private void Update()
		{
			if(Vector2.Distance(transform.position, _target.transform.position) < 0.25f)
			{
				_target.TakeDamage(_origin.model.attackDamage);
				Singleton<PoolTextPopup>.instance.GetObjectFromPool(_target.transform.position, _origin.model.attackDamage.ToString(), TextPopupType.Damage);
				TouchUnit();
			}
		}
	}
}