using System;
using ArbanFramework.Config;
using DataConfig;
namespace _App.Scripts.Configs
{
	public class DataStatTowerConfig : IConfigItem
	{
		public string TowerType { get; private set; }
		public int AttackDamage { get; private set; }
		public float AttackSpeed { get; private set; }
		public float AttackRange { get; private set; }
		public int CriticalRate { get; private set; }
		public int CriticalDamage { get; private set; }
		public float Health { get; private set; }
		
		public float RegenHp { get; private set; }

		public string GetId()
		{
			return TowerType;
		}

		public void OnReadImpl(IConfigReader reader)
		{
			TowerType = reader.ReadString();
			AttackDamage = reader.ReadInt();
			AttackSpeed = reader.ReadFloat();
			AttackRange = reader.ReadFloat();
			CriticalRate = reader.ReadInt();
			CriticalDamage = reader.ReadInt();
			Health = reader.ReadFloat();
			RegenHp = reader.ReadFloat();
		}
	}

	public class DataStatTowerConfigTable : Configs<DataStatTowerConfig>
	{
		public override string FileName => nameof(DataStatTowerConfig);

		public DataStatTowerConfig GetConfig(TowerType type)
		{
			return GetConfig(type.ToString());
		}
		
		public float GetConfigStat(TowerType typeTower, TypeStatTower type)
		{
			var data = GetConfig(typeTower.ToString());
			return type switch
			{
				TypeStatTower.AttackDamage => data.AttackDamage,
				TypeStatTower.AttackRange => data.AttackRange,
				TypeStatTower.AttackSpeed => data.AttackSpeed,
				TypeStatTower.Health => data.Health,
				TypeStatTower.CriticalRate => data.CriticalRate,
				TypeStatTower.CriticalDamage => data.CriticalDamage,
				TypeStatTower.RegenHp => data.RegenHp,
				_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
			};
		}
	}
}