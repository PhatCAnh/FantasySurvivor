using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemEquipDetail : View<GameApp>, IPopup
{
    [SerializeField] private Image _imgSkin, _imgRank, _imgIcon;
    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtValue, _txtBtnAction, _txtPriceUpdate;
    [SerializeField] private Button _btnAction, _btnClose, _btnUpdate;

    [SerializeField] private Sprite _spriteWeapon, _spriteArmor, _spriteShoes, _spriteGloves, _spriteHat, _spriteRing;

    private ItemSlotUI _itemSlotUI;

    private ItemInBag _dataInBag;

    public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isEquip = false)
    {
        _itemSlotUI = itemSlotEquipUI;
        _dataInBag = dataInBag;
        if(isEquip)
        {
            _txtBtnAction.text = "Unequip";
        }
        var dataRankStat = app.configs.dataStatRankItemEquip.GetConfig(dataInBag.rank);
        _imgSkin.sprite = imgSkin.sprite;
        _imgRank.sprite = imgRank.sprite;
        _txtName.text = $"{itemData.dataConfig.name} - Level: {dataInBag.level} / {dataRankStat.levelLimit}";
        _txtValue.text = $"+ {itemData.dataConfig.baseValue}";
        _txtPriceUpdate.text = $"{(1000 + (int)dataInBag.rank * 250) * dataInBag.level}";
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
        app.models.dataPlayerModel.UpdateItem(_dataInBag);
    }
}
