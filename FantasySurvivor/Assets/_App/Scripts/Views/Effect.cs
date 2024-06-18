using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

public class Effect : View<GameApp>
{
    protected virtual void DoneEffect()
    {
        
        Destroy(gameObject);
    }
}
