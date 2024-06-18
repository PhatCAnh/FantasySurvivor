using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightningWebWeb : SkillActive
{
    public Cooldown cdTime;
    public Cooldown reActive;
    public float reAct;
    public float slow;
    public void InitThunderWeb(float dame, int level, float size, float slow, float reAct)
    {
        this.damage = dame;
        this.level = level;
        this.size = size;
        cdTime = new Cooldown(5f);
        reActive = new Cooldown(reAct);
        reActive.elapse = reAct;
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
            ThunderWeb(reAct);
            reActive.Restart();
        }
    }
    private void ThunderWeb(float duration)
    {
        foreach (var mons in gameController.listMonster.ToList())
        {
            if (gameController.CheckTouch(mons.transform.position, transform.position, size))
            {
                mons.UpdateStat(StatModifierType.Mul, 1, slow, 1, 1, duration+0.2f);
            }
        }
        reActive.Restart();
    }
}
