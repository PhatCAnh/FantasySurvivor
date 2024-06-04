using ArbanFramework;
using FantasySurvivor;
using System.Diagnostics;

using UnityEngine;

public class Cyclone : SkillBulletActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        callBackKilled += SpeedUP;
    }

    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
        TakeDamage(monster);
        return true;
    }

    private void SpeedUP()
    {
        if (origin.model.moveSpeed == origin.stat.moveSpeed.BaseValue)
        {
            origin.UpdateStat(StatModifierType.Mul,1,data.valueSpecial1,1,1,1,1,data.valueSpecial2);
            
        }
    }
}
