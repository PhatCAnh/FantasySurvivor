using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthShield : SkillBulletActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
    }

    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
        TakeDamage(monster);
        if (monster.isDead )
        {
            ShieldUp();
        }
        return true;
    }

    private void ShieldUp()
    {
        app.models.characterModel.shield += (origin.model.maxHealthPoint/10f);
        Debug.Log(origin.model.shield);
    }
}
