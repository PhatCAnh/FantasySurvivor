using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;

public enum TypeItemSell
{
    Gold,
    Gem,
    Free,
    Ads
}

public class ShopUI : View<GameApp>
{
    [SerializeField] private Transform _containerDaily;
    [SerializeField] private GameObject _prefabItemSlotUI;

    private ItemController itemController => Singleton<ItemController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        Instantiate(_prefabItemSlotUI, _containerDaily).TryGetComponent(out ItemSlotUIShop item);
        item.Init(itemController.GetDataItem(ItemId.Ring, ItemRank.Normal), 1000, this, TypeItemSell.Ads, 0);

        Instantiate(_prefabItemSlotUI, _containerDaily).TryGetComponent(out ItemSlotUIShop item1);
        item1.Init(itemController.GetDataItem(ItemId.Armor, ItemRank.Normal), 2000, this, TypeItemSell.Ads, 0);

        Instantiate(_prefabItemSlotUI, _containerDaily).TryGetComponent(out ItemSlotUIShop item2);
        item2.Init(itemController.GetDataItem(ItemId.Axe, ItemRank.Normal), 3000, this, TypeItemSell.Ads, 0);
    }
}