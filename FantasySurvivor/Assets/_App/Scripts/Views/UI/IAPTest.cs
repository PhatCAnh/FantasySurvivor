using System;
using System.Collections;
using System.Collections.Generic;
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

public class IAPTest : MonoBehaviour, IStoreListener
{
    private IStoreController iStoreController;
    
    public ConsumableItem item;
    
    private void Start()
    {
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

        if (product.definition.id == item.id)
        {
            //mua xong thi lam gi
            Debug.Log("Purchase successful product");
        }

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
