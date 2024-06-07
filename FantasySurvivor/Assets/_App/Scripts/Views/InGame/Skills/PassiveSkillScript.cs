using System.Transactions;
using _App.Datas.DataScript;
namespace FantasySurvivor
{
	public class PassiveSkill : Skill
	{
		public override void Init(SkillDataTotal dataTotal)
		{
			base.Init(dataTotal);
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