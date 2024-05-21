using ArbanFramework;
using ArbanFramework.StateMachine;
using UnityEngine;
namespace FantasySurvivor
{
	public class MonsterAttack : State<Monster>
	{
        private GameController gameController => Singleton<GameController>.instance;

        private Monster monster => Singleton<Monster>.instance;
        public MonsterAttack(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
            flip();
            agent.animator.SetFloat("Speed", 0f);
			agent.firePoint.up = agent.target.transform.position - agent.firePoint.position;
            agent.Attack();
			agent.IdleState();
        }

        public void flip()
        {
            if (agent.transform.position.x > gameController.character.transform.position.x)
            {
                Vector2 localScale = agent.animator.transform.localScale;
                localScale.x = -1;
                agent.animator.transform.localScale = localScale;
            }
            else
            {
                Vector2 localScale = agent.animator.transform.localScale;
                localScale.x = 1;
                agent.animator.transform.localScale = localScale;
            }
        }
    }
}