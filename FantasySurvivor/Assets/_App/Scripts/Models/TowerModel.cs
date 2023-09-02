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

		public TowerModel(int hp, float attackSpeed, int attackDamage) : base(dataChangedEvent)
		{
			currentHealthPoint = hp;
			maxHealthPoint = hp;
			this.attackSpeed = attackSpeed;
			this.attackDamage = attackDamage;
		}

		private int _currentHealthPoint;
		private int _maxHealthPoint;
		private float _attackSpeed;
		private int _attackDamage;
		
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

		public float attackSpeed
		{
			get => _attackSpeed;
			set {
				if(attackSpeed.Equals(value)) return;
				_attackSpeed = value;
				RaiseDataChanged(nameof(attackSpeed));
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
	}
}