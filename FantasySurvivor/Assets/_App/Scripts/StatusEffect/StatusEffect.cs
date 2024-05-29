using ArbanFramework;
using FantasySurvivor;
public enum EffectType
{
	Buff,
	Debuff,
}

public class StatusEffect
{
	public EffectType type;
	public float value;
	public Monster target;
	public Cooldown cdTotal;
	

	public StatusEffect(Monster target ,float value, float duration)
	{
		this.target = target;
		this.target.listStatusEffect.Add(this);
		this.value = value;
		cdTotal = new Cooldown();
		cdTotal.Restart(duration);
	}
	
	public virtual bool Cooldown(float deltaTime)
	{
		cdTotal.Update(deltaTime);
		if(cdTotal.isFinished)
		{
			target.listStatusEffect.Remove(this);
			return true;
		}
		return false;
	}

	public virtual void Active()
	{
		
	}
}

