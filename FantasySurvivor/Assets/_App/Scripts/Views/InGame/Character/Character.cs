using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.Config;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using StateMachine = ArbanFramework.StateMachine.StateMachine;

public class Character : ObjectRPG
{
	public Transform skinAttackRange;

	public SpriteRenderer skinBase;

	public float sizeBase;

	[HideInInspector]
	public Rigidbody2D myRigid;

	public Animator animator;
	public Monster target { set; get; }
	public CharacterModel model => app.models.characterModel;

	public CharacterStat stat { get; private set; }

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

	public List<Skill> proactiveSkills = new List<Skill>();

	public bool IsAlive => model.currentHealthPoint > 0;
	public bool IsMove => _stateMachine.currentState == _moveSm;

	private Vector2 _direction = Vector2.zero;

	private StateMachine _stateMachine;
	private CharacterIdle _idleSm;
	private CharacterMove _moveSm;

	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		if(_stateMachine == null)
		{
			_stateMachine = new StateMachine();
			_idleSm = new CharacterIdle(this, _stateMachine);
			_moveSm = new CharacterMove(this, _stateMachine);
			_stateMachine.Init(_idleSm);
		}
		else
		{
			IdleState();
		}

		myRigid = GetComponent<Rigidbody2D>();

		AddDataBinding("fieldCharacter-moveSpeedValue", animator, (control, e) =>
			{
				control.SetFloat("SpeedMul", model.moveSpeed / 2.5f);
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackSpeed), model)
		);

		AddDataBinding("fieldCharacter-attackRangeValue", skinAttackRange, (control, e) =>
			{
				control.localScale = model.attackRange / 10 * Vector3.one;
				if(model.attackRange > 10)
				{
					var value = model.attackRange % 10;
					Camera.main.orthographicSize += value * 0.2f;
				}
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackRange), model)
		);

		skinAttackRange.DORotate(new Vector3(0, 0, -360), 7.5f, RotateMode.FastBeyond360)
			.SetLoops(-1, LoopType.Incremental)
			.SetEase(Ease.Linear);


	}

	public void Init(CharacterStat statInit)
	{
		app.models.characterModel = new CharacterModel(
			statInit.moveSpeed.BaseValue,
			statInit.health.BaseValue,
			statInit.attackRange.BaseValue,
			statInit.attackDamage.BaseValue
		);
	}

	private void Update()
	{
		if(gameController.isStop) return;
		Controlled(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);

		HandlePhysicUpdate();
		HandleProactiveSkill(Time.deltaTime);
	}

	private void FixedUpdate()
	{
		if(gameController.isStop) return;

		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

	private void LateUpdate()
	{
		var position = transform.position;
		Camera.main.transform.position = new Vector3(position.x, position.y, -1);
	}

	public void Controlled(Vector2 moveForce)
	{
		moveDirection = moveForce;
	}

	public void TakeDamage(int damage)
	{
		if(!IsAlive) return;
		model.currentHealthPoint -= damage;
		Singleton<PoolTextPopup>.instance.GetObjectFromPool(transform.position, damage.ToString(), TextPopupType.MonsterDamage);
		if(!IsAlive) Die();
	}

	public void AddHealth(float value)
	{
		model.currentHealthPoint += value; 
	}

	public void AddProactiveSkill(SkillData skillData)
	{
		switch (skillData.type)
		{
			case SkillType.Proactive:
				var skill = GetSkill(skillData.name);
				if(skill != null)
				{
					skill.UpLevel();
					if(skill.level >= 6)
					{
						gameController.map.RemoveSkill(skill.skillName);
					}
				}
				else
				{
					var proactiveSkill = new ProactiveSkill();
					proactiveSkill.Init(skillData);
					proactiveSkills.Add(proactiveSkill);
				}
				break;
			case SkillType.Passive:
				break;
			case SkillType.Buff:
				if(skillData.name == SkillName.Food)
				{
					model.currentHealthPoint += model.currentHealthPoint * 20 / 100;
				}
				break;

		}
	}

	public Skill GetSkill(SkillName skillName)
	{
		foreach(var skill in proactiveSkills)
		{
			if(skill.skillName.Equals(skillName))
			{
				return skill;
			}
		}
		return null;
	}

	private void Die()
	{
		gameController.CharacterDie(this);
	}

	private void HandlePhysicUpdate()
	{

		if(moveDirection == Vector2.zero)
			IdleState();
		else
			MoveState();

		SetAnimation(moveDirection, idleDirection);
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private void HandleProactiveSkill(float deltaTime)
	{
		foreach(var skill in proactiveSkills)
		{
			skill.CoolDownSkill(deltaTime);
		}
	}

	private void SetAnimation(Vector2 dir, Vector2 idleDirection)
	{
		animator.SetFloat("Speed", dir.normalized.magnitude);
		animator.SetFloat("Horizontal", idleDirection.x);
		animator.SetFloat("Vertical", idleDirection.y);
	}

	#region State Machine Method

	public void IdleState() => _stateMachine.ChangeState(_idleSm);

	public void MoveState()
	{
		if(IsMove) return;
		_stateMachine.ChangeState(_moveSm);
	}

	#endregion

	protected override void OnDestroy()
	{
		base.OnDestroy();
		skinAttackRange.DOKill();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, sizeBase);
	}
}