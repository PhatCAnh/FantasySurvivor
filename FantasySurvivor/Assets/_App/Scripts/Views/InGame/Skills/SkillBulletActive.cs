using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Controllers;
using ArbanFramework;
using UnityEngine;
namespace FantasySurvivor
{
    public class SkillBulletActive : SkillActive
    {
        [SerializeField] protected Transform skin;
        
		public TargetType targetType;

		protected HashSet<Monster> attackedMonsters = new HashSet<Monster>();

        public bool canBlock;

        public float moveSpeed;

        protected Monster oldTarget;

        protected Vector3 targetPos;

		protected Vector3 direction;
		public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
		{
			base.Init(data, target, level, type);

			if(target == null) return;


            this.direction = target.transform.position - transform.position;

            skin.up = direction;
        }

        protected virtual void FixedUpdate()
        {
            if (gameController.isStop) return;
			switch (targetType)
			{
				case TargetType.Shot:
					transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);
					break;
				case TargetType.Target:
					transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
					skin.up = direction;
					if(gameController.CheckTouch(targetPos, transform.position, 0.1f))
					{
						//fix it
						Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
					}
					break;
			}
			HandleTouch();
		}

		protected virtual void HandleTouch()
		{
			if(!canBlock)
			{
				switch (skillDamagedType)
				{
					case SkillDamagedType.Single:
						if(gameController.CheckTouch(targetPos, transform.position, sizeTouch))
						{
							TakeDamage();
							Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
						}
						break;
					case SkillDamagedType.AreaOfEffect:
						foreach(var mons in gameController.listMonster.ToList())
						{
							CheckTouchMonsters(mons);
						}
						break;
				}

			}
			else
			{
				if(gameController.listMonster.ToList().Any(CheckTouchMonsters))
				{
					Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
					return;
				}
			}
			if(!gameController.CheckTouch(targetPos, transform.position, 30))
			{
				Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
			}
		}
	}
}