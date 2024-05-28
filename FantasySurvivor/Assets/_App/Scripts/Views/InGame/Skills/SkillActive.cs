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

		public TargetType targetType;
		
		public ItemPrefab type;
		
		protected float damage;

		protected bool isCritical;

		protected float sizeTouch;

        [HideInInspector]
        public LevelSkillData data;

        [HideInInspector]
        public Character origin;

		protected GameObject hitEffect;

		protected int level;
		
		protected Action callBackDamaged, callBackKilled;
		public GameController gameController => Singleton<GameController>.instance;

		public virtual void Init(LevelSkillData dataLevel, Monster target, int level, ItemPrefab type)
		{
			this.origin = gameController.character;

			this.type = type;

			if(targetType != TargetType.None)
			{
				this.target = target;
				
				this.sizeTouch = size + target.size;
			}

			this.level = level;
			
			data = dataLevel;

			this.damage = origin.model.attackDamage * data.value / 100;
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
			CreateExplo();
            return true;
		}

		protected virtual void CreateExplo()
		{
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