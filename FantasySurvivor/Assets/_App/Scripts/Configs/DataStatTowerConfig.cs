using ArbanFramework.Config;
namespace _App.Scripts.Configs
{
	public class DataStatTowerConfig : IConfigItem
	{
		public string towerType { get; private set; }
		
		public int attackDamage { get; private set; }
		public float attackSpeed { get; private set; }
		public float attackRange { get; private set; }
		public int criticalRate { get; private set; }
		public int criticalDamage { get; private set; }
		
		public int health { get; private set; }


		public string GetId()
		{
			return towerType.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			towerType = reader.ReadString();
			attackDamage = reader.ReadInt();
			attackSpeed = reader.ReadFloat();
			attackRange = reader.ReadFloat();
			criticalRate = reader.ReadInt();
			criticalDamage = reader.ReadInt();
			health = reader.ReadInt();
		}
	}

	public class DataStatTowerConfigTable : Configs<DataStatTowerConfig>
	{
		public override string FileName => nameof(DataStatTowerConfig);

		public DataStatTowerConfig GetConfig(TowerType type)
		{
			return GetConfig(type.ToString());
		}
	}
}