using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUIShop : View<GameApp>
{
    [SerializeField] private TextMeshProUGUI _txtName, _txtPrice, _txtCount;
    [SerializeField] private Image _imgRank, _imgItem, _imgTypePrice;
    [SerializeField] private Button _btnBuy;
    [SerializeField] private Sprite _spriteGold, _spriteGem;

    private ItemData _data;

    private int _count, _price;

    private Sprite _rank, _skin, _typePrice;

    private ShopUI _parent;

    private ItemController itemController => Singleton<ItemController>.instance;

    public void Init(ItemData data, int price, ShopUI parent, TypeItemSell typePrice, int count = 0)
    {
        _parent = parent;

        _txtName.text = data.dataConfig.name;

        _data = data;

        _count = count;

        _price = price;

        _rank = itemController.GetSpriteRank(data.rank);

        _skin = data.dataUi.skin;

        _txtCount.text = _count.ToString();
        
        if (typePrice is TypeItemSell.Gold)
        {
            _typePrice = _spriteGold;
        }
        else if (typePrice is TypeItemSell.Gem)
        {
            _typePrice = _spriteGem;
        }
        else if (typePrice is TypeItemSell.Ads)
        {
            _txtCount.text = "Watch Ads";
        }

        _txtPrice.text = _price.ToString();
        
        _txtCount.gameObject.SetActive(_count != 0);
        _imgRank.sprite = _rank;
        _imgItem.sprite = _skin;
        _imgTypePrice.sprite = _typePrice;

        if (typePrice == TypeItemSell.Ads)
        {
            _btnBuy.onClick.AddListener(OnClickWatchAds);
        }
        else
        {
            _btnBuy.onClick.AddListener(OnClickBtnBuy);
        }
    }

    private void OnClickBtnBuy()
    {
        app.resourceManager.ShowPopup(PopupType.PurchaseItemPopup)
            .TryGetComponent(out PurchaseItemPopup purchaseItemPopup);
        purchaseItemPopup.Init(_data, _price, _count, _rank, _skin, _typePrice);
    }

    private void OnClickWatchAds()
    {
        Singleton<AdsController>.instance.ShowReward(() =>
        {
            app.resourceManager.ShowPopup(PopupType.PurchaseItemPopup)
                .TryGetComponent(out PurchaseItemPopup purchaseItemPopup);
            purchaseItemPopup.Init(_data, _price, _count, _rank, _skin, _typePrice);
        });
    }
}