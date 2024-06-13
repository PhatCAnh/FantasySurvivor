using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform _goCostUpdate;

    private ItemSlotUI _itemSlotUI;

    private ItemInBag _dataInBag;

    private ItemData _itemData;

    private DataStatRankItemEquip _dataStatRank;

    private float _costUpdate, _currentCoin;

    private Tween _punchCostPrice;

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
        _currentCoin = app.models.dataPlayerModel.Coin;
        var textCurrentCoin = _currentCoin < _costUpdate ? $"<color=red>{_currentCoin}</color>" : $"{_currentCoin}";
        
        _txtPriceUpdate.text = textCurrentCoin + $"/{_costUpdate}";
        _txtDescription.text = itemData.dataConfig.description;
        _btnClose.onClick.AddListener(Close);
        _btnAction.onClick.AddListener(Action);
        _btnUpdate.onClick.AddListener(UpdateLevel);
        switch (itemData.dataConfig.type)
        {
            case ItemType.Weapon:
                _imgIcon.sprite = _spriteWeapon;
                break;
            case ItemType.Armor:
                _imgIcon.sprite = _spriteArmor;
                break;
            case ItemType.Shoes:
                _imgIcon.sprite = _spriteShoes;
                break;
            case ItemType.Gloves:
                _imgIcon.sprite = _spriteGloves;
                break;
            case ItemType.Hat:
                _imgIcon.sprite = _spriteHat;
                break;
            case ItemType.Ring:
                _imgIcon.sprite = _spriteRing;
                break;
        }
    }

    public void Action()
    {
        _itemSlotUI.Action();
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
        _txtPriceUpdate.text = $"{(1000 + (int)_dataInBag.rank * 250) * _dataInBag.level}";
    }
}
