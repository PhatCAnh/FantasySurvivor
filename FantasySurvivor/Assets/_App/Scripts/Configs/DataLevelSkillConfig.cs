using System.Collections.Generic;
using ArbanFramework.Config;
using UnityEngine;
namespace FantasySurvivor
{
	public class DataLevelSkillConfig : IConfigItem
	{
		public string skillName { get; private set; }
		public Dictionary<int, LevelSkillData> data { get; private set; }
		public string GetId()
		{
			return skillName;
		}

		public void OnReadImpl(IConfigReader reader)
		{
			data = new Dictionary<int, LevelSkillData>();
			
			skillName = reader.ReadString();

			var levelArr = reader.ReadIntArr();
			var valueArr = reader.ReadFloatArr();
			var cooldownArr = reader.ReadFloatArr();
			var descriptionArr = reader.ReadStringArr();

			for(int i = 0; i < levelArr.Length; i++)
			{
				data.Add(levelArr[i], new LevelSkillData(valueArr[i], cooldownArr[i], descriptionArr[i]));
			}
		}
	}

	public class DataLevelSkillConfigTable : Configs<DataLevelSkillConfig>
	{
		public override string FileName => nameof(DataLevelSkillConfig);

		public DataLevelSkillConfig GetConfig(SkillName skillName)
		{
			return GetConfig(skillName.ToString());
		}
	}
}