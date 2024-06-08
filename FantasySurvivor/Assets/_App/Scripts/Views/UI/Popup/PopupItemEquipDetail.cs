using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupItemEquipDetail : View<GameApp>, IPopup
{
    [SerializeField] private Image _imgSkin, _imgRank, _imgIcon;
    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtValue, _txtBtnAction;
    [SerializeField] private Button _btnAction, _btnClose, _btnUpdate;
    [SerializeField] private GameObject _goIcon, _goText;

    [SerializeField] private Sprite _spriteWeapon, _spriteArmor, _spriteShoes, _spriteGloves, _spriteHat, _spriteRing;

    private ItemSlotUI _itemSlotUI;

    public void Init(ItemSlotUI itemSlotEquipUI, ItemInBag dataInBag, ItemData itemData, Image imgSkin, Image imgRank, bool isEquip = false)
    {
        _itemSlotUI = itemSlotEquipUI;
        if(isEquip)
        {
            _txtBtnAction.text = "Unequip";
        }
        
        _imgSkin.sprite = imgSkin.sprite;
        _imgRank.sprite = imgRank.sprite;
        _txtName.text = itemData.dataConfig.name;
        _txtValue.text = $"+ {itemData.dataConfig.baseValue}";
        _txtDescription.text = itemData.dataConfig.description;
        _btnClose.onClick.AddListener(Close);
        _btnAction.onClick.AddListener(Action);
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
            case ItemType.Piece:
                _goIcon.SetActive(false);
                _goText.SetActive(true);
                _goText.GetComponent<TextMeshProUGUI>().text = dataInBag.quantity.ToString();
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
}
