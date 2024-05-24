﻿using System;
using System.Runtime.InteropServices.WindowsRuntime;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using Unity.Mathematics;
using Unity.Services.Analytics.Internal;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Monster : ObjectRPG
{
	#region Fields

	public Rigidbody2D myRigid;

	public Animator animator;

	public Transform firePoint;

	public bool justSpawnVertical = false;

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

    public bool isDead;

	public bool isNoMove = false;

	public bool isStandStill { get; private set; } = false;

    private StateMachine _stateMachine;
	private GameController gameController => Singleton<GameController>.instance;

	private MonsterIdle _idleState;

	private MonsterMove _moveState;

	private MonsterAttack _attackState;

    #endregion

    public float size;
	public Character target => gameController.character; 

	public MapView.WaveData wave { get; private set; }

	protected float sizeAttack;

	protected Cooldown cdAttack = new Cooldown();

	protected Vector3 moveTarget;

    protected float moveSpeedDecreaseAmount;

    protected Cooldown moveSpeedCooldown = new Cooldown();

    protected float moveSpeedCooldownTime = 3f;

    #endregion

    #region Base Methods

    public virtual void Init(MonsterStat monsterStat, MapView.WaveData wave, ItemPrefab monsType)
	{
		stat = monsterStat;

		var exp = 0;

		if(wave != null)
		{
            this.wave = wave;
			exp = wave.expMonster;
        }

        model = new MonsterModel(
			stat.moveSpeed.BaseValue,
			stat.health.BaseValue,
			stat.attackDamage.BaseValue,
			stat.attackSpeed.BaseValue,
			exp);
		sizeAttack = stat.attackRange.BaseValue != 0 ? stat.attackRange.BaseValue : 0.1f + target.sizeBase + size;

        InitializationStateMachine();

	}

    private void Update()
	{
		if(gameController.isStop) return;
		var time = Time.deltaTime;
		cdAttack.Update(time);
		_stateMachine.currentState.LogicUpdate(time);
	}

	private void FixedUpdate()
	{
		if(gameController.isStop)
		{
			Stop();
			return;
		}
		HandlePhysicUpdate();
		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	#endregion

	protected virtual void HandlePhysicUpdate()
	{
        if (isDead) return;
        moveTarget = gameController.character.transform.position;
		moveDirection = moveTarget - transform.position;

		if(moveDirection.magnitude < sizeAttack)
		{
			if(cdAttack.isFinished)
			{
                animator.SetBool("Attack", true);
                AttackState();			
				cdAttack.Restart(1 / model.attackSpeed);
            } else
			{
                IdleState();
                animator.SetBool("Attack", false);
            }
		}
		else if (moveDirection.magnitude > 25)
		{
			transform.position = gameController.RandomPositionSpawnMonster(20);
		}
		else
		{
            MoveState();
            animator.SetBool("Attack", false);
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


	public virtual void TakeDamage(float damage, bool isCritical = false, Action callBackDamaged = null, Action callBackKilled = null)
	{
        if (!isAlive) return;
		model.currentHealthPoint -= damage;
		callBackDamaged?.Invoke();

		var text = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
		text.GetComponent<TextPopup>().Create(damage.ToString(), TextPopupType.TowerDamage, isCritical);
        if (isAlive) return;
		Die();
        callBackKilled?.Invoke();
    }

    public void ResetAttackCountdown()
    {
        cdAttack.Restart(model.attackSpeed);
    }

    public virtual void Move(Vector2 dir, float deltaTime)
	{
		if(isDead) return;
        var movement = model.moveSpeed * GameConst.MOVE_SPEED_ANIMATION_RATIO * deltaTime * speedMul * dir;
        var newPosition = myRigid.position + movement;
        myRigid.MovePosition(newPosition);
    }

    public virtual void Die(bool selfDie = false)
	{
        isDead = true;
        animator.SetBool("Dead", isDead);
        gameController.MonsterDie(this, selfDie);
    }

	protected virtual void Stop()
	{
		myRigid.velocity = Vector2.zero;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, size);
	}

    public virtual void flip()
    {
		if (isNoMove) return;
		
        if (transform.position.x > gameController.character.transform.position.x)
        {
            Vector2 localScale = animator.transform.localScale;
            localScale.x = -1;
            animator.transform.localScale = localScale;
        }
        else
        {
            Vector2 localScale = animator.transform.localScale;
            localScale.x = 1;
            animator.transform.localScale = localScale;
        }
    }

    #region State Machine Method

    protected virtual void InitializationStateMachine()
	{
		if(_stateMachine != null)
		{
			IdleState();
		}
		else
		{
            _stateMachine = new StateMachine();
			_idleState = new MonsterIdle(this, _stateMachine);
			_moveState = new MonsterMove(this, _stateMachine);
			_attackState = new MonsterAttack(this, _stateMachine);
			_stateMachine.Init(_idleState);
		}
	}

    public virtual void IdleState()
	{
		if(isIdle) return;
		_stateMachine.ChangeState(_idleState);
	}

	public virtual void MoveState()
	{
		flip();
		if(isMove) return;
        _stateMachine.ChangeState(_moveState);
	}

	public virtual void AttackState()
	{
		flip();
		if(isAttack) return;
        _stateMachine.ChangeState(_attackState);
	}

    #endregion

}