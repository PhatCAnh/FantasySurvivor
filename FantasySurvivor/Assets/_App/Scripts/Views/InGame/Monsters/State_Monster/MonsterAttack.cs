using ArbanFramework.StateMachine;
namespace FantasySurvivor
{
	public class MonsterAttack : State<Monster>
	{

		public MonsterAttack(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			agent.animator.SetFloat("Speed", 0f);
			agent.firePoint.up = agent.target.transform.position - agent.firePoint.position;
			agent.Attack();
			agent.IdleState();
        }
	}
}