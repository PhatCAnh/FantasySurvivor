using System;
using System.Collections.Generic;
using System.Linq;
using _App.Scripts.Controllers;
using _App.Scripts.Views.InGame.Skills;
using _App.Scripts.Views.InGame.Skills.SkillMono;
using ArbanFramework;
using ArbanFramework.Config;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using static FantasySurvivor.Shark;
using StateMachine = ArbanFramework.StateMachine.StateMachine;

public class Character : ObjectRPG
{

	[SerializeField] private Transform circleAttackRange;

	[FormerlySerializedAs("a"), SerializeField] private int asd = 5;
	public int b;
	public float healingAmount;

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
    public List<SkillData> listSkillDataCurrents = new List<SkillData>();
    public List<Skill> listSkillCooldown = new List<Skill>();
	public List<CharacterUpdateStat> listUpdateStat = new List<CharacterUpdateStat>();

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
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackRange), model)
		);


	}

	public void Init(CharacterStat statInit)
	{

		stat = statInit;
	}

	private void Update()
	{
		if(gameController.isStop) return;
		Controlled(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);

		HandlePhysicUpdate();
		HandleProactiveSkill(time);
		HandleUpdateStat(time);
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
		damage = MinusDamage(damage);
		model.currentHealthPoint -= damage;
		GameObject text = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
		text.GetComponent<TextPopup>().Create(damage.ToString(), TextPopupType.MonsterDamage);

        if (!IsAlive)
        {
            Die();
        }
    }


	public void AddHealth(float value)
	{
		model.currentHealthPoint += value;
	}

	public void AddProactiveSkill(SkillData skillData)
	{
		UpdateStat(StatModifierType.Add, 10, 0, 0, 0, 0, 0, 4);

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
		listSkillDataCurrents.Add(skillData);
		gameController.CheckExistSkill();
		
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
					case SkillName.Waterball:
						skillIns = new WaterBallControl();
						break;
					// case SkillName.ThunderChanneling:
					// 	skillIns = new ThunderChanneling();
					// 	break;
					case SkillName.FireShield:
						skillIns = new FireShieldControl();
						break;
					case SkillName.Shark:
						skillIns = new Shark();
						break;
					case SkillName.PoisonBullet:
						skillIns = new Poisonball();
						break;
					case SkillName.Earthpunch:
						skillIns = new EarthPunch();
						break;
					case SkillName.SkyBoom:
						skillIns = new Skyboom();
						break;
					case SkillName.Boomerang:
						skillIns = new Boomerangl();
						break;
					case SkillName.SmilingFace:
						skillIns = new SmilingFacel();
						break;
					default:
						skillIns = new ProactiveSkill();
						break;
				}
				skillIns.Init(skillData);
				listSkills.Add(skillIns);
				break;
			}
			case SkillType.Buff:
				if(skillData.name == SkillName.Food)
				{
					model.currentHealthPoint += model.currentHealthPoint * 20 / 100;
				}
				break;
		}
	}
	//ham check exist skill
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

	public void UpdateStat(StatModifierType typeStat, float maxH, float ms, float ad, float ar, float itemR, int armor, float duration)
	{
		var updateStat = new CharacterUpdateStat(typeStat, maxH, ms, ad, ar, itemR, armor, duration);
		
		stat.maxHealth.AddModifier(updateStat.maxHealth);
		stat.moveSpeed.AddModifier(updateStat.moveSpeed);
		stat.attackDamage.AddModifier(updateStat.attackDamage);
		stat.attackRange.AddModifier(updateStat.attackRange);
		stat.itemAttractionRange.AddModifier(updateStat.itemAttractionRange);
		stat.armor.AddModifier(updateStat.armor);

		listUpdateStat.Add(updateStat);
		UpdateModel();
	}

	private void RemoveStat(CharacterUpdateStat statModifier)
	{
		stat.maxHealth.RemoveModifier(statModifier.maxHealth);
		stat.moveSpeed.RemoveModifier(statModifier.moveSpeed);
		stat.attackDamage.RemoveModifier(statModifier.attackDamage);
		stat.attackRange.RemoveModifier(statModifier.attackRange);
		stat.itemAttractionRange.RemoveModifier(statModifier.itemAttractionRange);
		stat.armor.RemoveModifier(statModifier.armor);

		listUpdateStat.Remove(statModifier);
		UpdateModel();
	}

	private void UpdateModel()
	{
		model.maxHealthPoint = stat.maxHealth.Value;
		model.moveSpeed = stat.moveSpeed.Value;
		model.attackDamage = stat.attackDamage.Value;
		model.itemAttractionRange = stat.itemAttractionRange.Value;
		model.attackRange = stat.attackRange.Value;
		model.armor = stat.armor.Value;
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

	private void HandleUpdateStat(float deltaTime)
	{
		foreach(var item in listUpdateStat.ToList())
		{
			item.cdTime.Update(deltaTime);
			if(item.cdTime.isFinished)
			{
				RemoveStat(item);
			}
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
		circleAttackRange.DOKill();
	}


	private void HandleHealing()
	{
		{
			model.healingTimer += Time.deltaTime;
			if(model.healingTimer >= model.healingCooldown && CheckHeal()) // Kiểm tra điều kiện trước khi hồi máu
			{
				model.healingTimer = 0f;
				AddHealth(model.maxHealthPoint * model.healingRate); // Hồi máu
				ActivateHealing();
			}
		}

	}

	public void ActivateHealing()
	{
		int roundedHealingRate = Mathf.RoundToInt(model.healingRate * 100);
		GameObject healingText = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
		healingText.GetComponent<TextPopup>().Create(roundedHealingRate.ToString(), TextPopupType.Healing);
	}

	public void DeactivateHealing()
	{
		model.isHealing = false;
		model.healingTimer = 0f;
	}

	private bool CheckHeal()
	{
		return model.currentHealthPoint < model.maxHealthPoint;
	}


    private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, sizeBase);
		Gizmos.DrawWireSphere(transform.position, abc);
		Gizmos.DrawWireSphere(transform.position, 1);
	}
}