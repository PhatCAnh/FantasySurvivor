using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Purchasing;

[Serializable]
public class ConsumableItem
{
    public string name;
    public string id;
    public string desc;
    public float price;
}

public class IAPTest : View<GameApp>
{
    public ConsumableItem item;
    public void Consumable_btn_click()
    {
        Singleton<IAPController>.instance.BuyItem(item.id);
    }
}