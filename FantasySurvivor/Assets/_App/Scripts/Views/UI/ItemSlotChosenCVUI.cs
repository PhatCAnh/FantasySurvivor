using ArbanFramework;
using UnityEditor;
using UnityEngine;

public class ItemSlotChosenCVUI : ItemSlotCVUI
{
    [SerializeField] private GameObject _imgEquip, _imgUnEquip;
    [SerializeField] private Sprite _baseRank;
    public ItemType ItemType { get; set; }
    public bool isChosen;
    public override void Init(ItemInBag dataUI)
    {
        base.Init(dataUI);
        if (dataUI != null)
        {
            InitData(dataUI);
        }
        ItemType = ItemType.None;
        isChosen = false;
        _imgEquip.SetActive(true);
        _imgUnEquip.SetActive(false);
    }

    protected override void OnClickBtn()
    {
        parent.UnChosenItem(itemData.dataConfig.type, itemInBag);
    }

    /*public override void Action(int value)
    {
        if (!isChosen) return;
        isShow = false;
    }
*/
    public void ResetData()
    {
        if (!isChosen) return;
        itemInBag = null;
        itemData = null;
        _imgEquip.SetActive(false);
        _imgUnEquip.SetActive(true);
        imageRank.sprite = _baseRank;
        isChosen = false;
        btn.onClick.RemoveAllListeners();
    }
    private void InitData(ItemInBag data)
    {
        var itemController = Singleton<ItemController>.instance;
        this.itemInBag = data;
        this.itemData = itemController.GetDataItem(data.id, data.rank, data.level);
        image.sprite = itemData.dataUi.skin;
        imageRank.sprite = itemController.GetSpriteRank(data.rank);
        
    }
}