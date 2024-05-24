using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : SkillBulletActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
    }
}
