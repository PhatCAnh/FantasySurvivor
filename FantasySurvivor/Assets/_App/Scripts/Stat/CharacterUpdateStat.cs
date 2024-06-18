using ArbanFramework;
namespace FantasySurvivor
{
	public class CharacterUpdateStat
	{
		public StatModifierFloat maxHealth;
		
		public StatModifierFloat moveSpeed;
		
		public StatModifierFloat attackDamage;
		
		public StatModifierFloat attackRange;
		
		public StatModifierFloat itemAttractionRange;
		
		public StatModifierInt armor;

		public Cooldown cdTime;

        public StatModifierFloat shield;

        public CharacterUpdateStat(StatModifierType type, float maxH, float ms, float ad, float ar, float itemR, int armor, float shield, float duration)
        {
            this.maxHealth = new StatModifierFloat(maxH, type, maxHealth);
			this.moveSpeed = new StatModifierFloat(ms, type, moveSpeed);
			this.attackDamage = new StatModifierFloat(ad, type, attackDamage);
			this.attackRange = new StatModifierFloat(ar, type, attackRange);
			this.itemAttractionRange = new StatModifierFloat(itemR, type, itemAttractionRange);
			this.armor = new StatModifierInt(armor, type, armor);
            this.shield = new StatModifierFloat(shield, type, shield);
            cdTime = new Cooldown(duration);
		}
	}
}