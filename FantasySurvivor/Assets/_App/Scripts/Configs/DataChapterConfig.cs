
using System.Collections.Generic;
using ArbanFramework.Config;
namespace FantasySurvivor
{
	public class WaveConfig
	{
		public string idMonster { get; private set; }
		public int stepTime { get; private set; }
		public int number { get; private set; }
		public int adMonster { get; private set; }
		public int healthMonster { get; private set; }
		public int expMonster { get; private set; }

		public WaveConfig(string idMonster, int stepTime, int number, int adMonster, int healthMonster, int expMonster)
		{
			this.idMonster = idMonster;
			this.stepTime = stepTime;
			this.number = number;
			this.adMonster = adMonster;
			this.healthMonster = healthMonster;
			this.expMonster = expMonster;
		}
	}

	public class ControlWaveeConfig
	{
		public int timeEnd { get; private set; }
		public WaveConfig[] waves { get; private set; }
		public int coin { get; private set; }

		public ControlWaveeConfig(int timeEnd, WaveConfig[] waves, int coin)
		{
			this.timeEnd = timeEnd;

			this.waves = waves;

			this.coin = coin;
		}
	}

	public class DataChapterConfig : IConfigItem
	{
		public int chapter { get; private set; }

		public Dictionary<int, ControlWaveeConfig[]> dataLevel;
		public string GetId()
		{
			return chapter.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			// var arrTimeStart = reader.ReadString().Split(lineDelimiter);
			var lineDelimiter = '_';

			dataLevel = new Dictionary<int, ControlWaveeConfig[]>();

			chapter = reader.ReadInt();

			var levelArr = reader.ReadIntArr();

			var waveArr = reader.ReadIntArr();

			var idMonsterArr = reader.ReadStringArr();

			var endTimeArr = reader.ReadIntArr();

			var stepTimeArr = reader.ReadStringArr();

			var numberArr = reader.ReadStringArr();

			var atkDamageArr = reader.ReadStringArr();

			var healthPointArr = reader.ReadStringArr();

			var expArr = reader.ReadStringArr();

			var coinArr = reader.ReadIntArr();

			var checkLevel = 0;
			List<int> listLevel = new List<int>();
			for(int i = 0; i < levelArr.Length; i++)
			{
				if(levelArr[i] != checkLevel)
				{
					checkLevel = levelArr[i];
					listLevel.Add(checkLevel);
				}
			}

			foreach(var currentLevel in listLevel)
			{
				List<ControlWaveeConfig> levelData = new List<ControlWaveeConfig>();

				for(int i = 0; i < levelArr.Length; i++)
				{
					if(currentLevel == levelArr[i])
					{
						var idMonster = idMonsterArr[i].Split(lineDelimiter);
						var stepTime = stepTimeArr[i].Split(lineDelimiter);
						var number = numberArr[i].Split(lineDelimiter);
						var atkDamage = atkDamageArr[i].Split(lineDelimiter);
						var healthPoint = healthPointArr[i].Split(lineDelimiter);
						var exp = expArr[i].Split(lineDelimiter);

						WaveConfig[] waves = new WaveConfig[idMonster.Length];
						for(int j = 0; j < waves.Length; j++)
						{
							WaveConfig wave = new WaveConfig(
								idMonster[j],
								int.Parse(stepTime[j]),
								int.Parse(number[j]),
								int.Parse(atkDamage[j]),
								int.Parse(healthPoint[j]),
								int.Parse(exp[j])
							);
							waves[j] = wave;
						}

						ControlWaveeConfig level = new ControlWaveeConfig(
							endTimeArr[i],
							waves,
							coinArr[i]
						);
						levelData.Add(level);
					}
				}
				dataLevel.Add(currentLevel, levelData.ToArray());
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

		public ControlWaveeConfig[] GetConfigLevel(int chapter, int level)
		{
			return GetConfig(chapter).dataLevel[level];
		}
	}
}