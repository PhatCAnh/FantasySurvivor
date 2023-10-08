using System;
using ArbanFramework.MVC;
using Unity.VisualScripting;
using UnityEngine;
namespace FantasySurvivor
{
	public class TwinBorn : SkillBulletActive
	{
		[SerializeField] private SkillActive _twinInstantiate;

		public void InitTwin(Vector2 direction, float moveSpeed, float damage, GameObject effect, Monster oldTarget, int level)
		{
			this.level = level;
			this.callBackEffect = effect;
			this.origin = gameController.character;
			this.direction = direction;
			this.moveSpeed = moveSpeed;
			skin.up = -direction;
			this.damage = damage;
			this.oldTarget = oldTarget;

			if(this.level == 6)
			{
				callBackKilled += SpawnTwinBorn;
			}
		}

		protected override bool CheckTouchMonsters(Monster monster)
		{
			if(monster.Equals(oldTarget)) return false;
			return base.CheckTouchMonsters(monster);
		}

		private void SpawnTwinBorn()
		{
			var bullet = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, 90, 0));
			bullet.GetComponent<TwinBorn>().InitTwin(new Vector2(-direction.y, direction.x), moveSpeed, damage/2, callBackEffect, target, level);
			var bullet2 = Instantiate(_twinInstantiate, transform.position, new Quaternion(0, 0, -90, 0));
			bullet2.GetComponent<TwinBorn>().InitTwin(new Vector2(direction.y, -direction.x), moveSpeed, damage/2, callBackEffect, target, level);
			Destroy(gameObject);
		}
	}
}