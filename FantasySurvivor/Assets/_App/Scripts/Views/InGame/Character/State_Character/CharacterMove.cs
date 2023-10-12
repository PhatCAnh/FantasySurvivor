using ArbanFramework.StateMachine;
using UnityEngine;

namespace FantasySurvivor
{
	public class CharacterMove : State<Character>
	{
		public CharacterMove(Character agent, StateMachine stateMachine) : base( agent, stateMachine )
		{
		}

		public override void PhysicUpdate(float fixedDeltaTime)
		{
			base.PhysicUpdate(fixedDeltaTime);
			Move(agent.moveDirection.normalized, Time.fixedDeltaTime);
		}

		private void Move(Vector2 dir, float deltaTime)
		{
			var movement = agent.model.moveSpeed * GameConst.MOVE_SPEED_ANIMATION_RATIO * deltaTime * dir;
			agent.isCharacterMoving?.Invoke(movement.magnitude);
			var newPosition = agent.myRigid.position + movement;
			agent.myRigid.MovePosition(newPosition);
			
		}
	}
}