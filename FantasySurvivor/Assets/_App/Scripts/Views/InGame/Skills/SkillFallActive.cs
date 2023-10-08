using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillFallActive : SkillActive
	{
		public override void Init(float damage, Monster target, GameObject effect, int level)
		{
			base.Init(damage, target, effect, level);

			if(target == null) return;

			transform.position = target.transform.position;
		}
	}
}