using System.Collections.Generic;
using ArbanFramework.Config;
namespace DataConfig
{
	public class WaveConfig
	{
		public string idMonster { get; private set; }
		public int timeStart { get; private set; }
		public int stepTime { get; private set; }
		public int number { get; private set; }
		public int adMonster { get; private set; }
		public int healthMonster { get; private set; }
		public int expMonster { get; private set; }

		public WaveConfig(string idMonster, int timeStart, int stepTime, int number, int adMonster, int healthMonster, int expMonster)
		{
			this.idMonster = idMonster;
			this.timeStart = timeStart;
			this.stepTime = stepTime;
			this.number = number;
			this.adMonster = adMonster;
			this.healthMonster = healthMonster;
			this.expMonster = expMonster;
		}
	}
	
	public class DataChapterConfig : IConfigItem
	{
		public int level { get; private set; }
		public WaveConfig[] waves { get; private set; }
		
		public int coin { get; private set; }

		public string GetId()
		{
			return level.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			// var arrTimeStart = reader.ReadString().Split(lineDelimiter);
			var lineDelimiter = '_';

			level = reader.ReadInt();
			var idMonsterArr = reader.ReadString();
			var idMonster = idMonsterArr.Split(lineDelimiter);
			var startTime = reader.ReadString().Split(lineDelimiter);
			var stepTime = reader.ReadString().Split(lineDelimiter);
			var number = reader.ReadString().Split(lineDelimiter);
			var atkDamage = reader.ReadString().Split(lineDelimiter);
			var healthPoint = reader.ReadString().Split(lineDelimiter);
			var exp = reader.ReadString().Split(lineDelimiter);
			coin = reader.ReadInt();

			waves = new WaveConfig[idMonster.Length];

			for(int i = 0; i < waves.Length; i++)
			{
				WaveConfig wave = new WaveConfig(
					idMonster[i],
					int.Parse(startTime[i]),
					int.Parse(stepTime[i]),
					int.Parse(number[i]),
					int.Parse(atkDamage[i]),
					int.Parse(healthPoint[i]),
					int.Parse(exp[i])
				);

				waves[i] = wave;
			}
		}
	}

	public class DataChapterConfigTable : Configs<DataChapterConfig>
	{
		public override string FileName => nameof(DataChapterConfig);

		public DataChapterConfig GetConfig(int level)
		{
			return GetConfig(level.ToString());
		}
	}
}