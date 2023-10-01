using ArbanFramework.MVC;
using UnityEngine;
namespace _App.Scripts.Views.InGame.Skills
{
	public class SkillAttack : View<GameApp>
	{
		private float _damage;

		private Monster _target;

		public void Init(float dmg, Monster target)
		{
			_damage = dmg;
			_target = target;
		}

		public void Attack()
		{
			_target.TakeDamage(Mathf.RoundToInt(_damage));
		}
	}
}