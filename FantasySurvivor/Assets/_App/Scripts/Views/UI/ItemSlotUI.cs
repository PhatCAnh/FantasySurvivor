using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemSlotUI : View<GameApp>
{
    public ItemEquipData data;
    [SerializeField] protected Image image;
    [SerializeField] protected Image imageRank;
    [SerializeField] protected Button btn;

    protected StatUI parent;
    
    public void Init(ItemEquipData data, StatUI ui)
    {
        parent = ui;
        if(data != null)
        {
            InitData(data);
        }
        btn.onClick.AddListener(OnClickBtn);
    }
    
    public virtual void Init(ItemEquipData data)
    {
        InitData(data);
        btn.onClick.AddListener(OnClickBtn);
    }

    protected virtual void OnClickBtn()
    {
        parent.EquipItem(data.dataUi.type, data);
        Destroy(gameObject);
    }

    private void InitData(ItemEquipData data)
    {
        this.data = data;
        image.sprite = this.data.dataUi.skin;
        imageRank.sprite = data.spriteRank;
    }
}
