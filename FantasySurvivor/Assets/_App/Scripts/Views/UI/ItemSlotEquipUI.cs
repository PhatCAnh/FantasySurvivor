using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEquipUI : ItemSlotUI
{
    [SerializeField] private Sprite _baseSprite;
    [SerializeField] private Sprite _baseSpriteRank;

    private bool _isEquip;
    
    public override void Init(ItemEquipData dataUI)
    {
        base.Init(dataUI);
        _isEquip = true;
    }
    
    protected override void OnClickBtn()
    {
        if(!_isEquip) return;
        parent.UnEquipItem(data.dataUi.type, data);
    }

    

    public void ResetData()
    {
        if(!_isEquip) return;
        data = null;
        image.sprite = _baseSprite;
        imageRank.sprite = _baseSpriteRank;
        _isEquip = false;
        btn.onClick.RemoveAllListeners();
    }
}
