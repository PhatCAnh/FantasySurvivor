using System;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Purchasing;

namespace _App.Scripts.Controllers
{
    public class IAPController: Controller<GameApp>, IStoreListener
    {
        private IStoreController iStoreController;

        private void Start()
        {
            Init();
        }

        private void Awake()
        {
            Singleton<IAPController>.Set(this);
        }
		
        protected override void OnDestroy()
        {
            base.OnDestroy();
            Singleton<IAPController>.Unset(this);
        }

        public void Init()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("coin_100", ProductType.Consumable);
            builder.AddProduct("coin_50", ProductType.Consumable);
            builder.AddProduct("coin_10", ProductType.Consumable);
            UnityPurchasing.Initialize(this, builder);
        }

        public void BuyItem(string id)
        {
            iStoreController.InitiatePurchase(id);
        }
        
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("Initialized IAP");
            iStoreController = controller;
        }
        
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var product = purchaseEvent.purchasedProduct;

            Debug.Log("Purchase successful product:" + product.definition.id);


            int value = 0;

            if (product.definition.id == "coin_10")
            {
                value = 20;
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
}