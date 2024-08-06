using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoisonBullet : SkillBulletActive
{

    private bool check = false;
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFXLoop("PoisonBulletsloop");
    }

    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
        TakeDamage(monster);
        if (!monster.isDead)
        {
            var poison = new Poison(monster, damage,data.valueSpecial1, level);
        }
        return true;
    }
}

