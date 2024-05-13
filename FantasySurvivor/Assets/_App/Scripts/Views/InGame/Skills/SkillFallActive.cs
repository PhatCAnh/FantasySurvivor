using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillFallActive : SkillActive
	{
		public override void Init(float damage, Monster target, int level)
		{
			base.Init(damage, target, level);

			if(target == null) return;

			transform.position = target.transform.position;
		}
	}
	// public class Thunder :SkillFallActive {
 //        protected override void CheckAoeMons()
 //        {
 //            foreach(var mons in gameController.listMonster.ToList())
	// 		{
	// 			if(mons == target)
	// 			{
 //                    mons.TakeDamage(damage, isCritical);
 //                }
	// 			else
	// 			{
	// 				mons.TakeDamage(damage * 0.75f , isCritical);
 //                }
	// 		}
 //        }
 //    }
}