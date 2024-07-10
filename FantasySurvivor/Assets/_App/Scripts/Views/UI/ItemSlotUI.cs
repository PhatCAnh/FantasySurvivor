using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemSlotUI : View<GameApp>
{
    public ItemInBag itemInBag;
    public ItemData itemData;
    [SerializeField] protected Image image;
    [SerializeField] protected Image imageRank;
    [SerializeField] protected Button btn;
    [SerializeField] private TextMeshProUGUI txtNumber;

    protected CharacterInformation parent;

    public bool isShow = false;
    
    public void Init(ItemInBag data, CharacterInformation ui)
    {
        parent = ui;
        if(data != null)
        {
            InitData(data);
        }
        btn.onClick.AddListener(OnClickBtn);
    }

    
    public virtual void Init(ItemInBag data)
    {
        InitData(data);
        btn.onClick.AddListener(OnClickBtn);
    }

    protected virtual void OnClickBtn()
    {
        if(isShow) return;
        isShow = true;
        var type = itemData.dataConfig.type;
        if(type == 0)
        {
            app.resourceManager.ShowPopup(PopupType.ItemPieceDetail).TryGetComponent(out PopupItemPieceDetail popup);
            popup.Init(this, itemInBag, itemData, image, imageRank);
        }
        else
        {
            app.resourceManager.ShowPopup(PopupType.ItemEquipDetail).TryGetComponent(out PopupItemEquipDetail popup);
            popup.Init(this, itemInBag, itemData, image, imageRank);
        }

    }

    public virtual void Action(int value)
    {
        parent.EquipItem(itemData.dataConfig.type, itemInBag, value);

        Destroy(gameObject);
    }

    public void UpdateLevel(ItemInBag data)
    {
        txtNumber.text = $"{data.level}/{app.configs.dataStatRankItemEquip.GetConfig(data.rank).levelLimit}";
    }

    private void InitData(ItemInBag data)
    {
        var itemController = Singleton<ItemController>.instance;
        this.itemInBag = data;
        this.itemData = itemController.GetDataItem(data.id, data.rank);
        image.sprite = itemData.dataUi.skin;
        imageRank.sprite = itemController.GetSpriteRank(data.rank);
        if(data.quantity != 0)
        {
            txtNumber.text = $"{data.quantity}";
        } else if(data.level != 0)
        {
            txtNumber.text = $"{data.level}/{app.configs.dataStatRankItemEquip.GetConfig(data.rank).levelLimit}";
        }
    }
}
