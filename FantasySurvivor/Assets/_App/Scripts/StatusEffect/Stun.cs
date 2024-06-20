using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : StatusEffect
{
    public Stun(Monster target, float duration) : base(target, duration)
    {
        type = EffectType.Debuff;
        this.value = value / 2;
    }
    public Stun(Character target, float duration) : base(target, duration)
    {
        type = EffectType.Debuff;
        this.value = value / 2;
    }


    public override void Active()
    {
        base.Active();
        target.isStun = true;
    }

    public override bool Cooldown(float deltaTime)
    {
        Active();
        cdTotal.Update(deltaTime);
        if (cdTotal.isFinished)
        {
            target.isStun = false;
            target.listStatusEffect.Remove(this);
            return true;
        }
        return false;
    }
}
