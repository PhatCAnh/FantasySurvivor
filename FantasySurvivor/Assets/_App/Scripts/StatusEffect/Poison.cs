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

    public Poison(Monster target, float value, float duration , float level) : base(target, value, duration)
    {
        type = EffectType.Debuff;
        this.value = value / 2;
        cd = new Cooldown();
        this.duration = duration;
        this.level = level;
    }

    public void PoisonTranf(Monster monster)
    {
        var poison = new Poison(monster, value, duration, level);
    }


    public override void Active()
    {
        base.Active();

        target.TakeDamage(value, TextPopupType.Fire);

        if (burnDie && level == 6 )
        {
            burnDie = false;
            var nearestMonster = gameController.FindNearestMonster(target.transform.position, 3f);
            if (nearestMonster != null)
            {
                PoisonTranf(nearestMonster);
            }
        }
    }

    public override bool Cooldown(float deltaTime)
    {
        if (base.Cooldown(deltaTime)) return true;

        cd.Update(deltaTime);

        if (cd.isFinished)
        {
            Active();

            if (target.isDead)
            {
                burnDie = true;
            }

            cd.Restart(0.5f);
        }

        return false;
    }
}
