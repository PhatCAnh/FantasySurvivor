using ArbanFramework.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
    public class BlockConfig : IConfigItem
    {
        public int id { get; private set; }
        public int blockId { get; private set; }
        public ItemConfig[] items { get; private set; }
        public class ItemConfig
        {
            public ItemType type { get; private set; }
            public Vector2 position { get; private set; }

            public ItemConfig(ItemType type, Vector2 position)
            {
                this.type = type;
                this.position = position;
            }
        }

        public string GetId()
        {
            return id.ToString();
        }

        public void OnReadImpl(IConfigReader reader)
        {
            id = reader.ReadInt();
            blockId = reader.ReadInt();
            var mapStr = reader.ReadString();
            var lineDelimiter = ';';
            var charDelimiter = '-';

            var lines = mapStr.Split(lineDelimiter);
            items = new ItemConfig[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                var strs = lines[i].Split(charDelimiter);
                var itemId = int.Parse(strs[0]);
                var position = new Vector2(int.Parse(strs[1]), int.Parse(strs[2]));
                items[i] = new ItemConfig((ItemType)itemId, position);
            }
        }
    }

    public class BlockConfigTable : Configs<BlockConfig>
    {
        public override string FileName => nameof(BlockConfig);

        public BlockConfig GetConfig(int id)
        {
            return GetConfig(id.ToString());
        }
    }
}
