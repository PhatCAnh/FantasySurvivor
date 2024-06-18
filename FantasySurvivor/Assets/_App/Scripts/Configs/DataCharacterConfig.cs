using ArbanFramework.Config;
namespace FantasySurvivor
{
	public class DataCharacterConfig : IConfigItem
	{
		public string id { get; private set; }
		public int hp { get; private set; }
		public int moveSpeed { get; private set; }
		public int damage { get; private set; }
		public float itemAttractionRange { get; private set; }
		public float attackRange { get; private set; }
		public int armor { get; private set; }
		public float regen { get; private set; }
        public int shield { get; private set; } // Thêm thuộc tính shield

        public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadString();
			hp = reader.ReadInt();
			moveSpeed = reader.ReadInt();
			damage = reader.ReadInt();
			itemAttractionRange = reader.ReadFloat();
			attackRange = reader.ReadFloat();
			armor = reader.ReadInt();
			regen = reader.ReadFloat();
            shield = reader.ReadInt();
        }
        public string GetId()
		{
			return id;
		}
	}

	public class DataCharacterConfigTable : Configs<DataCharacterConfig>
	{
		public override string FileName => nameof(DataCharacterConfig);

		public DataCharacterConfig GetConfig(CharacterId id)
		{
			return GetConfig(id.ToString());
		}
	}
}