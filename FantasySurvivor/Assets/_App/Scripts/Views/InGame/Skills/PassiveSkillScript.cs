using System.Transactions;
namespace FantasySurvivor
{
	public class PassiveSkill : Skill
	{
		public override void Init(SkillData data)
		{
			base.Init(data);
			UpdateBuff();
		}

		public virtual void UpdateBuff()
		{
			
		}
	}
	
	public class Passive1 : PassiveSkill
	{
		public override void UpdateBuff()
		{
			base.UpdateBuff();
			origin.model.itemAttractionRange += levelData[level].value;
		}
	}
}