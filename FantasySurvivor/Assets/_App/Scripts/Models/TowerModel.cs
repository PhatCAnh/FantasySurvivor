using ArbanFramework.MVC;
namespace FantasySurvivor
{
	public class TowerModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase( nameof(TowerModel) + ".dataChanged" );

		public TowerModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}
		
		public TowerModel() : base(dataChangedEvent)
		{
			
		}

		public TowerModel(int hp, float attackSpeed, int attackDamage, float attackRange) : base(dataChangedEvent)
		{
			currentHealthPoint = hp;
			maxHealthPoint = hp;
			this.attackSpeed = attackSpeed;
			this.attackDamage = attackDamage;
			this.attackRange = attackRange;

			level = 1;
			exp = 0;
			maxExp = app.configs.dataUpStatTowerInGame.GetConfig(1).maxExp;
		}

		private int _currentHealthPoint;
		private int _maxHealthPoint;
		private int _attackDamage;
		private float _attackSpeed;
		private float _attackRange;
		private int _exp;
		private int _maxExp;
		private int _level;
		
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
		
		public int attackDamage
		{
			get => _attackDamage;
			set {
				if(attackDamage == value) return;
				_attackDamage = value;
				RaiseDataChanged(nameof(attackDamage));
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
		
		public int exp
		{
			get => _exp;
			set {
				if(exp.Equals(value)) return;
				_exp = value;
				if(_exp >= maxExp)
				{
					maxExp = app.configs.dataUpStatTowerInGame.GetConfig(level).maxExp;
					_exp = 0;
					level++;
				}
				RaiseDataChanged(nameof(exp));
			}
		}
		
		public int maxExp
		{
			get => _maxExp;
			set {
				if(maxExp.Equals(value)) return;
				_maxExp = value;
				RaiseDataChanged(nameof(maxExp));
			}
		}
		
		public int level
		{
			get => _level;
			set {
				if(level.Equals(value)) return;
				_level = value;
				RaiseDataChanged(nameof(level));
			}
		}

		public void AddMaxHealth(int value)
		{
			maxHealthPoint += value;
			currentHealthPoint += value;
		}
	}
}