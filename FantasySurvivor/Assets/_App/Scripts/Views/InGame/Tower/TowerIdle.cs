using ArbanFramework.StateMachine;
namespace MR.CharacterState.Tower
{
	public class TowerIdle : State<TowerView>
	{

		public TowerIdle(TowerView agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void LogicUpdate(float deltaTime)
		{
			base.LogicUpdate(deltaTime);
			agent.target = agent.gameController.GetFirstMonster();
			if(agent.target != null)
				agent.AttackState();
		}
	}
}