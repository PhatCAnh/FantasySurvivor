using ArbanFramework;
namespace FantasySurvivor
{
	public class MonsterStat
	{
		public StatFloat moveSpeed = new();
		public StatInt health = new();
		public StatInt attackDamage = new();
		public StatFloat attackSpeed = new();
		public StatFloat attackRange = new();
		public StatInt exp = new();

		public MonsterStat(float moveSpeed, int health, int attackDamage, float attackSpeed, float attackRange
						, int exp)
		{
			this.moveSpeed.BaseValue = moveSpeed;
			this.health.BaseValue = health;
			this.attackDamage.BaseValue = attackDamage;
			this.attackSpeed.BaseValue = attackSpeed;
			this.attackRange.BaseValue = attackRange;
			this.exp.BaseValue = exp;
		}
        public MonsterStat(float moveSpeed, int health, int attackDamage, float attackSpeed, float attackRange)
        {
            this.moveSpeed.BaseValue = moveSpeed;
            this.health.BaseValue = health;
            this.attackDamage.BaseValue = attackDamage;
            this.attackSpeed.BaseValue = attackSpeed;
            this.attackRange.BaseValue = attackRange;
        }
    }
	
	public class TowerStat
	{
		public StatFloat atk = new();
		public StatFloat ats = new();
		public StatFloat atr = new();
		public StatFloat critRate = new();
		public StatFloat critDmg = new();
		public StatFloat health = new();
		public StatFloat regenHp = new();

		public TowerStat(
			int atk,
			float ats,
			float atr,
			int critRate,
			float critDmg,
			float health,
			float regenHp)
		{
			this.atk.BaseValue = atk;
			this.ats.BaseValue = ats;
			this.atr.BaseValue = atr;
			this.critRate.BaseValue = critRate;
			this.critDmg.BaseValue = critDmg;
			this.health.BaseValue = health;
			this.regenHp.BaseValue = regenHp;
			
		}
	}
}