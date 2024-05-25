using ArbanFramework;
using ArbanFramework.StateMachine;
using UnityEngine;

public class MonsterIdle : State<Monster>
{
    private GameController gameController => Singleton<GameController>.instance;
    private Monster monster => Singleton<Monster>.instance;

    public MonsterIdle(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
	{
	}
	
	public override void Enter()
	{
		base.Enter();
        agent.animator.SetFloat("Speed", 0f);
	}
}
