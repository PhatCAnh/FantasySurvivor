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
using UnityEngine.TextCore.Text;
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

	public List<CharacterUpdateStat> listUpdateStat = new List<CharacterUpdateStat>();

	public List<StatusEffect> listStatusEffect = new List<StatusEffect>();

	public bool IsAlive => model.currentHealthPoint > 0;
	public bool IsMove => _stateMachine.currentState == _moveSm;

	public Action<float> isCharacterMoving;

	private Vector2 _direction = Vector2.zero;

	private Cooldown _cdRegen;

	private StateMachine _stateMachine;
	private CharacterIdle _idleSm;
	private CharacterMove _moveSm;

	private GameController gameController => Singleton<GameController>.instance;
	private SkillController skillController => Singleton<SkillController>.instance;

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

	public void Init()
	{
		/*foreach (var item in app.models.dataPlayerModel.ListItemEquipped)
		{
			var itemData = ArbanFramework.Singleton<ItemController>.instance.GetDataItem(item.id, item.rank).dataConfig;
			switch (GetTypeStatItemEquip(itemData.type))
			{
				case "Atk":
					var dataAtk = GetDataStat("Atk", item.rank);
					var valueAtk = itemData.baseValue + dataAtk.Item2 * (item.level - 1);
					model.attackDamage += valueAtk;
					break;
				case "Health":
					var dataHealth = GetDataStat("Health", item.rank);
					var valueHealth = itemData.baseValue + dataHealth.Item2 * (item.level - 1);
					model.maxHealthPoint += valueHealth;
					break;
			}
		}*/

		stat = new CharacterStat(
			model.maxHealthPoint,
			model.moveSpeed,
			model.attackDamage,
			model.itemAttractionRange,
			model.attackRange,
			model.armor,
			model.shield
		);
		
		_cdRegen = new Cooldown();
		_cdRegen.Restart(1);
	}

	private void Update()
	{
		if(gameController.isStop) return;
		var time = Time.deltaTime;
		_stateMachine.currentState.LogicUpdate(time);
		HandlePhysicUpdate();
		HandleProactiveSkill(time);
		HandleUpdateStat(time);
		HandleUpdateHealing(time);
		HandleStatusEffect(time);
    }

    private void FixedUpdate()
	{
		if(gameController.isStop) return;

		_stateMachine.currentState.PhysicUpdate(Time.fixedTime);
	}

    private void HandleStatusEffect(float deltaTime)
    {
        foreach (var item in listStatusEffect.ToList())
        {
            item.Cooldown(deltaTime);
        }
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
		if (model.isInvincible) return;
		 if(!IsAlive) return;
		 if (model.armor != 0)
		 {
			 damage -= Convert.ToInt32(damage * DamageReductionByArmor());
		 }
		 var currentShield = model.shield;
		 model.shield -= damage;
		 damage = Mathf.RoundToInt(damage - currentShield);
		 if(damage < 0) damage = 0;
		
		model.currentHealthPoint -= damage;

		GameObject text = Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position);
		text.GetComponent<TextPopup>().Create(damage, TextPopupType.Red);

		if(!IsAlive)
		{
			Die();
		}
	}


	public void AddHealth(float value)
	{
		model.currentHealthPoint += value;
		Singleton<PoolController>.instance.GetObject(ItemPrefab.TextPopup, transform.position).TryGetComponent(out TextPopup healingText);
		healingText.Create(value, TextPopupType.Healing);
	}

	public void AddProactiveSkill(SkillId id)
	{
		skillController.ChoiceSkillInGame(id);
	}

	public void UpdateStat(StatModifierType typeStat, float maxH, float ms, float ad, float ar, float itemR, int armor, int shield, float duration)
	{
		var updateStat = new CharacterUpdateStat(typeStat, maxH, ms, ad, ar, itemR, armor, shield, duration);

		stat.maxHealth.AddModifier(updateStat.maxHealth);
		stat.moveSpeed.AddModifier(updateStat.moveSpeed);
		stat.attackDamage.AddModifier(updateStat.attackDamage);
		stat.attackRange.AddModifier(updateStat.attackRange);
		stat.itemAttractionRange.AddModifier(updateStat.itemAttractionRange);
		stat.armor.AddModifier(updateStat.armor);

		listUpdateStat.Add(updateStat);
		UpdateModel();
	}
    private float DamageReductionByArmor()
	{
		var delta = 0.06f;
		var armor = model.armor;
		return 1 - delta * armor / (1 + delta * Mathf.Abs(armor));
	}


    private void RemoveStat(CharacterUpdateStat statModifier)
    {
        stat.maxHealth.RemoveModifier(statModifier.maxHealth);
        stat.moveSpeed.RemoveModifier(statModifier.moveSpeed);
        stat.attackDamage.RemoveModifier(statModifier.attackDamage);
        stat.attackRange.RemoveModifier(statModifier.attackRange);
        stat.itemAttractionRange.RemoveModifier(statModifier.itemAttractionRange);
        stat.armor.RemoveModifier(statModifier.armor);
        stat.shield.RemoveModifier(statModifier.shield);

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
        model.shield = stat.shield.Value;
    }

	private void Die()
	{
		gameController.CharacterDie(this);
	}

	private void HandleUpdateHealing(float deltaTime)
	{
		if(model.currentHealthPoint >= model.maxHealthPoint || model.regen == 0) return;

		_cdRegen.Update(deltaTime);
		if(_cdRegen.isFinished) // Kiểm tra điều kiện trước khi hồi máu
		{
			_cdRegen.Restart(1);
			AddHealth(model.regen); // Hồi máu
		}
	}

	// ReSharper disable Unity.PerformanceAnalysis
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
		foreach(var skill in skillController.listSkills)
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