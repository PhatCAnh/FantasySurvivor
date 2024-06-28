using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemEquipDetail : View<GameApp>, IPopup
{
	[SerializeField] private Image _imgSkin, _imgRank, _imgIcon;

	[SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtValue, _txtBtnAction, _txtPriceUpdate;

	[SerializeField] private Button _btnAction, _btnClose, _btnUpdate;

	[SerializeField] private Sprite _spriteWeapon, _spriteArmor, _spriteShoes, _spriteGloves, _spriteHat, _spriteRing;

	[SerializeField] private Sprite _spriteAtk, _spriteHealth;

	[SerializeField] private Transform _goCostUpdate, _goMainContent;

	[SerializeField] private GameObject _prefabStat, _statContainer;

	private ItemSlotUI _itemSlotUI;

	private ItemInBag _dataInBag;

	private ItemData _itemData;

	private DataStatRankItemEquip _dataStatRank;

	private float _costUpdate, _currentCoin;

	private Tween _punchCostPrice;

	private int _numberValue;

	public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isEquip = false)
	{
		_itemSlotUI = itemSlotEquipUI;
		_dataInBag = dataInBag;
		_itemData = itemData;
		if(isEquip)
		{
			_txtBtnAction.text = "Unequip";
		}
		_dataStatRank = app.configs.dataStatRankItemEquip.GetConfig(dataInBag.rank);
		_imgSkin.sprite = imgSkin.sprite;
		_imgRank.sprite = imgRank.sprite;
		_txtName.text = $"{itemData.dataConfig.name} - Level: {dataInBag.level} / {_dataStatRank.levelLimit}";
		_txtValue.text = $"+ {itemData.dataConfig.baseValue}";

		_costUpdate = (1000 + (int) dataInBag.rank * 250) * dataInBag.level;
		_currentCoin = app.models.dataPlayerModel.Gold;
		var textCurrentCoin = _currentCoin < _costUpdate ? $"<color=red>{_currentCoin}</color>" : $"{_currentCoin}";

		_txtPriceUpdate.text = textCurrentCoin + $"/{_costUpdate}";
		_txtDescription.text = itemData.dataConfig.description;
		_btnClose.onClick.AddListener(Close);
		_btnAction.onClick.AddListener(Action);
		_btnUpdate.onClick.AddListener(UpdateLevel);
		var nameStat = "";
		switch (itemData.dataConfig.type)
		{
			case ItemType.Weapon:
				_imgIcon.sprite = _spriteWeapon;
				nameStat = "Atk";
				break;
			case ItemType.Armor:
				_imgIcon.sprite = _spriteArmor;
				nameStat = "Health";
				break;
			case ItemType.Shoes:
				_imgIcon.sprite = _spriteShoes;
				nameStat = "Health";
				break;
			case ItemType.Gloves:
				_imgIcon.sprite = _spriteGloves;
				nameStat = "Atk";
				break;
			case ItemType.Hat:
				_imgIcon.sprite = _spriteHat;
				nameStat = "Health";
				break;
			case ItemType.Ring:
				_imgIcon.sprite = _spriteRing;
				nameStat = "Atk";
				break;
		}

		Instantiate(_prefabStat, _statContainer.transform).TryGetComponent(out StatInPopupDescription statInPopupDescription);
		var data = Singleton<GameController>.instance.GetDataStat(nameStat, dataInBag.rank);
		_numberValue = _itemData.dataConfig.baseValue + data.Item2 * (dataInBag.level - 1);
		Sprite sprite = null;
		switch (data.Item3)
		{
			case StatId.Atk:
				sprite = _spriteAtk;
				break;
			case StatId.Health:
				sprite = _spriteHealth;
				break;

		}
		statInPopupDescription.Init(data.Item1, _numberValue, sprite);
		Open();
	}

	public void Action()
	{
		_itemSlotUI.Action(_numberValue);
		Close();
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
				_itemSlotUI.isShow = false;
				Destroy(gameObject);
			});
	}

	private void UpdateLevel()
	{
		if(_currentCoin < _costUpdate)
		{
			if(_punchCostPrice != null && _punchCostPrice.IsPlaying()) return;
			_punchCostPrice = _goCostUpdate
				.DOPunchScale(Vector3.one, 1, 1, 3)
				.OnComplete(() => _punchCostPrice = null);
			return;
		}

		app.models.dataPlayerModel.UpdateItem(_dataInBag);
		_itemSlotUI.UpdateLevel(_dataInBag);
		_txtName.text = $"{_itemData.dataConfig.name} - Level: {_dataInBag.level} / {_dataStatRank.levelLimit}";
		_txtPriceUpdate.text = $"{(1000 + (int) _dataInBag.rank * 250) * _dataInBag.level}";
	}
}