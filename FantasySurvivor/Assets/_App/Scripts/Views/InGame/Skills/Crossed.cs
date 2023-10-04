using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class Crossed : BulletView
	{
		[SerializeField] private TwinInstantiate _twinInstantiate;

		private Vector2 _direction;

		private Vector3 _oldTransform;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			_oldTransform = transform.position;
		}

		protected override void TouchUnit()
		{
			base.TouchUnit();

			_direction = transform.position - _oldTransform;
			var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
			bullet.GetComponent<TwinInstantiate>().Init(new Vector2(-_direction.y, _direction.x), moveSpeed, damage, target);
			var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
			bullet2.GetComponent<TwinInstantiate>().Init(new Vector2(_direction.y, -_direction.x), moveSpeed, damage, target);
			Destroy(gameObject);
		}
	}
}