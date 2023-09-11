using ArbanFramework.Config;
namespace DataConfig
{
	public class DataChapterConfig : IConfigItem
	{
		public int id { get; private set; }

		public int chapter { get; private set; }

		public WaveConfig[] waves { get; private set; }
		public class WaveConfig
		{
			public int timeStart { get; private set; }
			public int duration { get; private set; }
			public int coolDown { get; private set; }

			public MonsterType monsterType { get; private set; }

			public int adMonster { get; private set; }
			public int healthMonster { get; private set; }
			public int coinMonster { get; private set; }

			public WaveConfig(int timeStart, int duration, int coolDown, MonsterType monsterType, int adMonster, int healthMonster, int coinMonster)
			{
				this.timeStart = timeStart;
				this.duration = duration;
				this.coolDown = coolDown;
				this.monsterType = monsterType;
				this.adMonster = adMonster;
				this.healthMonster = healthMonster;
				this.coinMonster = coinMonster;
			}
		}

		public string GetId()
		{
			return id.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadInt();
			chapter = reader.ReadInt();
			var lineDelimiter = ';';
			var arrTimeStart = reader.ReadString().Split(lineDelimiter);
			var arrDuration = reader.ReadString().Split(lineDelimiter);
			var arrCooldown = reader.ReadString().Split(lineDelimiter);
			var arrMonsterType = reader.ReadString().Split(lineDelimiter);
			var arrAdMonster = reader.ReadString().Split(lineDelimiter);
			var arrHealthMonster = reader.ReadString().Split(lineDelimiter);
			var arrCoinMonster = reader.ReadString().Split(lineDelimiter);

			waves = new WaveConfig[arrTimeStart.Length];

			for(int i = 0; i < arrTimeStart.Length; i++)
			{
				var timeStart = int.Parse(arrTimeStart[i]);
				var duration = int.Parse(arrDuration[i]);
				var cooldown = int.Parse(arrCooldown[i]);
				var monsterType = int.Parse(arrMonsterType[i]);
				var adMonster = int.Parse(arrAdMonster[i]);
				var healthMonster = int.Parse(arrHealthMonster[i]);
				var coinMonster = int.Parse(arrCoinMonster[i]);

				waves[i] = new WaveConfig(timeStart, duration, cooldown, (MonsterType) monsterType, adMonster, healthMonster, coinMonster);
			}
		}
	}

	public class DataChapterConfigTable : Configs<DataChapterConfig>
	{
		public override string FileName => nameof(DataChapterConfig);

		public DataChapterConfig GetConfig(int id)
		{
			return GetConfig(id.ToString());
		}
	}
}