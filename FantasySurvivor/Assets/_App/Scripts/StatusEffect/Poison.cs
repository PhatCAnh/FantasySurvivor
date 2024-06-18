using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
	public Cooldown cd;
	public bool burnDie = false;
	public float duration;
	public float level;
	private GameController gameController => Singleton<GameController>.instance;

	public Poison(Monster target, float duration, float level) : base(target, duration)
	{
		type = EffectType.Debuff;
		cd = new Cooldown();
		this.duration = duration;
		this.level = level;
	}

	public override void Active()
	{
		base.Active();
		//value = 5;
		value = target.model.maxHealthPoint / 10;
		target.TakeDamage(value, TextPopupType.Poison);
		if(target.isDead)
		{
			burnDie = true;
		}
		if(burnDie && level == 6)
		{
			burnDie = false;
			var nearestMonster = gameController.FindNearestMonster(target.transform.position, 3f);
			if(nearestMonster != null)
			{
				PoisonTranf(nearestMonster);
			}
		}
	}
	public void PoisonTranf(Monster monster)
	{
		var poison = new Poison(monster, duration, level);
	}

	public override bool Cooldown(float deltaTime)
	{
		if(base.Cooldown(deltaTime)) return true;
		cd.Update(deltaTime);
		if(cd.isFinished)
		{
			Active();
			cd.Restart(0.5f);
		}
		return false;
	}
}