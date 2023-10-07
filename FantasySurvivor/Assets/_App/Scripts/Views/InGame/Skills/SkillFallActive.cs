using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillFallActive : SkillActive
	{
		public override void Init(float damage, Monster target, GameObject effect)
		{
			base.Init(damage, target, effect);
			
			this.origin = gameController.character;
			
			this.target = target;

			transform.position = target.transform.position;

			callBackEffect = effect;

			this.damage = damage;

			sizeTouch = size + target.size;
		}
	}
}