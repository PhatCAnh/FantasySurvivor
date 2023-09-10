using ArbanFramework.Config;
namespace _App.Scripts.Configs
{
	public class NormalMonsterStatConfig : IConfigItem
	{
		public int level { get; private set; }
		public float moveSpeed { get; private set; }
		public int health { get; private set; }
		public int attackDamage { get; private set; }
		public float attackSpeed { get; private set; }

		
		public string GetId()
		{
			return level.ToString();
		}
		
		public void OnReadImpl(IConfigReader reader)
		{
			level = reader.ReadInt();
			moveSpeed = reader.ReadFloat();
			health = reader.ReadInt();
			attackDamage = reader.ReadInt();
			attackSpeed = reader.ReadFloat();
		}
	}

	public class NormalMonsterStatConfigTable : Configs<NormalMonsterStatConfig>
	{

		public override string FileName => nameof(NormalMonsterStatConfig);

		public NormalMonsterStatConfig GetConfig(int level)
		{
			return GetConfig(level.ToString());
		}
	}
}