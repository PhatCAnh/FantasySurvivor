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

		public TowerModel(int hp, float attackSpeed, int attackDamage, float attackRange,
				int criticalRate, int criticalDamage)
			: base(dataChangedEvent)
		{
			maxHealthPoint = hp;
			this.attackSpeed = attackSpeed;
			this.attackDamage = attackDamage;
			this.attackRange = attackRange;
			this.criticalRate = criticalRate;
			this.criticalDamage = criticalDamage;
			
			levelAd = 0;
			levelAr = 0;
			levelAs = 0;
			levelCr = 0;
			levelCd = 0;
			levelHealth = 0;
		}

		private float _currentHealthPoint;
		private float _maxHealthPoint;
		
		private int _attackDamage;
		private float _attackSpeed;
		private float _attackRange;
		private int _criticalRate;
		private int _criticalDamage;
		
		private int _levelAd;
		private int _levelAr;
		private int _levelAs;
		private int _levelCr;
		private int _levelCd;
		private int _levelHealth;
		
		public float currentHealthPoint
		{
			get => _currentHealthPoint;
			set {
				if(currentHealthPoint.Equals(value)) return;
				_currentHealthPoint = value;
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
		
		public int attackDamage
		{
			get => _attackDamage;
			set {
				if(attackDamage == value) return;
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
		
		public int criticalRate
		{
			get => _criticalRate;
			set {
				if(criticalRate.Equals(value)) return;
				_criticalRate = value;
			}
		}
		
		public int criticalDamage
		{
			get => _criticalDamage;
			set {
				if(criticalDamage.Equals(value)) return;
				_criticalDamage = value;
			}
		}

		public int levelAd
		{
			get => _levelAd;
			set {
				if(levelAd == value) return;
				_levelAd = value;
				RaiseDataChanged(nameof(levelAd));
			}
		}

		public int levelAr
		{
			get => _levelAr;
			set {
				if(levelAr == value) return;
				_levelAr = value;
				RaiseDataChanged(nameof(levelAr));
			}
		}

		public int levelAs
		{
			get => _levelAs;
			set {
				if(levelAs == value) return;
				_levelAs = value;
				RaiseDataChanged(nameof(levelAs));
			}
		}
		public int levelHealth
		{
			get => _levelHealth;
			set {
				if(levelHealth == value) return;
				_levelHealth = value;
				RaiseDataChanged(nameof(levelHealth));
			}
		}
		public int levelCr
		{
			get => _levelCr;
			set {
				if(levelCr == value) return;
				_levelCr = value;
				RaiseDataChanged(nameof(levelCr));
			}
		}
		
		public int levelCd
		{
			get => _levelCd;
			set {
				if(levelCd == value) return;
				_levelCd = value;
				RaiseDataChanged(nameof(levelCd));
			}
		}
	}
}