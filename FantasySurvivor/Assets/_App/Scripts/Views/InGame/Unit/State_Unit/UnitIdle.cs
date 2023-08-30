using System.Collections;
using System.Collections.Generic;
using ArbanFramework.StateMachine;
using UnityEngine;

public class UnitIdle : State<Unit>
{
    // Start is called before the first frame update
    public UnitIdle(Unit agent, StateMachine stateMachine) : base(agent, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        agent.animator.SetFloat("Speed", 0f);
    }
}
