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

    public Poison(Monster target,  float duration, float level) : base(target, duration)
    {
        type = EffectType.Debuff;
        cd = new Cooldown();
        this.duration = duration;
        this.level = level;
    }

    public override void Active()
    {
        base.Active();
        value = target.model.maxHealthPoint / 20;
        target.TakeDamage(value, TextPopupType.Poison);
        if (burnDie && level == 6)
        {
            burnDie = false;
            var nearestMonster = gameController.FindNearestMonster( 3f, target);
            if (nearestMonster != null)
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
            if (target.isDead)
            {
                burnDie = true;
            }

        }
        return false;
    }
}
