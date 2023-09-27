using System;
using System.Collections.Generic;
using ArbanFramework.Config;
namespace DataConfig
{
	public class DataTowerOutGameLevelUpConfig : IConfigItem
	{
		public int Level { get; private set; }
		public DataLevelTowerConfig AttackDamage { get; private set; }
		public DataLevelTowerConfig AttackSpeed { get; private set; }
		public DataLevelTowerConfig AttackRange { get; private set; }
		public DataLevelTowerConfig CriticalRate { get; private set; }
		public DataLevelTowerConfig CriticalDamage { get; private set; }
		public DataLevelTowerConfig Health { get; private set; }
		public DataLevelTowerConfig RegenHp { get; private set; }
		
		public string GetId()
		{
			return Level.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			Level = reader.ReadInt();
			
			var valueAtk = reader.ReadInt();
			var priceAtk = reader.ReadInt();
			AttackDamage = new DataLevelTowerConfig(valueAtk, priceAtk);
			
			var valueAs = reader.ReadFloat();
			var priceAs = reader.ReadInt();
			AttackSpeed = new DataLevelTowerConfig(valueAs, priceAs);

			var valueAr = reader.ReadFloat();
			var priceAr = reader.ReadInt();
			AttackRange = new DataLevelTowerConfig(valueAr, priceAr);
			
			var valueCd = reader.ReadFloat();
			var priceCd = reader.ReadInt();
			CriticalDamage = new DataLevelTowerConfig(valueCd, priceCd);
			
			var valueCr = reader.ReadFloat();
			var priceCr = reader.ReadInt();
			CriticalRate = new DataLevelTowerConfig(valueCr, priceCr);
			
			var valueHealth = reader.ReadFloat();
			var priceHealth = reader.ReadInt();
			Health = new DataLevelTowerConfig(valueHealth, priceHealth);
			
			var valueRegenHp = reader.ReadFloat();
			var priceRegenHp = reader.ReadInt();
			RegenHp = new DataLevelTowerConfig(valueRegenHp, priceRegenHp);
		}
	}

	public class DataTowerOutGameLevelUpConfigTable : Configs<DataTowerOutGameLevelUpConfig>
	{

		public override string FileName => nameof(DataTowerOutGameLevelUpConfig);

		public DataTowerOutGameLevelUpConfig GetConfig(int level)
		{
			return GetConfig(level.ToString());
		}

		public DataLevelTowerConfig GetConfigStat(int level, TypeStatTower type)
		{
			var data = GetConfig(level.ToString());
			return type switch
			{
				TypeStatTower.AttackDamage => data.AttackDamage,
				TypeStatTower.AttackRange => data.AttackRange,
				TypeStatTower.AttackSpeed => data.AttackSpeed,
				TypeStatTower.Health => data.Health,
				TypeStatTower.CriticalRate => data.CriticalRate,
				TypeStatTower.CriticalDamage => data.CriticalDamage,
				TypeStatTower.RegenHp => data.RegenHp,
			};
		}
	}
}