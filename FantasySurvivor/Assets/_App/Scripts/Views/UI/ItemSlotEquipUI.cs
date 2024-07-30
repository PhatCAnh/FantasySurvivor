using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemSlotEquipUI : ItemSlotUI
{
    [SerializeField] private GameObject _imgEquip, _imgUnEquip;
    [SerializeField] private Sprite _baseRank;
    public bool isEquip;
    
    public override void Init(ItemInBag dataUI)
    {
        base.Init(dataUI);
        isEquip = false;
        _imgEquip.SetActive(true);
        _imgUnEquip.SetActive(false);
        if (parent == null)
        {
            parent = GetComponentInParent<CharacterInformation>();
            if (parent == null)
            {
                Debug.LogError("CharacterInformation component not found in parent.");
            }
        }
    }

    
    protected override void OnClickBtn()
    {
        if(isShow || !isEquip) return;
        isShow = true;
        app.resourceManager.ShowPopup(PopupType.ItemEquipDetail).TryGetComponent(out PopupItemEquipDetail popup);
        popup.Init(this, itemInBag, itemData, image, imageRank, isEquip);
    }

    public override void Action(int value)
    {
        if(!isEquip) return;
        isShow = false;
        parent.UnEquipItem(itemData.dataConfig.type, itemInBag, value);
    }

    public void ResetData()
    {
        if(!isEquip) return;
        itemInBag = null;
        itemData = null;
        _imgEquip.SetActive(false);
        _imgUnEquip.SetActive(true);
        imageRank.sprite = _baseRank;
        isEquip = false;
        btn.onClick.RemoveAllListeners();
    }
}
