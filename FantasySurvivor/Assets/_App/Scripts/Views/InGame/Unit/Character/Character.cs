using System;
using ArbanFramework;
using ArbanFramework.Config;
using ArbanFramework.MVC;
using FantasySurvivor;
using MR;
using MR.CharacterState;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using StateMachine = ArbanFramework.StateMachine.StateMachine;

public class Character : Unit
{
	
	protected override void OnViewInit()
	{
		base.OnViewInit();
		InitStateMachine(new CharacterIdle(this, stateMachine), new CharacterMove(this, stateMachine));
	}

	public void Controlled(Vector2 moveForce)
	{
		moveDirection = moveForce;
	}

	protected override void HandlePhysicUpdate()
	{
		if(moveDirection == Vector2.zero)
			IdleState();
		else
			MoveState();
		SetAnimation(idleDirection);
	}
	
	protected override void SetAnimation(Vector2 directionMove)
	{
		base.SetAnimation(directionMove);
		animator.SetFloat("Speed", moveDirection.normalized.magnitude);
	}
}