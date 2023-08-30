using ArbanFramework.StateMachine;
using UnityEngine;

namespace MR.CharacterState
{
	public class CharacterMove : UnitMove
	{
		public CharacterMove(Character agent, StateMachine stateMachine) : base( agent, stateMachine )
		{
		}
	}
}