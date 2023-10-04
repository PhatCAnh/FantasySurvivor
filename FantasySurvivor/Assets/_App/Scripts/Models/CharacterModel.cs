using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterModel : Model<GameApp>
{
	public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(CharacterModel) + ".dataChanged");
	public CharacterModel(float moveSpeed, float maxHp, float attackRange, float attackDamage) : base(dataChangedEvent)
	{
		this.currentHealthPoint = maxHp;
		this.maxHealthPoint = maxHp;
		this.moveSpeed = moveSpeed;
		this.attackRange = attackRange;
		this._attackDamage = attackDamage;
	}

	public CharacterModel() : base(dataChangedEvent)
	{
		
	}

	private float _currentHealthPoint;

	private float _maxHealthPoint;
	
	private float _moveSpeed;
	
	private float _attackDamage;
	private float _attackSpeed;
	private float _attackRange;

	public float moveSpeed
	{
		get => _moveSpeed;
		set {
			if(moveSpeed.Equals(value)) return;
			_moveSpeed = value;
			RaiseDataChanged(nameof(moveSpeed));
		}
	}

	public float currentHealthPoint
	{
		get => _currentHealthPoint;
		set {
			if(currentHealthPoint.Equals(value)) return;
			_currentHealthPoint = Mathf.Clamp(value, 0, maxHealthPoint);
			RaiseDataChanged(nameof(currentHealthPoint));
		}
	}
        
	public float maxHealthPoint
	{
		get => _maxHealthPoint;
		set {
			if(maxHealthPoint.Equals(value)) return;
			var currentMaxHp = _maxHealthPoint;
			_maxHealthPoint = value;
			RaiseDataChanged(nameof(maxHealthPoint));
				
			currentHealthPoint += (value - currentMaxHp);
		}
	}
	
	public float attackDamage
	{
		get => _attackDamage;
		set {
			if(attackDamage.Equals(value)) return;
			_attackDamage = value;
		}
	}
		
	public float attackSpeed
	{
		get => _attackSpeed;
		set {
			if(attackSpeed.Equals(value)) return;
			_attackSpeed = value;
			RaiseDataChanged(nameof(attackSpeed));
		}
	}

	public float attackRange
	{
		get => _attackRange;
		set {
			if(attackRange.Equals(value)) return;
			_attackRange = value;
			RaiseDataChanged(nameof(attackRange));
		}
	}
}