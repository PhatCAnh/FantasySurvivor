using System.Collections;
using System.Collections.Generic;
using ArbanFramework.StateMachine;
using UnityEngine;

namespace FantasySurvivor
{
	public class MonsterMove : UnitMove
	{
		public MonsterMove(Monster agent, StateMachine stateMachine) : base(agent, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			agent.animator.SetFloat("Speed", 1f);
		}
	}
}