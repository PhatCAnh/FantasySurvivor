
using System.Collections.Generic;
using System.Linq;
using FantasySurvivor;
using UnityEngine;
using System.Numerics;
using _App.Scripts.Controllers;
using ArbanFramework;
using Vector3 = UnityEngine.Vector3;
namespace _App.Scripts.Views.InGame.Skills.SkillMono
{
	public class FireShieldControl : ProactiveSkill
	{
		private List<Monster> _monstersTouched = new List<Monster>();
		public float speed = 1f;
		private List<SkillActive> fireBalls = new List<SkillActive>();
		private Cooldown cdExist = new Cooldown();
		private float radius = 3;
		public override void UpLevel()
		{
			base.UpLevel();
			RemoveFireShield();
			cdExist.End();
			cooldownSkill.End();
			Active();
			cooldownSkill.Restart(levelData[level].cooldown);
		}

		public override void Active()
		{
			_monstersTouched.Clear();
			float angleStep = 360f / level; // Góc giữa các quả cầu lửa
			var angle = 360f;
			for(int i = 0; i < level; i++)
			{
				var skill = Singleton<PoolController>.instance.GetObject(skillPrefab, Vector3.zero).GetComponent<SkillActive>();
				skill.Init(levelData[level], null, level, skillPrefab);
				fireBalls.Add(skill);
				var script = skill.GetComponent<FireShield>();
				script.UpdateAngle(angle - angleStep * i);
			}
			cdExist.Restart(levelData[level].valueSpecial1);
		}
		public override void CoolDownSkill(float deltaTime)
		{
			if(cdExist.isFinished)
			{
				RemoveFireShield();
			
				cooldownSkill.Update(deltaTime);
				if(cooldownSkill.isFinished)
				{
					Active();
					cooldownSkill.Restart(levelData[level].cooldown);
				}
			}
			else
			{
				cdExist.Update(deltaTime);
			}
		}

		private void RemoveFireShield()
		{
			foreach(var item in fireBalls.ToList())
			{
				//fix chỗ này
				GameObject.Destroy(item.gameObject);
			}
			fireBalls.Clear();
		}
	}
}