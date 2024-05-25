using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : View<GameApp>
{
    private Monster mons;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        mons = transform.parent.GetComponent<Monster>();
    }

    private void DeadEffect()
    {
        Singleton<PoolController>.instance.ReturnObject(mons.type, mons.gameObject);
    }
}
