using ArbanFramework.StateMachine;
using UnityEngine;

namespace MR.CharacterState
{
	public class CharacterMove : State<Character>
	{
		public CharacterMove(Character agent, StateMachine stateMachine) : base( agent, stateMachine )
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
			var movement = agent.model.moveSpeed * dir * GameConst.MOVE_SPEED_ANIMATION_RATIO * deltaTime * agent.speedMul;
			var newPosition = agent.myRigid.position + movement;
			agent.myRigid.MovePosition(newPosition);
		}
	}
}