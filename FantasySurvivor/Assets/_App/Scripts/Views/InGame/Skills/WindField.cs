using _App.Scripts.Controllers;
using ArbanFramework;
using DG.Tweening;
using FantasySurvivor;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class WindField : SkillActive
{

    private List<Monster> _monstersTouched = new List<Monster>();
    protected HashSet<Monster> attackedMonsters = new HashSet<Monster>();
    public Cooldown cdTime;
    public Cooldown reActive;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        cdTime = new Cooldown(data.cooldown);
        this.transform.localScale = Vector3.one * data.valueSpecial1;
        this.size = data.valueSpecial1;
    }

    protected virtual void FixedUpdate()
    {
        if (gameController.isStop) return;
        this.transform.position = origin.transform.position;
        cdTime.Update(Time.deltaTime);
        if (cdTime.isFinished)
        {
            Destroy(gameObject);
            attackedMonsters.Clear();
            cdTime.Restart();
        }
        WindFieldSlow();
    }

    private void WindFieldSlow()
    {
        foreach (var mons in gameController.listMonster.ToList())
        {
            if (gameController.CheckTouch(mons.transform.position, transform.position, size) && !attackedMonsters.Contains(mons))
            {
                //mons.UpdateStat(StatModifierType.Mul, 1, data.valueSpecial2, 1, 1, data.cooldown);
                var slow = new Slow(mons, data.valueSpecial2, 0.2f);
                attackedMonsters.Add(mons);
            }
        }
    }
}
