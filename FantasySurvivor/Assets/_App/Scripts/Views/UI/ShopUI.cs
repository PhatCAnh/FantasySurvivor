using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;

public enum TypeItemSell
{
    Gold,
    Gem,
    Free,
    Ads
}

public class ShopUI : View<GameApp>
{
    [SerializeField] private Transform _containerCoin, _cointainerGem;
    [SerializeField] private GameObject _prefabItemSlotUI;

    private ItemController itemController => Singleton<ItemController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        Instantiate(_prefabItemSlotUI, _containerCoin).TryGetComponent(out ItemSlotUIShop coin1);
        coin1.Init(itemController.GetDataItem(ItemId.Ring, ItemRank.Normal), 1000, this, TypeItemSell.Ads, 0);

        Instantiate(_prefabItemSlotUI, _containerCoin).TryGetComponent(out ItemSlotUIShop coin2);
        coin2.Init(itemController.GetDataItem(ItemId.Armor, ItemRank.Normal), 2000, this, TypeItemSell.Gold, 0);

        Instantiate(_prefabItemSlotUI, _containerCoin).TryGetComponent(out ItemSlotUIShop coin3);
        coin3.Init(itemController.GetDataItem(ItemId.Axe, ItemRank.Normal), 3000, this, TypeItemSell.Gold, 0);
        
        Instantiate(_prefabItemSlotUI, _cointainerGem).TryGetComponent(out ItemSlotUIShop gem1);
        gem1.Init(itemController.GetDataItem(ItemId.Ring, ItemRank.Normal), 1000, this, TypeItemSell.Ads, 0);

        Instantiate(_prefabItemSlotUI, _cointainerGem).TryGetComponent(out ItemSlotUIShop gem2);
        gem2.Init(itemController.GetDataItem(ItemId.Armor, ItemRank.Normal), 2000, this, TypeItemSell.Gem, 0);

        Instantiate(_prefabItemSlotUI, _cointainerGem).TryGetComponent(out ItemSlotUIShop gem3);
        gem3.Init(itemController.GetDataItem(ItemId.Axe, ItemRank.Normal), 3000, this, TypeItemSell.Gem, 0);
    }
}