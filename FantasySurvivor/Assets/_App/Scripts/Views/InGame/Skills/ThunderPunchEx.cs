using ArbanFramework;
using FantasySurvivor;
using System.Linq;
using UnityEngine;

public class ThunderPunchEx : SkillActive
{
    public Cooldown cdTime;
    public Cooldown reActive;
    public float reAct;
    public void InitThunderPunchEx(float dame, int level, float size, float duration)
    {
        this.damage = dame;
        this.level = level;
        this.size = size;
        cdTime = new Cooldown(duration);
        reActive = new Cooldown(1);
        reActive.elapse = 1f;
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
            ThunderEx();
            reActive.Restart();
        }
    }
    private void ThunderEx()
    {
        foreach (var mons in gameController.listMonster.ToList())
        {
            if (gameController.CheckTouch(mons.transform.position, transform.position, size))
            {
                mons.TakeDamage(damage ,TextPopupType.Normal);
            }
        }
    }
}
