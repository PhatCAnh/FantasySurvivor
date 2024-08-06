using System;
using System.Collections;
using System.Collections.Generic;
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

public class IAPTest : View<GameApp>, IStoreListener
{
    private IStoreController iStoreController;

    public ConsumableItem item;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        SetupBuilder();
    }

    private void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(item.id, ProductType.Consumable);
        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("Initialized IAP");
        iStoreController = controller;
    }

    public void Consumable_btn_click()
    {
        iStoreController.InitiatePurchase(item.id);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        Debug.Log("Purchase successful product:" + product.definition.id);


        int value = 0;

        if (product.definition.id == "coin_10")
        {
            value = 10;
        }
        else if (product.definition.id == "coin_50")
        {
            value = 50;
        }
        else if (product.definition.id == "coin_100")
        {
            value = 100;
        }

        app.models.dataPlayerModel.Gem += value;
        app.resourceManager.ShowPopup(PopupType.RewardGetPopup).TryGetComponent(out PopupReward rewardGetPopup);
        rewardGetPopup.Init(new List<ItemInBag> { new(ItemId.Gem.ToString(), ItemRank.Rare.ToString(), 0, value) });

        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("InitializeFailed" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("InitializeFailed" + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("PurchaseFailed");
    }
}