using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class TransferItemPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnTransfer, _btnBack;

    [SerializeField] private Transform _slotItemEquipContainer;

    [SerializeField] private GameObject _slotItemEquipPrefab;
    [SerializeField] private ItemSlotChosenCVUI _slotNone1, _slotNone2, _slotNone3;



    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip1;
    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip2;
    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip3;

    private ItemController itemController => Singleton<ItemController>.instance;

    private List<GameObject> _listItemSlot = new List<GameObject>();

    private int _numberValue;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        
        _btnBack.onClick.AddListener(Close);
 
    }



    public void Open()
    {

    }
    public void Close()
    {
        Destroy(gameObject);
    }
   
}
