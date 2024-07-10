using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlotCVUI : View<GameApp>
{
    public ItemInBag itemInBag;
    public ItemData itemData;
    [SerializeField] protected Image image;
    [SerializeField] protected Image imageRank;
    [SerializeField] protected Button btn;
    [SerializeField] private TextMeshProUGUI txtNumber;
    protected ConvertItemPopup parent;
    private int _numberValue;
    public bool isShow = true;

    public void Init(ItemInBag data, ConvertItemPopup ui)
    {
        parent = ui;
        if (data != null)
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
        parent.ChosenItem(itemData.dataConfig.type, itemInBag);
        Destroy(gameObject);
    }

    private void InitData(ItemInBag data)
    {
        var itemController = Singleton<ItemController>.instance;
        this.itemInBag = data;
        this.itemData = itemController.GetDataItem(data.id, data.rank, data.level);
        image.sprite = itemData.dataUi.skin;
        imageRank.sprite = itemController.GetSpriteRank(data.rank);
        if (data.quantity != 0)
        {
            txtNumber.text = $"{data.quantity}";
        }
        else if (data.level != 0)
        {
            txtNumber.text = $"{data.level}/{app.configs.dataStatRankItemEquip.GetConfig(data.rank).levelLimit}";
        }
    }
}
