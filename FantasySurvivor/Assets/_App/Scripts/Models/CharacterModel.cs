using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class CharacterModel : Model<GameApp>
{
	public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(CharacterModel) + ".dataChanged");
	public CharacterModel(float maxHp, float moveSpeed, float attackDamage, float itemAttractionRange, float attackRange, int armor, float regen, int shield) : base(dataChangedEvent)
	{
		this.currentHealthPoint = maxHp;
		this.maxHealthPoint = maxHp;
		this.moveSpeed = moveSpeed;
		this._attackDamage = attackDamage;
		this._itemAttractionRange = itemAttractionRange;
		this.attackRange = attackRange;
		this.armor = armor;
		this.regen = regen;
        this.shield = shield;
	}

    public CharacterModel() : base(dataChangedEvent)
    {

    }

    private float _currentHealthPoint;

    private float _maxHealthPoint;

    private float _moveSpeed;

    private float _attackDamage;

    private float _itemAttractionRange;

    private float _attackRange;
    
	private int _armor;
	
	private float _regen;

    private int _shield;
    public int shield
    {
        get => _shield;
        set
        {
            if (_shield.Equals(value)) return;
            _shield = Mathf.Clamp(value, 0, value); 
            RaiseDataChanged(nameof(shield));
        }
    }
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
        set
        {
            if (currentHealthPoint.Equals(value)) return;
            _currentHealthPoint = Mathf.Clamp(value, 0, maxHealthPoint);
            RaiseDataChanged(nameof(currentHealthPoint));
        }
    }

    public float maxHealthPoint
    {
        get => _maxHealthPoint;
        set
        {
            if (maxHealthPoint.Equals(value)) return;
            var currentMaxHp = _maxHealthPoint;
            _maxHealthPoint = value;
            RaiseDataChanged(nameof(maxHealthPoint));

            currentHealthPoint += (value - currentMaxHp);
        }
    }

    public float attackDamage
    {
        get => _attackDamage;
        set
        {
            if (attackDamage.Equals(value)) return;
            _attackDamage = value;
            RaiseDataChanged(nameof(attackDamage));
        }
    }

    public float itemAttractionRange
    {
        get => _itemAttractionRange;
        set
        {
            if (itemAttractionRange.Equals(value)) return;
            _itemAttractionRange = value;
            RaiseDataChanged(nameof(itemAttractionRange));
        }
    }
    public float attackRange
    {
        get => _attackRange;
        set
        {
            if (attackRange.Equals(value)) return;
            _attackRange = value;
			RaiseDataChanged(nameof(attackRange));
		}
	}
	public int armor
	{
		get => _armor;
		set {
			if(armor.Equals(value)) return;
			_armor = value;
			RaiseDataChanged(nameof(armor));
		}
	}
	
	public float regen
	{
		get => _regen;
		set {
			if(regen.Equals(value)) return;
			_regen = value;
			RaiseDataChanged(nameof(regen));
		}
	}

	public void AddStatFormItemEquip(float maxHp, float moveSpeed, float attackDamage, float itemAttractionRange, float attackRange, int armor)
    {
        this.maxHealthPoint += maxHp;
        this.moveSpeed += moveSpeed;
        this._attackDamage += attackDamage;
        this._itemAttractionRange += itemAttractionRange;
        this.attackRange += attackRange;
        this._armor += armor;
    }
    public void Revive()
    {
        currentHealthPoint = maxHealthPoint;
    }
}