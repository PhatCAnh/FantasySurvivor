using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
namespace MR.CharacterState.Unit.Monsters
{
	public class RangedMonster : Monster
	{
		public BulletMonster bulletPrefab;

		private Vector3 spawnPos { get; set; }

		[SerializeField] private int _numberBack = 0;

		private int _coolDownBack = 0;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			spawnPos = transform.position;
		}

		private bool CheckBack()
		{
			return _coolDownBack >= _numberBack;
		}
		
		protected override void HandlePhysicUpdate()
		{
			moveDirection = moveTarget - transform.position;
			if(CheckBack())
			{
				MoveState();
				
				if(moveDirection.magnitude < 0.1f)
				{
					Die(true);
				}
				SetAnimation(idleDirection);
				return;
			}

			if(moveDirection.magnitude < sizeAttack)
			{
				if(cdAttack.isFinished)
				{
					AttackState();
					cdAttack.Restart(1/ model.attackSpeed);
				}
				//IdleState();
			}
			else
			{
				MoveState();
			}
			SetAnimation(idleDirection);
		}
		
		

		public override void Attack()
		{
			var arrowIns = Instantiate(bulletPrefab);
			arrowIns.Init(this);
			_coolDownBack++;
			if(_numberBack != 0)
			{
				if(CheckBack())
				{
					moveTarget = spawnPos;
				}
			}
		}
	}
}