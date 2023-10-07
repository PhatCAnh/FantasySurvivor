using ArbanFramework.StateMachine;
namespace FantasySurvivor
{
	public class TowerAttack : State<TowerView>
	{
		public TowerAttack(TowerView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			agent.animator.SetTrigger("Attack");
			agent.weapon.up = agent.target.transform.position - agent.weapon.position;
		}
	}
}