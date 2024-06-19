using System;
using System.Collections.Generic;
using ArbanFramework.Config;
using UnityEngine;
namespace FantasySurvivor
{
    public class DataLevelSkillConfig : IConfigItem
    {
        public string Id { get; private set; }
        public string SkillName { get; private set; }
        public Dictionary<int, LevelSkillData> data { get; private set; }
        public string GetId()
        {
            return Id;
        }

        public void OnReadImpl(IConfigReader reader)
        {
            data = new Dictionary<int, LevelSkillData>();

            Id = reader.ReadString();

            SkillName = reader.ReadString();

            var levelArr = reader.ReadIntArr();
            var valueArr = reader.ReadFloatArr();
            var cooldownArr = reader.ReadFloatArr();
            var descriptionArr = reader.ReadStringArr();
            var vs1Arr = reader.ReadFloatArr();
            var vs2Arr = reader.ReadFloatArr();
            var vs3Arr = reader.ReadFloatArr();

            for (int i = 0; i < levelArr.Length; i++)
            {
                data.Add(levelArr[i], new LevelSkillData(valueArr[i], cooldownArr[i], descriptionArr[i], vs1Arr[i], vs2Arr[i], vs3Arr[i]));
            }
        }
    }

    public class DataLevelSkillConfigTable : Configs<DataLevelSkillConfig>
    {
        public override string FileName => nameof(DataLevelSkillConfig);

        public DataLevelSkillConfig GetConfig(SkillId skillName)
        {
            return GetConfig(skillName.ToString());
        }
    }
}