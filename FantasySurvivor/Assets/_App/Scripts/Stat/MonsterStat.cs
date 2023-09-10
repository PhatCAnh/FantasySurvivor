using ArbanFramework;
namespace Stat
{
	public class MonsterStat
	{
		public SkinMonsterType skinMonsterType;
		public MonsterType monsterType;
		public StatInt level;
		public StatFloat moveSpeed = new();
		public StatInt health = new();
		public StatInt attackDamage = new();
		public StatFloat attackSpeed = new();

		public MonsterStat(float moveSpeed, int health, int attackDamage, float attackSpeed)
		{
			this.moveSpeed.BaseValue = moveSpeed;
			this.health.BaseValue = health;
			this.attackDamage.BaseValue = attackDamage;
			this.attackSpeed.BaseValue = attackSpeed;
		}
	}
}