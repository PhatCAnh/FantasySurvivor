using ArbanFramework.Config;
namespace _App.Scripts.Configs
{
	public class DataStatMonsterConfig : IConfigItem
	{
		public string id { get; private set; }
		public string monsterType { get; private set; }
		public float moveSpeed { get; private set; }
		public float attackSpeed { get; private set; }
		public float attackRange { get; private set; }
		
		public string GetId()
		{
			return id.ToString();
		}
		
		public void OnReadImpl(IConfigReader reader)
		{
			id = reader.ReadString();
			monsterType = reader.ReadString();
			moveSpeed = reader.ReadFloat();
			attackSpeed = reader.ReadFloat();
			attackRange = reader.ReadFloat();
		}
	}

	public class DataStatMonsterConfigTable : Configs<DataStatMonsterConfig>
	{
		public override string FileName => nameof(DataStatMonsterConfig);
	}
}