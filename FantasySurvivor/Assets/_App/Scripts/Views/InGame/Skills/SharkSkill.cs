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
	protected Cooldown cooldownSkill = new Cooldown();

	protected GameObject skillPrefab;

	protected TowerView towerView;
	protected GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		towerView = GetComponent<TowerView>();
	}

	// Update is called once per frame
	void Update()
	{
		if(gameController.isStop) return;
		cooldownSkill.Update(Time.deltaTime);
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
		var mons = gameController.GetStrongMonster(towerView.model.attackRange);
		if(mons != null)
		{
			var shark = Instantiate(
				app.resourceManager.GetSkill(SkillType.SharkSkill).skillPrefab,
				new Vector3(mons.transform.position.x, mons.transform.position.y),
				quaternion.identity
			);
			shark.GetComponent<SkillAttack>().Init(towerView.model.attackDamage, mons);
		}
	}
}