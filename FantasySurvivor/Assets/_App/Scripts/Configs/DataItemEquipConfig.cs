using ArbanFramework.Config;
namespace FantasySurvivor
{
	public class DataItemEquipConfig : IConfigItem
	{
		public string id { get; private set; }

		public int hp { get; private set; }

		public int moveSpeed { get; private set; }

		public int damage { get; private set; }

		public float itemAttractionRange { get; private set; }

		public float attackRange { get; private set; }

		public int armor { get; private set; }

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