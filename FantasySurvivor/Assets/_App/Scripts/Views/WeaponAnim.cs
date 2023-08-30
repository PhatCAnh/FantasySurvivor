using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnim : MonoBehaviour
{
    private TowerView _tower;

    private void Start()
    {
        _tower = GetComponentInParent<TowerView>();
    }

    private void Attack()
    {
        _tower.Attack();
    }

    private void DoneAnim()
    {
        _tower.IdleState();
    }
}
