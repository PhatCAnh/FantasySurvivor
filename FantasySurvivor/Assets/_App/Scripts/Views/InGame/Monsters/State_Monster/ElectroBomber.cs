using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ElectroBomber : Monster
{

    [SerializeField] private GameObject _deadEffect;

    public override void Attack()
    {
        base.Attack();
        Die(true);
    }

    public override void Die(bool selfDie = false)
    {
        base.Die(selfDie);
        Instantiate(_deadEffect, transform.position, quaternion.identity);
    }
}
