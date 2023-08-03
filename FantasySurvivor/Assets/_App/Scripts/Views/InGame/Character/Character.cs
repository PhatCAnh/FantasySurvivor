using System;
using ArbanFramework;
using ArbanFramework.Config;
using ArbanFramework.MVC;
using MR.CharacterState;
using UnityEngine;
using UnityEngine.Serialization;
using StateMachine = ArbanFramework.StateMachine.StateMachine;

namespace MR
{
	public class Character : ObjectRPG
	{
		[HideInInspector]
		public Rigidbody2D myRigid;

		public Animator animator;

		public CharacterModel model { get; private set; }

		public CharacterStat stat { get; private set; }
		public bool isMove => _stateMachine.currentState == _moveSM;

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
		private CharacterIdle _idleSM;
		private CharacterMove _moveSM;

		private GameController _gameController => Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			if(_stateMachine == null)
			{
				_stateMachine = new StateMachine();
				_idleSM = new CharacterIdle(this, _stateMachine);
				_moveSM = new CharacterMove(this, _stateMachine);
				_stateMachine.Init(_idleSM);
			}
			else
			{
				IdleState();
			}

			myRigid = GetComponent<Rigidbody2D>();
			Init(new CharacterModel(1f, 120f));
		}

		public void Init(CharacterModel model)
		{
			this.model = model;
		}

		private void Update()
		{
			// if(_gameController.isStop) return;
			var time = Time.deltaTime;
			_stateMachine.currentState.LogicUpdate(time);
			// if(isDoingSomething) return;

			HandlePhysicUpdate();
		}

		private void FixedUpdate()
		{
			if(_gameController.isStop) return;

			_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
		}

		public void Controlled(float deltaTime, Vector2 moveForce)
		{
			moveDirection = moveForce;
		}

		private void HandlePhysicUpdate()
		{

			if(moveDirection == Vector2.zero)
				IdleState();
			else
				MoveState();

			SetAnimation(moveDirection, idleDirection);
		}

		private void SetAnimation(Vector2 dir, Vector2 idleDirection)
		{
			Debug.Log($"Dir: {dir}");
			Debug.Log($"idleDirection: {idleDirection}");
			//animator.SetFloat("SpeedMul", speedMul);
			animator.SetFloat("Speed", dir.normalized.magnitude);
			animator.SetFloat("Horizontal", idleDirection.x);
			animator.SetFloat("Vertical", idleDirection.y);
		}

		#region State Machine Method

		public void IdleState() => _stateMachine.ChangeState(_idleSM);

		public void MoveState()
		{
			if(isMove) return;
			_stateMachine.ChangeState(_moveSM);
		}

		#endregion
	}
}