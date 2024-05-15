using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Controllers;
using _App.Scripts.Views.InGame.Skills;
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

	[SerializeField] private Transform circleAttackRange;

	[FormerlySerializedAs("a"),SerializeField] private int asd= 5;
	public int b;
	
	public float abc = 2.5f;

	public float sizeBase;

	[HideInInspector]
	public Rigidbody2D myRigid;

	public Animator animator;
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

	public List<Skill> listSkills = new List<Skill>();
	public List<Skill> listSkillCooldown = new List<Skill>();

	public bool IsAlive => model.currentHealthPoint > 0;
	public bool IsMove => _stateMachine.currentState == _moveSm;

	public Action<float> isCharacterMoving;

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
		circleAttackRange.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360)
			.SetEase(Ease.Linear)
			.SetLoops(-1, LoopType.Restart);

		AddDataBinding("fieldCharacter-moveSpeedValue", animator, (control, e) =>
			{
				control.SetFloat("SpeedMul", model.moveSpeed / 2.5f);
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.moveSpeed), model)
		);

		AddDataBinding("fieldCharacter-attackRangeValue", circleAttackRange, (control, e) =>
			{
				control.localScale = Vector3.one * model.attackRange;
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackRange),model)
		);
		
		
	}

	public void Init(CharacterStat statInit)
	{
		
		app.models.characterModel = new CharacterModel(
			statInit.moveSpeed.BaseValue,
			statInit.maxHealth.BaseValue,
			statInit.attackRange.BaseValue,
			statInit.attackDamage.BaseValue,
			statInit.itemAttractionRange.BaseValue,
			statInit.armor.BaseValue
		);
		stat = statInit;
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
		if (!IsAlive) return;
		damage = MinusDamage(damage);
		model.currentHealthPoint -= damage;
		GameObject text = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
		text.GetComponent<TextPopup>().Create(damage.ToString(), TextPopupType.MonsterDamage);

		if (!IsAlive) Die();
	}
	

	public void AddHealth(float value)
	{
		model.currentHealthPoint += value; 
	}

	public void AddProactiveSkill(SkillData skillData)
	{
		var skill = GetSkill(skillData.name);
		if(skill != null)
		{
			skill.UpLevel();
			if(skill.level >= 6)
			{
				gameController.map.RemoveSkill(skill.skillName);
			}
			return;
		}
		switch (skillData.type)
		{
			case SkillType.Active:
				{
					Skill skillIns;
					switch (skillData.name)
					{
						case SkillName.Fireball:
							skillIns = new FireBall();
							break;
						case SkillName.ThunderStrike: 
							skillIns = new ThunderStrike();
							break;
						default:
							skillIns = new ProactiveSkill();
							break;
					}
					skillIns.Init(skillData);
					listSkills.Add(skillIns);
				}
				break;
			case SkillType.Buff:
				if (skillData.name == SkillName.Food)
				{
					model.currentHealthPoint += model.currentHealthPoint * 20 / 100;
				}
				break;
		}
	}

	public Skill GetSkill(SkillName skillName)
	{
		foreach(var skill in listSkills)
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

	private int MinusDamage(int dmg)
	{
		return dmg * (100 - model.armor) / 100;
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
		foreach(var skill in listSkillCooldown)
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
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, sizeBase);
		Gizmos.DrawWireSphere(transform.position, abc);
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}