using FantasySurvivor;
namespace _App.Datas.DataScript
{
	public class SkillDataTotal
	{
		public SkillId id;
		public SkillData skillDataUI;
		public DataLevelSkillConfig statSkillData;

		public SkillDataTotal(SkillId id, SkillData dataUI, DataLevelSkillConfig dataStat)
		{
			this.id = id;
			skillDataUI = dataUI;
			statSkillData = dataStat;
		}
	}
}