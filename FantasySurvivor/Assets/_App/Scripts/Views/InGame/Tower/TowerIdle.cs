using ArbanFramework.StateMachine;
namespace FantasySurvivor
{
	public class TowerIdle : State<TowerView>
	{

		public TowerIdle(TowerView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void LogicUpdate(float deltaTime)
		{
			base.LogicUpdate(deltaTime);
			// agent.target = agent.gameController.GetAllMonsterInAttackRange();
			// if(agent.target != null)
			// 	agent.AttackState();
		}
	}
}