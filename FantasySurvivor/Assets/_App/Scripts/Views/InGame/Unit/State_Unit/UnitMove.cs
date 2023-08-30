using System.Collections;
using System.Collections.Generic;
using ArbanFramework.StateMachine;
using UnityEngine;

public class UnitMove : State<Unit>
{
    public UnitMove(Unit agent, StateMachine stateMachine) : base( agent, stateMachine )
    {
    }

    public override void PhysicUpdate(float fixedDeltaTime)
    {
        base.PhysicUpdate(fixedDeltaTime);
        var directionUnit = agent.moveDirection.normalized;
        Move(directionUnit, Time.fixedDeltaTime);
    }
    
    private void Move(Vector2 dir, float deltaTime)
    {
        var movement = agent.model.moveSpeed  * GameConst.MOVE_SPEED_ANIMATION_RATIO * deltaTime * agent.speedMul * dir;
        var newPosition = agent.myRigid.position + movement;
        agent.myRigid.MovePosition(newPosition);
    }
}
