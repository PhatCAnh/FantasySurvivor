using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemSlotEquipUI : ItemSlotUI
{
    [SerializeField] private Sprite _baseSprite;
    [SerializeField] private Sprite _baseSpriteRank;
    
    [FormerlySerializedAs("_isEquip")]
    public bool isEquip;
    
    public override void Init(ItemEquipData dataUI)
    {
        base.Init(dataUI);
        isEquip = true;
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
        image.sprite = _baseSprite;
        imageRank.sprite = _baseSpriteRank;
        isEquip = false;
        btn.onClick.RemoveAllListeners();
    }
}
