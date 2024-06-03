using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using UnityEngine;

public class FireBurnArea : SkillActive
{
    public Cooldown cdTime;
    public Cooldown reActive;
    public float reAct;
    public void InitFireArea(float dame, int level, float size, float duration, float reAct)
    {
        this.damage = dame;
        this.level = level;
        this.size = size;
        cdTime = new Cooldown(duration);
        reActive = new Cooldown(reAct);
        reActive.elapse = reAct;
        this.reAct = reAct;
        transform.localScale = Vector3.one * size;
    }

    private void Update()
    {
        if (gameController.isStop) return;
        cdTime.Update(Time.deltaTime);
        reActive.Update(Time.deltaTime);
        if (cdTime.isFinished)
        {
            Destroy(gameObject);
            cdTime.Restart();
        }
        if (reActive.isFinished)
        {
            onBurnArea(reAct);
        }
    }
    private void onBurnArea(float duration)
    {
        foreach (var mons in gameController.listMonster.ToList())
        {
            if (gameController.CheckTouch(mons.transform.position, transform.position, size))
            {
                var ignite = new Ignite(mons, damage, 0.1f);
            }
        }
        reActive.Restart();
    }

}
