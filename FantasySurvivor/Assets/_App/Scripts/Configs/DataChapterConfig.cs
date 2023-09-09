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
			public MonsterType typeMonster { get; private set; }
			public int levelMonster { get; private set; }

			public WaveConfig(int timeStart, int duration, int coolDown, MonsterType monsterType, int levelMonster)
			{
				this.timeStart = timeStart;
				this.duration = duration;
				this.coolDown = coolDown;
				this.typeMonster = monsterType;
				this.levelMonster = levelMonster;
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
			var arrTypeMonster = reader.ReadString().Split(lineDelimiter);
			var arrLevelMonster = reader.ReadString().Split(lineDelimiter);
			waves = new WaveConfig[arrTimeStart.Length];

			for (int i = 0; i < arrTimeStart.Length; i++)
			{
				var timeStart = int.Parse(arrTimeStart[i]);
				var duration =  int.Parse(arrDuration[i]);
				var cooldown =  int.Parse(arrCooldown[i]);
				var typeMonster = int.Parse(arrTypeMonster[i]);
				var levelMonster =  int.Parse(arrLevelMonster[i]);
				
				waves[i] = new WaveConfig(timeStart, duration, cooldown, (MonsterType) typeMonster, levelMonster);
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