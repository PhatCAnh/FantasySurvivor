using System.Linq;
using UnityEditor;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillBulletActive : SkillActive
	{
		[SerializeField] protected Transform skin;

		public SpawnPos spawnPos;

		public TargetType targetType;

		public bool canBlock;

		public float moveSpeed;

		protected Monster oldTarget;

		protected Vector3 targetPos;

		protected Vector3 direction;
		public override void Init(float damage, Monster target, int level)
		{
			base.Init(damage, target, level);

			if(target == null) return;
			
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
					if(gameController.CheckTouch(targetPos, transform.position, 0.1f))
					{
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
				switch (skillDamagedType)
				{
					case SkillDamagedType.Single:
						if(gameController.CheckTouch(targetPos, transform.position, sizeTouch))
						{
							TakeDamage();
							Destroy(gameObject);
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
					Destroy(gameObject);
					return;
				}
			}
			if(!gameController.CheckTouch(targetPos, transform.position, 30))
			{
				Destroy(gameObject);
			}
		}
	}
}