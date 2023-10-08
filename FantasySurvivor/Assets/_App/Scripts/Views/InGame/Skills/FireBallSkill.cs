using System.Linq;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class FireBallSkill : ProactiveSkill
	{
		public override void Init(SkillData data)
		{
			base.Init(data);
			timeDelaySkill = Mathf.RoundToInt(levelData.Last().Value.cooldown / 2 * 1000);
		}

		public override void UpLevel()
		{
			base.UpLevel();
			if(level == 3)
			{
				numberProjectile++;
			}
		}
	}
}