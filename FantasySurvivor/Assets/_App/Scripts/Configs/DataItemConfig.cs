using System;
using ArbanFramework.Config;
namespace FantasySurvivor
{
	[Serializable]
	public class DataItemConfig : IConfigItem
	{
		public string id { get; private set; }

		public string name { get; private set; }
		
		public ItemType type { get; private set; }
		
		public int baseValue { get; private set; }
		
		public string description { get; private set; }

		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadString();
			name = reader.ReadString();
			type = (ItemType)Enum.Parse(typeof(ItemType), reader.ReadString());
			baseValue = reader.ReadInt();
			description = reader.ReadString();
		}
		public string GetId()
		{
			return id;
		}
	}
	
	public class DataItemConfigTable : Configs<DataItemConfig>
	{
		public override string FileName => nameof(DataItemConfig);
		
		public DataItemConfig GetConfig(ItemEquipId id)
		{
			return GetConfig(id.ToString());
		}
	}
}