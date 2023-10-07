using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
namespace FantasySurvivor
{
	public class SkillAttack : View<GameApp>
	{
		private float _damage;

		private Monster _target;
		
		protected GameObject callBackEffect;

		public void Init(float dmg, Monster target, SkillName name)
		{
			_damage = dmg;
			_target = target;
			//callBackEffect = app.resourceManager.GetSkillEffect(name);
		}

		public void Attack()
		{
			if(_target == null) return;
			_target.TakeDamage(Mathf.RoundToInt(_damage));
			if(callBackEffect != null)
			{
				Instantiate(callBackEffect, _target.transform.position, quaternion.identity);
			}
		}
	}
}