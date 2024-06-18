using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : StatusEffect
{
    public Cooldown cd;

    public Poison(Monster target, float duration) : base(target, duration)
    {
        type = EffectType.Debuff;
        cd = new Cooldown();
    }

    public override void Active()
    {
        base.Active();
        value = target.model.currentHealthPoint / 100;
        target.TakeDamage(value, TextPopupType.Poison);
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
