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
    
    public override void Init(ItemEquipData dataUI)
    {
        base.Init(dataUI);
        isEquip = true;
        _imgEquip.SetActive(true);
        _imgUnEquip.SetActive(false);
    }
    
    protected override void OnClickBtn()
    {
        if(!isEquip) return;
        parent.UnEquipItem(data.dataUi.type, data);
    }

    public void ResetData()
    {
        if(!isEquip) return;
        data = null;
        _imgEquip.SetActive(false);
        _imgUnEquip.SetActive(true);
        imageRank.sprite = _baseRank;
        isEquip = false;
        btn.onClick.RemoveAllListeners();
    }
}
