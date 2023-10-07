using System.Linq;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillBulletActive : SkillActive
	{
		public enum SpawnPos
		{
			Character,
			Monster,
			OldBullet,
		}

		public enum TargetType
		{
			Target,
			Shot,
		}
		
		[SerializeField] protected Transform skin;
		
		public SpawnPos spawnPos;

		public TargetType targetType;
		
		public bool canBlock;
		
		public float moveSpeed;
		
		protected Monster oldTarget;

		protected Vector3 targetPos;

		protected Vector3 direction;
		public override void Init(float damage, Monster target, GameObject effect)
		{
			base.Init(damage, target, effect);
			transform.position = spawnPos == SpawnPos.Character ? origin.transform.position : target.transform.position;
			
			this.direction = target.transform.position - transform.position;
			
			skin.up = direction;
		}

		private void FixedUpdate()
		{
			if(gameController.isStop) return;
			
			if(target != null && skillDamagedType == SkillDamagedType.Single)
			{
				targetPos = target.transform.position;
			}

			switch (targetType)
			{
				case TargetType.Shot:
					transform.Translate(moveSpeed * Time.fixedDeltaTime * direction.normalized);
					break;
				case TargetType.Target:
					transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.fixedDeltaTime);
					skin.up = direction;
					if(Vector2.Distance(targetPos, transform.position) < 0.1f)
					{
						TouchUnit(targetPos);
						Destroy(gameObject);
					}
					break;
			}
			HandleTouch();
		}
		
		protected virtual void HandleTouch()
		{
			if(!canBlock)
			{
				if(Vector2.Distance(targetPos, transform.position) < sizeTouch)
				{
					TakeDamage();
					TouchUnit(targetPos);
					Destroy(gameObject);
				}
			}
			else
			{
				foreach(var mons in gameController.listMonster.ToList())
				{
					if(CheckTouchMonsters(mons) && skillDamagedType == SkillDamagedType.Single)
					{
						Destroy(gameObject);
						return;
					}
				}
			}
			if(Vector2.Distance(origin.transform.position, transform.position) > 30)
			{
				TouchUnit(transform.position);
				Destroy(gameObject);
			}
		}
	}
}