using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using ArbanFramework.StateMachine;
using DG.Tweening;
using FantasySurvivor;
using MR.CharacterState;
using MR.CharacterState.Tower;
using Popup;
using Sirenix.OdinInspector;
using Stat;
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
	public TowerStat stat { get; protected set; }
	public bool isAlive => model.currentHealthPoint > 0;
	public GameController gameController => Singleton<GameController>.instance;
	public bool isIdle => _stateMachine.currentState == _idleState;
	public bool isAttack => _stateMachine.currentState == _attackState;

	private StateMachine _stateMachine;
	private TowerIdle _idleState;
	private TowerAttack _attackState;

	private readonly Cooldown _cooldownRegen = new Cooldown();

	public void Init(TowerStat towerStat)
	{
		this.stat = towerStat;
		this.model = new TowerModel(
			Mathf.RoundToInt(stat.health.BaseValue),
			stat.ats.BaseValue,
			Mathf.RoundToInt(stat.atk.BaseValue),
			stat.atr.BaseValue,
			Mathf.RoundToInt(stat.critRate.BaseValue),
			Mathf.RoundToInt(stat.critDmg.BaseValue),
			stat.regenHp.BaseValue
		);
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
				if(model.attackRange > 10)
				{
					var value = model.attackRange % 10;
					Camera.main.orthographicSize += value * 0.2f;
				}
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackRange), model)
		);

		skinAttackRange.DORotate(new Vector3(0, 0, -360), 7.5f, RotateMode.FastBeyond360)
			.SetLoops(-1, LoopType.Incremental)
			.SetEase(Ease.Linear);
		//gameObject.AddComponent<SharkSkill>();
	}

	protected virtual void Update()
	{
		if(gameController.isStop) return;
		var time = Time.deltaTime;
		Regen(time);
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

	public float GetBaseStat(TypeStatTower type)
	{
		return type switch
		{
			TypeStatTower.AttackDamage => stat.atk.BaseValue,
			TypeStatTower.AttackRange => stat.atr.BaseValue,
			TypeStatTower.AttackSpeed => stat.ats.BaseValue,
			TypeStatTower.Health => stat.health.BaseValue,
			TypeStatTower.CriticalRate => stat.critRate.BaseValue,
			TypeStatTower.CriticalDamage => stat.critDmg.BaseValue,
			TypeStatTower.RegenHp => stat.regenHp.BaseValue,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}

	private void Regen(float deltaTime)
	{
		_cooldownRegen.Update(deltaTime);
		if(_cooldownRegen.isFinished)
		{
			model.currentHealthPoint += model.regenHp;
			_cooldownRegen.Restart(1);
		}
	}

	private void Die()
	{
		//gameController.CharacterDie(this);
	}

	public void Attack()
	{
		if(target == null) return;
		var arrowIns = Instantiate(arrow);
		//arrowIns.Init(this);
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
		skinAttackRange.DOKill();
	}
}