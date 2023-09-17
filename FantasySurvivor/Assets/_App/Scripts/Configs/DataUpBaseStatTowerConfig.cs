using System.Collections.Generic;
using ArbanFramework.Config;
namespace DataConfig
{
	public class DataUpBaseStatTowerConfig : IConfigItem
	{
		public int level { get; private set; }

		public DataLevelConfig dataAttackDamage;
		public DataLevelConfig dataAttackRange;
		public DataLevelConfig dataAttackSpeed;
		public DataLevelConfig dataHealth;

		public string GetId()
		{
			return level.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			level = reader.ReadInt();
			var lineDelimiter = ';';
			var dataAttackDamageStr = reader.ReadString().Split(lineDelimiter);
			var dataAttackRangeStr = reader.ReadString().Split(lineDelimiter);
			var dataAttackSpeedStr = reader.ReadString().Split(lineDelimiter);
			var dataHealthStr = reader.ReadString().Split(lineDelimiter);

			dataAttackDamage = new DataLevelConfig(dataAttackDamageStr[0], dataAttackDamageStr[1]);
			dataAttackRange = new DataLevelConfig(dataAttackRangeStr[0], dataAttackRangeStr[1]);
			dataAttackSpeed = new DataLevelConfig(dataAttackSpeedStr[0], dataAttackSpeedStr[1]);
			dataHealth = new DataLevelConfig(dataHealthStr[0], dataHealthStr[1]);
		}
	}

	public class DataUpBaseStatTowerConfigTable : Configs<DataUpBaseStatTowerConfig>
	{

		public override string FileName => nameof(DataUpBaseStatTowerConfig);

		public DataUpBaseStatTowerConfig GetConfig(int level)
		{
			return GetConfig(level.ToString());
		}
	}
}