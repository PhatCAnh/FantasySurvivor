using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using MR;
using MR.CharacterState;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : Unit
{
	public float size;
	
	public TowerView target => gameController.tower;

	private float _sizeDirection;

	protected override void Start()
	{
		base.Start();
		_sizeDirection = 0.1f + target.sizeBase + size;
	}

	protected override void OnViewInit()
	{
		base.OnViewInit();
		InitStateMachine(new MonsterIdle(this, stateMachine), new MonsterMove(this, stateMachine));
	}

	protected override void HandlePhysicUpdate()
	{
		moveDirection = target.transform.position - transform.position;
		
		if(moveDirection.magnitude < _sizeDirection)
			IdleState();
		else
		{
			MoveState();
		}
		SetAnimation(idleDirection);
	}

	protected override void Die()
	{
		gameController.MonsterDie(this);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, size);
	}
}