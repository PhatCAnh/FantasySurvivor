using System.Collections.Generic;
using ArbanFramework.Config;
namespace DataConfig
{
	public class DataChapterConfig : IConfigItem
	{
		public int chapter { get; private set; }
		public List<WaveConfig> waves { get; private set; }
		public class WaveConfig
		{
			public int timeStart { get; private set; }
			public int duration { get; private set; }
			public int coolDown { get; private set; }
		
			public MonsterType monsterType { get; private set; }
		
			public int adMonster { get; private set; }
			public int healthMonster { get; private set; }
			public int coinMonster { get; private set; }
			
			public int expMonster { get; private set; }
		
			public WaveConfig(int timeStart, int duration, int coolDown, MonsterType monsterType, int adMonster, int healthMonster, int coinMonster, int expMonster)
			{
				this.timeStart = timeStart;
				this.duration = duration;
				this.coolDown = coolDown;
				this.monsterType = monsterType;
				this.adMonster = adMonster;
				this.healthMonster = healthMonster;
				this.coinMonster = coinMonster;
				this.expMonster = expMonster;
			}
		}

		public string GetId()
		{
			return chapter.ToString();
		}

		public void OnReadImpl(IConfigReader reader)
		{
			// var arrTimeStart = reader.ReadString().Split(lineDelimiter);
			
			var arrChapter = reader.ReadIntArr();
			var arrTimeStart = reader.ReadIntArr();
			var arrDuration = reader.ReadIntArr();
			var arrCooldown = reader.ReadIntArr();
			var arrMonsterType = reader.ReadIntArr();
			var arrAdMonster = reader.ReadIntArr();
			var arrHealthMonster = reader.ReadIntArr();
			var arrCoinMonster = reader.ReadIntArr();
			var arrExpMonster = reader.ReadIntArr();

			chapter = arrChapter[0];
			waves = new List<WaveConfig>();

			for(int i = 0; i < arrChapter.Length; i++)
			{
				WaveConfig wave = new WaveConfig(
					arrTimeStart[i],
					arrDuration[i],
					arrCooldown[i],
					(MonsterType) arrMonsterType[i],
					arrAdMonster[i],
					arrHealthMonster[i],
					arrCoinMonster[i],
					arrExpMonster[i]
				);
				
				waves.Add(wave);
			}
			
			
			
			
			
			
			
			

			// waves = new WaveConfig[arrTimeStart.Length];
			//
			// for(int i = 0; i < arrTimeStart.Length; i++)
			// {
			// 	var timeStart = int.Parse(arrTimeStart[i]);
			// 	var duration = int.Parse(arrDuration[i]);
			// 	var cooldown = int.Parse(arrCooldown[i]);
			// 	var monsterType = int.Parse(arrMonsterType[i]);
			// 	var adMonster = int.Parse(arrAdMonster[i]);
			// 	var healthMonster = int.Parse(arrHealthMonster[i]);
			// 	var coinMonster = int.Parse(arrCoinMonster[i]);
			//
			// 	waves[i] = new WaveConfig(timeStart, duration, cooldown, (MonsterType) monsterType, adMonster, healthMonster, coinMonster);
			// }
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