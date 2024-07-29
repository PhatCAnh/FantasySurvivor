using ArbanFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMonster : Monster
{
    private GameController gameController => Singleton<GameController>.instance;


    public override void TakeDamage(float damage, TextPopupType type, bool isCritical = false, Action callBackDamaged = null, Action callBackKilled = null)
    {
        base.TakeDamage(damage, type, isCritical, callBackDamaged, callBackKilled);
    }

    public override void Die(bool selfDie = true)
    {
        base.Die(true);
    }
}
