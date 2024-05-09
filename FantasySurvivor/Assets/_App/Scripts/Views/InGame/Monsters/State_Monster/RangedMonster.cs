using ArbanFramework;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
namespace FantasySurvivor
{

	public class RangedMonster : Monster
	{

		public BulletMonster bulletPrefab;

        private Vector3 spawnPos { get; set; }

		[SerializeField] private int _numberBack = 0;

		private int _coolDownBack = 0;
        private GameController gameController => Singleton<GameController>.instance;

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
            moveTarget = gameController.character.transform.position;
            moveDirection = moveTarget - transform.position;


            if (moveDirection.magnitude < sizeAttack)
            {
                if (cdAttack.isFinished)
                {
                    AttackState();
                    cdAttack.Restart(1 / model.attackSpeed);
                    animator.SetBool("Attack", true);
                }
                else
                {
                    animator.SetBool("Attack", false);
                }
            }
            else if (moveDirection.magnitude > 25)
            {
                transform.position = gameController.RandomPositionAroundCharacter(20);
            }
            else
            {
                MoveState();
            }
            SetAnimation(idleDirection);
        }



        public override void Attack()
		{
           
            // Tạo một instance mới của viên đạn từ bulletPrefab
            var arrowIns = Instantiate(bulletPrefab);
            // Khởi tạo viên đạn
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