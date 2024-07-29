using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBomberMonster : Monster
{
    public float dieCountdown;
    private float initialDieCountdown;


    private void Awake()
    {
        initialDieCountdown = dieCountdown;
    }
    protected override void HandlePhysicUpdate()
    {
        base.HandlePhysicUpdate();

        dieCountdown -= Time.deltaTime;
        if (dieCountdown <= 0f)
        {
            Die();
        }
    }

    public override void TakeDamage(float damage, TextPopupType type, bool isCritical = false, Action callBackDamaged = null, Action callBackKilled = null)
    {
        base.TakeDamage(damage, type, isCritical, callBackDamaged, callBackKilled);
        this.Die();
    }

    public override void Attack()
    {
        base.Attack();
        model.currentHealthPoint = 0;
        Die(true);
    }

    public override void Die(bool selfDie = true)
    {
        base.Die(selfDie);
        dieCountdown = initialDieCountdown;
    }
}
