using ArbanFramework;

namespace FantasySurvivor
{
	public class MonsterStat
	{
		public StatFloat moveSpeed = new();

		public MonsterStat(float moveSpeed)
		{
			this.moveSpeed.BaseValue = moveSpeed;
		}
	}
}

