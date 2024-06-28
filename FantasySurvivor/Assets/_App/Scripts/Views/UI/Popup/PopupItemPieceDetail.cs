using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemPieceDetail : View<GameApp>, IPopup
{
	[SerializeField] private Button _btnClose, _btnPurchase,_btnSale;
	[SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtNumber;
	[SerializeField] private Image _imgRank, _imgSkin;

	private ItemSlotUI _itemSlotUI;
    private ItemInBag _dataInBag;
    private float  _currentCoin;

    public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isPurchase = false)
	{
        _currentCoin = app.models.dataPlayerModel.Coin;
        _itemSlotUI = itemSlotEquipUI;
		_btnPurchase.onClick.AddListener(OnClickBtnPurchase);
        //_btnPurchase.gameObject.SetActive(isPurchase);
        _imgSkin.sprite = imgSkin.sprite;
		_imgRank.sprite = imgRank.sprite;
		_txtName.text = itemData.dataConfig.name;
		_txtDescription.text = itemData.dataConfig.description;
		_btnClose.onClick.AddListener(Close);
        _btnSale.onClick.AddListener(SaleItem);
    }
    private void SaleItem()
    {

        app.models.dataPlayerModel.AddCoins(99);
        app.models.dataPlayerModel.RemovePiece(ItemId.PieceFire, 2);

        Debug.Log(_currentCoin);
        Close();
    }
    public void Open()
	{
	}
	public void Close()
	{
		_itemSlotUI.isShow = false;
		Destroy(gameObject);
	}
	public void OnClickBtnPurchase()
	{
		Debug.Log("Clicked btn purchase");
	}
}