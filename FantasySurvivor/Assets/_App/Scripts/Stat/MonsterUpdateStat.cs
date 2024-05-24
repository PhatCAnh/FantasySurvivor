using ArbanFramework;
namespace FantasySurvivor
{
	public class MonsterUpdateStat
	{
		public StatModifierInt maxHealth;

		public StatModifierFloat moveSpeed;

		public StatModifierInt attackDamage;

		public StatModifierFloat attackSpeed;

		public Cooldown cdTime;

		public MonsterUpdateStat(StatModifierType type, int maxH, float ms, int ad, float attackSpeed, float duration)
		{
			this.maxHealth = new StatModifierInt(maxH, type, maxHealth);
			this.moveSpeed = new StatModifierFloat(ms, type, moveSpeed);
			this.attackDamage = new StatModifierInt(ad, type, attackDamage);
			this.attackSpeed = new StatModifierFloat(attackSpeed, type, this.attackSpeed);
			cdTime = new Cooldown(duration);
		}
	}
}