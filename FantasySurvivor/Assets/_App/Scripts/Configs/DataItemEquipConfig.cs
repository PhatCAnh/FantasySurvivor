using System;
using ArbanFramework.Config;
namespace FantasySurvivor
{
	[Serializable]
	public class DataItemEquipConfig : IConfigItem
	{
		public string id { get; set; }

		public int hp { get; set; }

		public int moveSpeed { get; set; }

		public int damage { get; set; }

		public float itemAttractionRange { get; set; }

		public float attackRange { get; set; }

		public int armor { get; set; }

		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadString();
			hp = reader.ReadInt();
			moveSpeed = reader.ReadInt();
			damage = reader.ReadInt();
			itemAttractionRange = reader.ReadFloat();
			attackRange = reader.ReadFloat();
			armor = reader.ReadInt();
		}
		public string GetId()
		{
			return id;
		}
	}
	
	public class DataItemEquipConfigTable : Configs<DataItemEquipConfig>
	{
		public override string FileName => nameof(DataItemEquipConfig);
		
		public DataItemEquipConfig GetConfig(ItemEquipId id)
		{
			return GetConfig(id.ToString());
		}
	}
}