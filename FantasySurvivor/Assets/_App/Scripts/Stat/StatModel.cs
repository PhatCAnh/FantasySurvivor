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
	
	public class CharacterStat
	{
		public StatFloat maxHealth = new();
		public StatFloat moveSpeed = new();
		public StatFloat attackDamage = new();
		public StatFloat attackRange = new();
		public StatFloat itemAttractionRange = new();
		public StatInt armor = new();

		public CharacterStat(float health,float ms,  float ad, float itemRange, float ar, int armor)
		{
			this.moveSpeed.BaseValue = ms;
			this.maxHealth.BaseValue = health;
			this.attackRange.BaseValue = ar;
			this.attackDamage.BaseValue = ad;
			this.armor.BaseValue = armor;
			this.itemAttractionRange.BaseValue = itemRange;
		}
	}
}