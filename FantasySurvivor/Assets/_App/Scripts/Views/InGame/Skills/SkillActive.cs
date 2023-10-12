using System;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
namespace FantasySurvivor
{
	public class SkillActive : View<GameApp>
	{
		public Monster target;

		public float size;
		
		public SkillDamagedType skillDamagedType;

		protected GameObject callBackEffect;

		protected float damage;

		protected bool isCritical;

		protected float sizeTouch;

		protected Character origin;

		protected int level;
		protected Action callBackDamaged, callBackKilled;
		protected GameController gameController => Singleton<GameController>.instance;
		protected override void OnViewInit()
		{
			base.OnViewInit();
		}

		public virtual void Init(float damage, Monster target, GameObject effect, int level)
		{
			this.origin = gameController.character;
			
			if(target == null)
			{
				Destroy(gameObject);
				return;
			}

			this.level = level;
			
			this.target = target;

			callBackEffect = effect;

			this.damage = damage;

			sizeTouch = size + target.size;
		}
		

		public virtual void TouchUnit(Vector3 pos)
		{
			if(callBackEffect != null)
			{
				Instantiate(callBackEffect, pos, quaternion.identity);
			}
		}
		

		public virtual void TakeDamage(Monster monster = null)
		{
			switch (skillDamagedType)
			{
				case SkillDamagedType.Single:
					if(monster == null)
					{
						monster = target;
					}
					monster.TakeDamage(damage, isCritical, callBackDamaged, callBackKilled);
					break;
				case SkillDamagedType.AreaOfEffect:
						CheckAoeMons();
					break;
			}
		}

		protected virtual bool CheckTouchMonsters(Monster monster)
		{
			sizeTouch = size + monster.size;
			if(Vector2.Distance(monster.transform.position, transform.position) < sizeTouch)
			{
				TakeDamage(monster);
				TouchUnit(monster.transform.position);
				return true;
			}
			return false;
		}

		protected void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, size);
		}
		
		protected void CheckAoeMons()
		{
			foreach(var mons in gameController.listMonster.ToList())
			{
				sizeTouch = size + mons.size;
				if(Vector2.Distance(mons.transform.position, transform.position) < sizeTouch)
				{
					mons.TakeDamage(damage, isCritical);
					TouchUnit(mons.transform.position);
				}
			}
		}
	}
}