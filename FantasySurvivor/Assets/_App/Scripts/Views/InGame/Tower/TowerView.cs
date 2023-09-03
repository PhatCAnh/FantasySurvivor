using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using FantasySurvivor;
using MR.CharacterState;
using MR.CharacterState.Tower;
using Sirenix.OdinInspector;
using UnityEngine;

public class TowerView : ObjectRPG
{
	public SpriteRenderer skinBase;

	public Animator animator;

	public Transform weapon;

	public Transform firePoint;

	public BulletView arrow;

	public float sizeBase;
	
	public Monster target { set; get; }
	public TowerModel model { get; protected set; }

	public bool isAlive => model.currentHealthPoint > 0;
	public GameController gameController => Singleton<GameController>.instance;

	public bool isIdle => _stateMachine.currentState == _idleState;
	public bool isAttack => _stateMachine.currentState == _attackState;

	private StateMachine _stateMachine;
	private TowerIdle _idleState;
	private TowerAttack _attackState;

	public void Init()
	{
		model = new TowerModel(100, 1f, 5, 10f);
	}

	protected override void OnViewInit()
	{
		base.OnViewInit();
		if(_stateMachine == null)
		{
			_stateMachine = new StateMachine();
			_idleState = new TowerIdle(this, _stateMachine);
			_attackState = new TowerAttack(this, _stateMachine);
			_stateMachine.Init(_idleState);
		}
		else
		{
			IdleState();
		}
		AddDataBinding("fieldTower-attackSpeedValue", this, (control, e) =>
			{
				animator.SetFloat("AttackSpeed", model.attackSpeed);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackSpeed), model)
		);
	}

	protected virtual void Update()
	{
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);
	}

	protected virtual void FixedUpdate()
	{
		if(gameController.isStop) return;
		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	public void TakeDamage(int damage)
	{
		model.currentHealthPoint -= damage;
		if(!isAlive) Die();
	}
	
	private void Die()
	{
		gameController.TowerDie(this);
	}

	public void Attack()
	{
		var arrowIns = Instantiate(arrow);
		arrowIns.Init(this, 15f);
	}

	public void IdleState()
	{
		if(isIdle) return;
		_stateMachine.ChangeState(_idleState);
	}

	public void AttackState()
	{
		if(isAttack) return;
		_stateMachine.ChangeState(_attackState);
	}
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, sizeBase);
	}
}