using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyGiftItem : View<GameApp>
{
	[SerializeField] private TextMeshProUGUI _txtValue;
	[SerializeField] private GameObject _goMissing, _goClear, _goHighlight, _goTomorrow;
	[SerializeField] private Button _btnClaim;

	private ItemId _id;
	private ItemType _type;
	private int _quantity;
	private DailyGiftPopup _parent;
	private ItemController itemController => Singleton<ItemController>.instance;

	public DailyGiftStatus status;

	public void Init(ItemId id, ItemType type, int quantity, DailyGiftStatus status, DailyGiftPopup parent)
	{
		_id = id;
		_type = type;
		_quantity = quantity;
		this.status = status;
		this._parent = parent;
		_txtValue.text = $"x{_quantity}";

		SetData();
	}

	private void SetData()
	{
		_goMissing.SetActive(false);
		_goClear.SetActive(false);
		_goHighlight.SetActive(false);
		_goTomorrow.SetActive(false);
		_btnClaim.interactable = false;

		switch (status)
		{
			case DailyGiftStatus.Missing:
				_goMissing.SetActive(true);
				break;
			case DailyGiftStatus.Claimed:
				_goClear.SetActive(true);
				break;
			case DailyGiftStatus.Ready:
				_goHighlight.SetActive(true);
				_btnClaim.interactable = true;
				_btnClaim.onClick.AddListener(OnClickBtnClaim);
				break;
			case DailyGiftStatus.Waiting:
				_goTomorrow.SetActive(true);
				break;
		}
	}

	private void OnClickBtnClaim()
	{
		status = DailyGiftStatus.Claimed;
		itemController.ClaimItem(_id, _type, ItemRank.Rare, 0, _quantity);
		_parent.OnClickClaim();
		SetData();
	}
}