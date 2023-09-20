using ArbanFramework.Config;
namespace DataConfig
{
	public class DataLevelTowerConfig
	{
		public float value;
		public int price;

		public DataLevelTowerConfig(float value, int price)
		{
			this.value = value;
			this.price = price;
		}
	}

	public class DataTowerInGameLevelUpConfig : IConfigItem
	{
		public int level { get; private set; }
		public int attackDamage { get; private set; }
		public float attackSpeed { get; private set; }
		public float attackRange { get; private set; }
		public int health { get; private set; }
		public int price { get; private set; }

		public string GetId()
		{
			return level.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			level = reader.ReadInt();
			attackDamage = reader.ReadInt();
			attackSpeed = reader.ReadFloat();
			attackRange = reader.ReadFloat();
			health = reader.ReadInt();
			price = reader.ReadInt();
		}
	}

	public class DataTowerInGameLevelUpConfigTable : Configs<DataTowerInGameLevelUpConfig>
	{
		public override string FileName => nameof(DataTowerInGameLevelUpConfig);

		public DataTowerInGameLevelUpConfig GetConfig(int level)
		{
			return GetConfig(level.ToString());
		}

		public DataLevelTowerConfig GetConfigStat(int level, TypeStatTower type)
		{
			var data = GetConfig(level.ToString());
			float value = 0;
			switch (type)
			{
				case TypeStatTower.AttackDamage:
					value = data.attackDamage;
					break;
				case TypeStatTower.AttackRange:
					value = data.attackRange;
					break;
				case TypeStatTower.AttackSpeed:
					value = data.attackSpeed;
					break;
				case TypeStatTower.Health:
					value = data.health;
					break;
			}
			return new DataLevelTowerConfig(value, data.price);
		}
	}


}