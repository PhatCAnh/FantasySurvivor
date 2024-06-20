using ArbanFramework;
using UnityEngine;

public class Slow : StatusEffect
{
    private bool isApplied;

    public Slow(Monster target, float value, float duration) : base(target, value, duration)
    {
        type = EffectType.Debuff;
        this.value = value / 2;
        isApplied = false;
    }
    public Slow(Character target, float value, float duration) : base(target, value, duration)
    {
        type = EffectType.Debuff;
        this.value = value / 2;
        isApplied = false;
    }


    public override void Active()
    {
        base.Active();
        if (!isApplied)
        {
            target.model.moveSpeed *= (value/100);
            isApplied = true;
        }
        Debug.Log(target.model.moveSpeed);
    }

    public override bool Cooldown(float deltaTime)
    {
        Active();
        cdTotal.Update(deltaTime);
        if (cdTotal.isFinished)
        {
            target.model.moveSpeed = target.stat.moveSpeed.BaseValue;
            target.listStatusEffect.Remove(this);
            return true;
        }
        return false;
    }
}
