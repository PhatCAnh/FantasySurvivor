using System;
using _App.Scripts.Interface;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using MR.CharacterState;
using Stat;
using UnityEngine;

public class Monster : ObjectRPG
{
	#region Fields

	public Rigidbody2D myRigid;

	public Animator animator;

	public Transform firePoint;

	#region Properties

	private Vector2 _direction = Vector2.zero;

	public MonsterModel model { get; protected set; }

	public MonsterStat stat { get; protected set; }

	public Vector2 idleDirection { get; private set; } = Vector2.down;
	
	public float speedMul { get; set; } = 1;

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

	#endregion

	#region Fields State Machine

	public bool isIdle => _stateMachine.currentState == _idleState;

	public bool isMove => _stateMachine.currentState == _moveState;

	public bool isAttack => _stateMachine.currentState == _attackState;

	public bool isAlive => model.currentHealthPoint > 0;

	private StateMachine _stateMachine;
	private GameController gameController => Singleton<GameController>.instance;

	private MonsterIdle _idleState;

	private MonsterMove _moveState;

	private MonsterAttack _attackState;

	#endregion

	public float size;
	public TowerView target => gameController.tower;
	
	public MapView.WaveData wave { get; private set; }

	public Action<Monster> onMonsterDie;

	protected float sizeAttack;

	protected Cooldown cdAttack = new Cooldown();

	protected Vector3 moveTarget;
	
	

	#endregion

	#region Base Methods

	public virtual void Init(MonsterStat monsterStat, MapView.WaveData wave)
	{
		stat = monsterStat;
		model = new MonsterModel(
			stat.moveSpeed.BaseValue,
			stat.health.BaseValue,
			stat.attackDamage.BaseValue,
			stat.attackSpeed.BaseValue,
			wave.expMonster);
		this.wave = wave;
		sizeAttack = stat.attackRange.BaseValue != 0? stat.attackRange.BaseValue : 0.1f + target.sizeBase + size;
		moveTarget = target.transform.position;
	}

	protected override void OnViewInit()
	{
		base.OnViewInit();
		if(_stateMachine == null)
		{
			_stateMachine = new StateMachine();
			_idleState = new MonsterIdle(this, _stateMachine);
			_moveState = new MonsterMove(this, _stateMachine);
			_attackState = new MonsterAttack(this, _stateMachine);
			_stateMachine.Init(_idleState);
		}
		else
		{
			IdleState();
		}
	}

	private void Update()
	{
		if(gameController.isStop) return;
		var time = Time.deltaTime;
		cdAttack.Update(time);
		_stateMachine.currentState.LogicUpdate(time);
		HandlePhysicUpdate();
	}

	private void FixedUpdate()
	{
		if(gameController.isStop) return;
		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	#endregion

	protected virtual void HandlePhysicUpdate()
	{
		moveDirection = moveTarget - transform.position;

		if(moveDirection.magnitude < sizeAttack)
		{
			if(cdAttack.isFinished)
			{
				AttackState();
				cdAttack.Restart(model.attackSpeed);
			}
			//IdleState();
		}
		else
		{
			MoveState();
		}
		SetAnimation(idleDirection);
	}

	protected virtual void SetAnimation(Vector2 directionMove)
	{
		animator.SetFloat("SpeedMul", speedMul);
		animator.SetFloat("Horizontal", directionMove.x);
		animator.SetFloat("Vertical", directionMove.y);
	}

	public virtual void Attack()
	{
		target.TakeDamage(model.attackDamage);
	}

	public void TakeDamage(int damage)
	{
		if(!isAlive) return;
		model.currentHealthPoint -= damage;
		Singleton<PoolTextPopup>.instance.GetObjectFromPool(transform.position, damage.ToString(), TextPopupType.TowerDamage);
		if(!isAlive) Die();
	}

	protected virtual void Die(bool selfDie = false)
	{
		gameController.MonsterDie(this, selfDie);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, size);
	}

	#region State Machine Method

	public virtual void IdleState()
	{
		if(isIdle) return;
		_stateMachine.ChangeState(_idleState);
	}

	public void MoveState()
	{
		if(isMove) return;
		_stateMachine.ChangeState(_moveState);
	}

	public void AttackState()
	{
		if(isAttack) return;
		_stateMachine.ChangeState(_attackState);
	}

	#endregion
}