using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using MR;
using MR.CharacterState;
using UnityEngine;

public class Monster : ObjectRPG
{
	[HideInInspector]
	public Rigidbody2D myRigid;

	public Animator animator;

	public MonsterStat stat { get; private set; }
	
	public MonsterModel model { get; private set; }

	public Character target { get; private set; }
	
	public bool isMove => _stateMachine.currentState == _moveSM;
	public bool isIdle => _stateMachine.currentState == _idleSM;

	public float speedMul { get; set; } = 1;
	public Vector2 idleDirection { get; private set; } = Vector2.down;

	public Vector2 moveDirection
	{
		get => _direction;
		set {
			if(value != Vector2.zero)
			{
				idleDirection = value;
			}
			_direction = value;
		}
	}
	
	private Vector2 _direction = Vector2.zero;

	private StateMachine _stateMachine;
	private MonsterIdle _idleSM;
	private MonsterMove _moveSM;
	
	private GameController _gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		if(_stateMachine == null)
		{
			_stateMachine = new StateMachine();
			_idleSM = new MonsterIdle(this, _stateMachine);
			_moveSM = new MonsterMove(this, _stateMachine);
			_stateMachine.Init(_idleSM);
		}
		else
		{
			IdleState();
		}

		myRigid = GetComponent<Rigidbody2D>();
	}

	public void Init(MonsterModel modelInit, Character character)
	{
		model = modelInit;
		target = character;
	}

	private void Update()
	{
		var time = Time.deltaTime;
		Controlled(Vector2.right);
		_stateMachine.currentState.LogicUpdate(time);
		
		HandlePhysicUpdate();
	}

	private void FixedUpdate()
	{
		if(_gameController.isStop) return;
		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	public void Controlled(Vector2 moveForce)
	{
		moveDirection = moveForce;
	}

	private void HandlePhysicUpdate()
	{
		moveDirection = target.transform.position - transform.position;
		if(moveDirection.magnitude < 0.1f)
			IdleState();
		else
		{
			MoveState();
		}

		SetAnimation(idleDirection);
	}

	private void SetAnimation(Vector2 idleDirection)
	{
		animator.SetFloat("SpeedMul", speedMul);
		animator.SetFloat("Horizontal", idleDirection.x);
		animator.SetFloat("Vertical", idleDirection.y);
	}

	#region State Machine Method

	public void IdleState()
	{
		if(isIdle) return;
		animator.SetFloat("Speed", 0f);
		_stateMachine.ChangeState(_idleSM);
	}

	public void MoveState()
	{
		if(isMove) return;
		animator.SetFloat("Speed", 1f);
		_stateMachine.ChangeState(_moveSM);
	}

	#endregion
	
}