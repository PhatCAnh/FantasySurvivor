using ArbanFramework.Config;
namespace DataConfig
{
	public class DataLevelTowerConfig
	{
		public float value;
		public int price;

		public DataLevelTowerConfig()
		{
			
		}

		public DataLevelTowerConfig(float value, int price)
		{
			this.value = value;
			this.price = price;
		}
	}

	public class DataTowerInGameLevelUpConfig : IConfigItem
	{
		public int level { get; private set; }
		public DataLevelTowerConfig attackDamage { get; private set; }
		public DataLevelTowerConfig attackSpeed { get; private set; }
		public DataLevelTowerConfig attackRange { get; private set; }
		public DataLevelTowerConfig health { get; private set; }

		public string GetId()
		{
			return level.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			level = reader.ReadInt();

			var valueAtk = reader.ReadInt();
			var priceAtk = reader.ReadInt();
			attackDamage = new DataLevelTowerConfig(valueAtk, priceAtk);
			
			var valueAs = reader.ReadFloat();
			var priceAs = reader.ReadInt();
			attackSpeed = new DataLevelTowerConfig(valueAs, priceAs);

			var valueAr = reader.ReadFloat();
			var priceAr = reader.ReadInt();
			attackRange = new DataLevelTowerConfig(valueAr, priceAr);
			
			var valueHealth = reader.ReadInt();
			var priceHealth = reader.ReadInt();
			health = new DataLevelTowerConfig(valueHealth, priceHealth);
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
			return type switch
			{
				TypeStatTower.AttackDamage => data.attackDamage,
				TypeStatTower.AttackRange => data.attackRange,
				TypeStatTower.AttackSpeed => data.attackSpeed,
				TypeStatTower.Health => data.health,
			};
		}
	}


}