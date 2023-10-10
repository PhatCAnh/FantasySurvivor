using System.Linq;
using UnityEngine;
namespace FantasySurvivor
{
	public class FireBall : ProactiveSkill
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
	
	public class Twin : ProactiveSkill
	{
		public override void Init(SkillData data)
		{
			base.Init(data);
			timeDelaySkill = Mathf.RoundToInt(1000);
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
	
	public class Shark : ProactiveSkill
	{

		protected override void UpdatePrefab(SkillActive prefab)
		{
			base.UpdatePrefab(prefab);
			if(level == 6)
			{
				prefab.transform.localScale = Vector3.one * 2;
				prefab.size = 2;
			}
		}
		
	}
}