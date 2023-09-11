using ArbanFramework;
namespace Stat
{
	public class MonsterStat
	{
		public StatFloat moveSpeed = new();
		public StatInt health = new();
		public StatInt attackDamage = new();
		public StatFloat attackSpeed = new();
		public StatFloat attackRange = new();

		public MonsterStat(float moveSpeed, int health, int attackDamage, float attackSpeed, float attackRange)
		{
			this.moveSpeed.BaseValue = moveSpeed;
			this.health.BaseValue = health;
			this.attackDamage.BaseValue = attackDamage;
			this.attackSpeed.BaseValue = attackSpeed;
			this.attackRange.BaseValue = attackRange;
		}
	}
}