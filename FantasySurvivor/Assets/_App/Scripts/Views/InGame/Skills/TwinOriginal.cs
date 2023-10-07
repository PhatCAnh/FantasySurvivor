using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class TwinOriginal : SkillBulletActive
	{
		[SerializeField] private SkillActive _twinInstantiate;

		protected override void OnViewInit()
		{
			base.OnViewInit();
		}

		public override void TouchUnit(Vector3 pos)
		{
			base.TouchUnit(pos);

			var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
			bullet.GetComponent<TwinBorn>().InitTwin(new Vector2(-direction.y, direction.x), moveSpeed, damage, target);
			var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
			bullet2.GetComponent<TwinBorn>().InitTwin(new Vector2(direction.y, -direction.x), moveSpeed, damage, target);
			Destroy(gameObject);
		}
	}
}