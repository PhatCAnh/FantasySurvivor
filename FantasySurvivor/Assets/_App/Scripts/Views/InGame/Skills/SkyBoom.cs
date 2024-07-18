using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FantasySurvivor;
using _App.Scripts.Controllers;
using Unity.Mathematics;

public class SkyBoom : SkillFallActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFX("Sky boom1");
    }
}
