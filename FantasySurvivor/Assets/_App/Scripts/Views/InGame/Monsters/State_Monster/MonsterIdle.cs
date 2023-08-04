using ArbanFramework.StateMachine;

public class MonsterIdle : State<Monster>
{
	public MonsterIdle(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}
}
