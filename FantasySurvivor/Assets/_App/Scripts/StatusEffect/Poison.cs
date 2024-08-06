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
	public float damage;
	public float level;
	private GameController gameController => Singleton<GameController>.instance;

	public Poison(Monster target,float damage ,float duration, float level) : base(target, duration)
	{
		type = EffectType.Debuff;
		cd = new Cooldown();
		this.damage = damage;
		this.duration = duration;
		this.level = level;
	}

	public override void Active()
	{
		base.Active();
        AudioManager.Instance.StopLoopingSFX();
        AudioManager.Instance.PlaySFX("PoisonBulletsboom");
        value = damage / 2;
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
		var poison = new Poison(monster,damage ,duration, level);
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