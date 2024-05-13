using ArbanFramework;

namespace FantasySurvivor
{
	public class CharacterStat
	{
		public StatFloat moveSpeed = new();
		public StatFloat health = new();
		public StatFloat attackRange = new();
		public StatFloat attackDamage = new();
        public StatInt armor = new();

        public CharacterStat(float moveSpeed, float health, float attackRange, float attackDamage, int armor)
		{
			this.moveSpeed.BaseValue = moveSpeed;
			this.health.BaseValue = health;
			this.attackRange.BaseValue = attackRange;
			this.attackDamage.BaseValue = attackDamage;
            this.armor.BaseValue = armor;
        }
	}
}

