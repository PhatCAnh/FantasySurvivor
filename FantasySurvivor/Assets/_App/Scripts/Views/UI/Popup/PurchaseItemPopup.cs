using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnPurchase, _btnDimer, _btnClose;

    [SerializeField] private TextMeshProUGUI _txtPrice, _txtCount;
    
    [SerializeField] private Transform _goMainContent;
    
    [SerializeField] private Image _imgRank, _imgSkin, _imgTypePrice;
    
    private ItemData _data;

    private int _count, _price;

    public void Init(ItemData data, int price, int count, Sprite rank, Sprite skin, Sprite typePrice, bool canBuy)
    {
        _data = data;
        
        _count = count;

        _price = price;

        _txtPrice.text = price.ToString();

        _txtCount.text = count.ToString();
        
        _txtCount.gameObject.SetActive(_count != 0);

        _imgRank.sprite = rank;

        _imgSkin.sprite = skin;

        _imgTypePrice.sprite = typePrice;
        
        _btnDimer.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
        _btnPurchase.onClick.AddListener(OnClickBtnPurchase);

        _btnPurchase.interactable = canBuy;
        
        Open();
    }
    
    public void Open()
    {
        _goMainContent.localScale = Vector3.zero;

        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    private void OnClickBtnPurchase()
    {
        app.models.dataPlayerModel.AddItemEquipToBag(_data.id, _data.rank, 0);
        app.models.dataPlayerModel.Gold -= _price;
        app.resourceManager.ShowPopup(PopupType.RewardGetPopup).TryGetComponent(out PopupReward rewardGetPopup);
        rewardGetPopup.Init(new List<ItemInBag> { new (_data.id.ToString(), _data.rank.ToString(),0, _count)});
        Close();
    }
}
