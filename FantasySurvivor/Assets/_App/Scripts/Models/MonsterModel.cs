using ArbanFramework.MVC;
using FantasySurvivor;

public class MonsterModel : Model<GameApp>
{
    public static EventTypeBase dataChangedEvent = new EventTypeBase( nameof(MonsterModel) + ".dataChanged" );
    public MonsterModel(EventTypeBase eventType) : base(dataChangedEvent)
    {
    }

    public MonsterModel() : base(dataChangedEvent)
    {
    }
    public MonsterModel(float moveSpeed, int healthPoint, int attackDamage, float attackSpeed, int exp) : base(dataChangedEvent)
    {
        this.moveSpeed = moveSpeed;
        this.currentHealthPoint = healthPoint;
        this.maxHealthPoint = healthPoint;
        this.attackDamage = attackDamage;
        this.attackSpeed = attackSpeed;
        this.exp = exp;
    }

    private int _attackDamage;
    
    private float _attackSpeed;
        
    private float _moveSpeed;
		
    private int _currentHealthPoint;
		
    private int _maxHealthPoint;

    private int _exp;

    public int attackDamage
    {
        get => _attackDamage;
        set {
            if(attackDamage != value)
            {
                _attackDamage = value;
                RaiseDataChanged(nameof(_attackDamage));
            }
        }
    }
    
    public float attackSpeed
    {
        get => _attackSpeed;
        set {
            if(!attackSpeed.Equals(value))
            {
                _attackSpeed = value;
                RaiseDataChanged(nameof(attackSpeed));
            }
        }
    }



    public float moveSpeed
    {
        get => _moveSpeed;
        set
        {
            if (!moveSpeed.Equals(value))
            {
                _moveSpeed = value;
                RaiseDataChanged(nameof(moveSpeed));
            }
        }
    }
		
    public int currentHealthPoint
    {
        get => _currentHealthPoint;
        set {
            if(currentHealthPoint == value) return;
            _currentHealthPoint = value;
            RaiseDataChanged(nameof(currentHealthPoint));
        }
    }
        
    public int maxHealthPoint
    {
        get => _maxHealthPoint;
        set {
            if(maxHealthPoint == value) return;
            _maxHealthPoint = value;
            RaiseDataChanged(nameof(maxHealthPoint));
        }
    }

    public int exp
    {
        get => _exp;
        set {
            if(exp == value) return;
            _exp = value;
            RaiseDataChanged(nameof(exp));
        }
    }
}
