using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using DG.Tweening;
using FantasySurvivor;
using MR.CharacterState;
using MR.CharacterState.Tower;
using Sirenix.OdinInspector;
using UnityEngine;

public class TowerView : ObjectRPG
{
	public Transform skinAttackRange;
	
	public SpriteRenderer skinBase;

	public Animator animator;

	public Transform weapon;

	public Transform firePoint;

	public BulletView arrow;

	public float sizeBase;
	
	public Monster target { set; get; }
	public TowerModel model { get; protected set; }
	
	public HealthBar healthBar { get; set; }

	public bool isAlive => model.currentHealthPoint > 0;
	public GameController gameController => Singleton<GameController>.instance;

	public bool isIdle => _stateMachine.currentState == _idleState;
	public bool isAttack => _stateMachine.currentState == _attackState;

	private StateMachine _stateMachine;
	private TowerIdle _idleState;
	private TowerAttack _attackState;

	public void Init(TowerModel modelInit, HealthBar healthBar)
	{
		this.model = modelInit;
		this.healthBar = healthBar;
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
		
		AddDataBinding("fieldTower-attackSpeedValue", animator, (control, e) =>
			{
				control.SetFloat("AttackSpeed", model.attackSpeed);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackSpeed), model)
		);
		AddDataBinding("fieldTower-attackRangeValue", skinAttackRange, (control, e) =>
			{
				control.localScale = model.attackRange / 10 * Vector3.one;
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackRange), model)
		);
		
		skinAttackRange.DORotate(new Vector3(0, 0, -360), 7.5f, RotateMode.FastBeyond360)
			.SetLoops(-1, LoopType.Incremental)
			.SetEase(Ease.Linear);
	}

	protected virtual void Update()
	{
		if(gameController.isStop) return;
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
		if(!isAlive) return;
		model.currentHealthPoint -= damage;
		Singleton<PoolTextPopup>.instance.GetObjectFromPool(transform.position, damage.ToString(), TextPopupType.MonsterDamage);
		if(!isAlive) Die();
	}
	
	private void Die()
	{
		gameController.TowerDie(this);
	}

	public void Attack()
	{
		var arrowIns = Instantiate(arrow);
		arrowIns.Init(this);
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

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Destroy(healthBar.gameObject);
		skinAttackRange.DOKill();
	}
}