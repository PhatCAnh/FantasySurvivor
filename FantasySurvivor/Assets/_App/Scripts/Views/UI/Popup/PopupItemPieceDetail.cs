using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemPieceDetail : View<GameApp>, IPopup
{
	[SerializeField] private Button _btnClose, _btnPurchase;
	[SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtNumber;
	[SerializeField] private Image _imgRank, _imgSkin;

	private ItemSlotUI _itemSlotUI;

	public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isPurchase = false)
	{
		_itemSlotUI = itemSlotEquipUI;
		_btnPurchase.onClick.AddListener(OnClickBtnPurchase);
		_btnPurchase.gameObject.SetActive(isPurchase);

		_imgSkin.sprite = imgSkin.sprite;
		_imgRank.sprite = imgRank.sprite;
		_txtName.text = itemData.dataConfig.name;
		_txtDescription.text = itemData.dataConfig.description;
		_btnClose.onClick.AddListener(Close);
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