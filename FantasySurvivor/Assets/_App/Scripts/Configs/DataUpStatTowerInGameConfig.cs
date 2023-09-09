using ArbanFramework.Config;
namespace Config
{
	public class DataUpStatTowerInGameConfig : IConfigItem
	{
		public class DataLevelConfig
		{
			public float value { get; private set; }
			public int price { get; private set; }

			public DataLevelConfig(string value, string price)
			{
				this.value = float.Parse(value);
				this.price = int.Parse(price);
			}
		}
		
		public int id { get; private set; }
		
		public int level { get; private set; }

		public DataLevelConfig dataAttackDamage;
		public DataLevelConfig dataAttackRange;
		public DataLevelConfig dataAttackSpeed;
		public DataLevelConfig dataHealth;
		
		public string GetId()
		{
			return id.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadInt();
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


			// var lines = mapStr.Split(lineDelimiter);
			// items = new ItemConfig[lines.Length];
			//
			// for (int i = 0; i < lines.Length; i++)
			// {
			// 	var strs = lines[i].Split(charDelimiter);
			// 	var itemId = int.Parse(strs[0]);
			// 	var position = new Vector2(int.Parse(strs[1]), int.Parse(strs[2]));
			// 	items[i] = new ItemConfig((ItemType)itemId, position);
			// }
		}
	}

	public class DataUpStatTowerInGameConfigTable : Configs<DataUpStatTowerInGameConfig>
	{
		public override string FileName => nameof(DataUpStatTowerInGameConfig);

		public DataUpStatTowerInGameConfig GetConfig(int id)
		{
			return GetConfig(id.ToString());
		}
	}
}