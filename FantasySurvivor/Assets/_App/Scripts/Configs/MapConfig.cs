using ArbanFramework.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
    public class MapConfig : IConfigItem
    {
        public int level { get; private set; }
        public int[,] mapConfig { get; private set; }

        public string GetId()
        {
            return level.ToString();
        }

        public void OnReadImpl(IConfigReader reader)
        {
            level = reader.ReadInt();
            var mapStr = reader.ReadString();
            var lineDelimiter = ';';
            var charDelimiter = '-';

            var lines = mapStr.Split(lineDelimiter);
            var column = lines[0].Split(charDelimiter);
            var rowAmount = lines.Length;
            var columnAmount = column.Length;
            mapConfig = new int[rowAmount, columnAmount];

            for (int i = 0; i < rowAmount; i++)
            {
                var str = lines[i].Split(charDelimiter);
                for (int j = 0; j < columnAmount; j++)
                {
                    var value = int.Parse(str[j]);
                    mapConfig[i, j] = value;
                }
            }
        }
    }

    public class MapConfigTable : Configs<MapConfig>
    {
        public override string FileName => nameof(MapConfig);

        public MapConfig GetConfig(int level)
        {
            return GetConfig(level.ToString());
        }
    }
}
