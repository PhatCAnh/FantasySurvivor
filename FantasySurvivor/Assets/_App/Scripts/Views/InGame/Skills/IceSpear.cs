using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System;
using System.Linq;
using System.Threading;
public class IceSpear : SkillBulletActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
    }
    private static Random random = new Random();

    public static bool IsActionSuccessful(double probability)
    {
        return random.NextDouble() < probability;
    }

    protected override void HandleTouch()
    {
        if (!canBlock)
        {
            switch (skillDamagedType)
            {
                case SkillDamagedType.AreaOfEffect:
                    foreach (var mons in gameController.listMonster.ToList())
                    {
                        if (!attackedMonsters.Contains(mons) && gameController.CheckTouch(mons.transform.position, transform.position, sizeTouch))
                        {
                            TouchUnit(mons);
                            attackedMonsters.Add(mons);
                        }
                    }
                    break;
            }
        }
    }

    protected override void TouchUnit(Monster mons)
    {
        mons.TakeDamage(damage, TextPopupType.Normal,isCritical);
        if (IsActionSuccessful(data.valueSpecial2))
        {
            mons.UpdateStat(StatModifierType.Mul, 1, 0, 1, 1, data.valueSpecial1);
        }
    }
}


