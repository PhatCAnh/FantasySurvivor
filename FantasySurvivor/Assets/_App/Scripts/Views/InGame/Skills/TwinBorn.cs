using System;
using ArbanFramework.MVC;
using UnityEngine;
namespace FantasySurvivor
{
	public class TwinBorn : SkillBulletActive
	{
		public void InitTwin(Vector2 direction, float moveSpeed, float damage, Monster oldTarget)
		{
			this.origin = gameController.character;
			this.direction = direction;
			this.moveSpeed = moveSpeed;
			skin.up = -direction;
			this.damage = damage;
			this.oldTarget = oldTarget;
		}

		protected override bool CheckTouchMonsters(Monster monster)
		{
			if(monster.Equals(oldTarget)) return false;
			return base.CheckTouchMonsters(monster);
		}
	}
}