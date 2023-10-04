using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Views.InGame.Skills;
using ArbanFramework.MVC;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Cooldown = ArbanFramework.Cooldown;

public class ProactiveSkill : View<GameApp>
{
	protected int timeSKill = 2;
	public Cooldown cooldownSkill = new Cooldown();

	protected GameObject skillPrefab;

	protected Character origin;
	protected GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	private void Awake()
	{
		origin = GetComponent<Character>();
	}

	public void CoolDownSkill(float deltaTime)
	{
		cooldownSkill.Update(deltaTime);
		if(cooldownSkill.isFinished)
		{
			Active();
			cooldownSkill.Restart(timeSKill);
		}
	}

	public virtual void Active()
	{

	}
}


public class SharkSkill : ProactiveSkill
{
	public override void Active()
	{
		var mons = gameController.GetStrongMonster(origin.model.attackRange);
		if(mons != null)
		{
			var shark = Instantiate(
				app.resourceManager.GetSkill(SkillType.SharkSkill).skillPrefab,
				new Vector3(mons.transform.position.x, mons.transform.position.y),
				quaternion.identity
			);
			shark.GetComponent<SkillAttack>().Init(origin.model.attackDamage, mons);
		}
	}
}

public class FireBallSkill : ProactiveSkill
{
	public override void Active()
	{
		var mons = gameController.GetFirstMonster(origin.model.attackRange);
		if(mons != null)
		{
			var shark = Instantiate(
				app.resourceManager.GetSkill(SkillType.FireBallSkill).skillPrefab,
				origin.transform.position,
				quaternion.identity
			);
			shark.GetComponent<BulletView>().Init(origin, mons);
		}
	}
}

public class TwinSkill : ProactiveSkill
{
	public override void Active()
	{
		var mons = gameController.GetFirstMonster(origin.model.attackRange);
		if(mons != null)
		{
			var skill = Instantiate(
				app.resourceManager.GetSkill(SkillType.TwinSkill).skillPrefab,
				origin.transform.position,
				quaternion.identity
			);
			skill.GetComponent<Crossed>().Init(origin, mons);
		}
	}
}	