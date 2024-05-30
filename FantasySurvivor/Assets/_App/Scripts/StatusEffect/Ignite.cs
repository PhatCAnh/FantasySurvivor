using ArbanFramework;
using UnityEngine;
public class Ignite : StatusEffect
{
	public Cooldown cd;

	public Ignite(Monster target, float value, float duration) : base(target, value, duration)
	{
		type = EffectType.Debuff;
		this.value = value / 2;
		cd = new Cooldown();
	}

	public override void Active()
	{
		base.Active();
		target.TakeDamage(value, TextPopupType.Fire);
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