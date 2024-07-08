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

    private ShopUI _parent;

    private ItemController itemController => Singleton<ItemController>.instance;

    public void Init(ItemData data, int price, ShopUI parent, ItemId typePrice = ItemId.Gold, int count = 0)
    {
        this._data = data;
        this._parent = parent;
        
        _txtName.text = data.dataConfig.name;
        _txtPrice.text = price.ToString();
        _txtCount.text = count.ToString();
        _imgRank.sprite = itemController.GetSpriteRank(data.rank);
        _imgItem.sprite = data.dataUi.skin;

        _imgTypePrice.sprite = typePrice is ItemId.Gem ? _spriteGem : _spriteGold;
        
        _btnBuy.onClick.AddListener(OnClickBtnBuy);
    }

    private void OnClickBtnBuy()
    {
        Debug.Log("Clicked Btn Buy");
    }
}
