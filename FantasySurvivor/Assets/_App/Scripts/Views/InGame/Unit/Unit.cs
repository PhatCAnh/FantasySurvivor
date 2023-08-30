using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using MR.CharacterState;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Unit : ObjectRPG
{
	#region Fields

	[HideInInspector]
	public Rigidbody2D myRigid;

	public Animator animator;
	
	public Transform trfUI;
	
	public UnitModel model { get; protected set; }
	
	public UnitStat stat { get; protected set; }
	
	public float speedMul { get; set; } = 1;
	
	public Vector2 idleDirection { get; private set; } = Vector2.down;
	public Vector2 moveDirection
	{
		get => direction;
		set {
			if(value != Vector2.zero)
			{
				idleDirection = value;
			}
			direction = value;
		}
	}
	public bool isIdle => stateMachine.currentState == _idleState;

	public bool isMove => stateMachine.currentState == _moveState;
	
	public bool isAlive => model.currentHealthPoint > 0;

	
	protected Vector2 direction = Vector2.zero;
	
	protected StateMachine stateMachine;
	protected GameController gameController => Singleton<GameController>.instance;
	
	private UnitIdle _idleState;
	
	private UnitMove _moveState;

	#endregion
	protected virtual void Update()
	{
		var time = Time.deltaTime;
		stateMachine.currentState.LogicUpdate(time);
		HandlePhysicUpdate();
	}
	
	protected virtual void FixedUpdate()
	{
		if(gameController.isStop) return;
		stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	protected override void OnViewInit()
	{
		base.OnViewInit();
		myRigid = GetComponent<Rigidbody2D>();
	}
	public virtual void Init(UnitModel unitModel)
	{
		this.model = unitModel;
	}
	
	[Button]
	public void TakeDamage(int damage)
	{
		model.currentHealthPoint -= damage;
		if(!isAlive) Die();
	}
	
	protected virtual void Die()
	{
		Destroy(gameObject);
	}
	
	protected virtual void InitStateMachine(UnitIdle idle, UnitMove move)
	{
		base.OnViewInit();
		if(stateMachine == null)
		{
			stateMachine = new StateMachine();
			_idleState = idle;
			_moveState = move;
			stateMachine.Init(_idleState);
		}
		else
		{
			IdleState();
		}
	}
	protected abstract void HandlePhysicUpdate();
	
	protected virtual void SetAnimation(Vector2 directionMove)
	{
		animator.SetFloat("SpeedMul", speedMul);
		animator.SetFloat("Horizontal", directionMove.x);
		animator.SetFloat("Vertical", directionMove.y);
	}
	
	#region State Machine Method

	public virtual void IdleState()
	{
		if(isIdle) return;
		stateMachine.ChangeState(_idleState);
	}

	public virtual void MoveState()
	{
		if(isMove) return;
		stateMachine.ChangeState(_moveState);
	}

	#endregion
}
