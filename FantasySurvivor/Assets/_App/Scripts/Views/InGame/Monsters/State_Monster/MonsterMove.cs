using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.StateMachine;
using DG.Tweening;
using UnityEngine;

namespace FantasySurvivor
{
	public class MonsterMove : State<Monster>
	{

        private GameController gameController => Singleton<GameController>.instance;

        private Monster monster => Singleton<Monster>.instance;

        public MonsterMove(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
            base.Enter();
            agent.animator.SetFloat("Speed", 1f);
        }

		public override void PhysicUpdate(float fixedDeltaTime)
		{
			base.PhysicUpdate(fixedDeltaTime);
			var directionUnit = agent.moveDirection.normalized;
            Move(directionUnit, Time.fixedDeltaTime);
        }
    
		private void Move(Vector2 dir, float deltaTime)
		{
            agent.Move(dir, deltaTime);
        }

	}
}