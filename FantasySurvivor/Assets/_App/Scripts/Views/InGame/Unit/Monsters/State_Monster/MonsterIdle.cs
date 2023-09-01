using ArbanFramework.StateMachine;

public class MonsterIdle : State<Monster>
{
	public MonsterIdle(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}
	
	public override void Enter()
	{
		base.Enter();
		agent.animator.SetFloat("Speed", 0f);
	}
}
