using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Channeling : StatusEffect
{
    public Cooldown cd;
    public float ChannelingTime;
    public float duration;
    public float level;
    private GameController gameController => Singleton<GameController>.instance;

    public Channeling(Monster target,float dame , float duration, float level,float channelingTime) : base(target, duration)
    {
        this.value = dame;
        type = EffectType.Debuff;
        cd = new Cooldown();
        this.duration = duration;
        this.level = level;
        this.ChannelingTime = channelingTime;
    }

    public override void Active()
    {
        base.Active();
        target.TakeDamage(value, TextPopupType.Lightning);
        if (ChannelingTime > 0)
        {
            Monster nearestMonster = gameController.FindNearestMonster( 3f, target);
            if (nearestMonster != null)
            {
                var channeling = new Channeling(nearestMonster,value , duration, level, ChannelingTime - 1);
            }else if (level == 6)
            {
                var channeling = new Channeling(target,value, duration, level, ChannelingTime - 1);
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
            cd.Restart(0.1f);
        }
        return false;
    }
}
