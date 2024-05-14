using System;
using System.Linq;
using _App.Scripts.Controllers;
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
		
		public ItemPrefab type;
		
		protected float damage;

		protected bool isCritical;

		protected float sizeTouch;


		protected Character origin;

		protected GameObject hitEffect;

		protected int level;
		
		protected Action callBackDamaged, callBackKilled;
		protected GameController gameController => Singleton<GameController>.instance;

		public virtual void Init(float damage, Monster target, int level, ItemPrefab type)
		{
			this.origin = gameController.character;

			this.type = type;

			if(target == null)
			{
				Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
				return;
			}

			this.level = level;

			this.target = target;

			this.damage = damage;
			
			this.

			sizeTouch = size + target.size;
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

			if(!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
			TakeDamage(monster);
			return true;
		}

		protected virtual void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(transform.position, size);
		}

		protected virtual void TouchUnit(Monster mons)
		{
			mons.TakeDamage(damage, isCritical);
		}

		protected virtual void CheckAoeMons()
		{
			foreach(var mons in gameController.listMonster.ToList())
			{
				if(gameController.CheckTouch(mons.transform.position, transform.position, sizeTouch))
				{
					TouchUnit(mons);
				}
			}
		}
	}
}