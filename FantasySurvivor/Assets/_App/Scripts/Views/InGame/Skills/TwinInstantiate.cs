using System;
using ArbanFramework.MVC;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class TwinInstantiate : View<GameApp>
	{
		[SerializeField] private Transform _skin;
		
		private Vector2 _direction;

		private Monster _oldTarget;
		
		private float _moveSpeed;
		private float _damage;
		private bool _isCritical;

		private bool _isDamage;
		

		public void Init(Vector2 direction, float moveSpeed, float damage, Monster oldTarget)
		{
			_direction = direction;
			_moveSpeed = moveSpeed;
			_skin.up = -direction;
			_damage = damage;
			_oldTarget = oldTarget;
		}

		private void FixedUpdate()
		{
			transform.Translate(_moveSpeed * Time.fixedDeltaTime * _direction.normalized);
		}
		
		protected virtual void TouchUnit()
		{
			Destroy(gameObject);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.TryGetComponent(out Monster monster) && !monster.Equals(_oldTarget) && !_isDamage)
			{
				_isDamage = true;
				monster.TakeDamage(_damage, _isCritical);
				TouchUnit();
			}
		}
	}
}