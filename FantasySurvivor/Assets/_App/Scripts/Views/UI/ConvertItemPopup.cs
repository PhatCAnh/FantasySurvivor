using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ConvertItemPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnConvert, _btnBack, _btnTalismanConvert;

    [SerializeField] private Transform _slotItemEquipContainer, _goCostUpdate;

    [SerializeField] private GameObject _slotItemEquipPrefab;

    [SerializeField] private TextMeshProUGUI _txtPriceUpdate;

    [SerializeField] private ItemSlotChosenCVUI _slotNone1, _slotNone2, _slotNone3;

    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip1;
    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip2;
    private Dictionary<ItemType, ItemSlotChosenCVUI> _dicItemEquip3;

    private ItemController itemController => Singleton<ItemController>.instance;

    private List<GameObject> _listItemSlot = new List<GameObject>();

    private int _numberValue;


    private float _costUpdate, _currentCoin;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        _slotNone1.Init(null, this);
        _slotNone2.Init(null, this);
        _slotNone3.Init(null, this);

        _dicItemEquip1 = new Dictionary<ItemType, ItemSlotChosenCVUI>
        {
            {ItemType.Weapon, _slotNone1},
            {ItemType.Ring, _slotNone1},
            {ItemType.Gloves, _slotNone1},
            {ItemType.Hat, _slotNone1},
            {ItemType.Shoes, _slotNone1},
            {ItemType.Armor, _slotNone1},
        };
        _dicItemEquip2 = new Dictionary<ItemType, ItemSlotChosenCVUI>
        {
            {ItemType.Weapon, _slotNone2},
            {ItemType.Ring, _slotNone2},
            {ItemType.Gloves, _slotNone2},
            {ItemType.Hat, _slotNone2},
            {ItemType.Shoes, _slotNone2},
            {ItemType.Armor, _slotNone2},
        };
        _dicItemEquip3 = new Dictionary<ItemType, ItemSlotChosenCVUI>
        {
            {ItemType.Weapon, _slotNone3},
            {ItemType.Ring, _slotNone3},
            {ItemType.Gloves, _slotNone3},
            {ItemType.Hat, _slotNone3},
            {ItemType.Shoes, _slotNone3},
            {ItemType.Armor, _slotNone3},
        };
        _costUpdate = 0;
        _currentCoin = app.models.dataPlayerModel.Talisman;
        var textCurrentCoin = _currentCoin < _costUpdate ? $"<color=red>{_currentCoin}</color>" : $"{_currentCoin}";

        _txtPriceUpdate.text = textCurrentCoin + $"/{_costUpdate}";
        _btnBack.onClick.AddListener(Close);
        _btnConvert.onClick.AddListener(ConvertItem);
        _btnTalismanConvert.onClick.AddListener(ConvertItemWithTalisman);
        var dataPlayerModel = app.models.dataPlayerModel;

        foreach (var item in app.models.dataPlayerModel.BagItem)
        {
            var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
            go.TryGetComponent(out ItemSlotCVUI item1);
            item1.Init(item, this);
            _listItemSlot.Add(go);
        }
        var sortedItemSlots = _listItemSlot
           .Select(go => go.GetComponent<ItemSlotCVUI>())
           .OrderBy(itemSlot => GetItemTypeOrder(itemSlot.itemData.dataConfig.type))
           .ToList();
        for (int i = 0; i < sortedItemSlots.Count; i++)
        {
            sortedItemSlots[i].transform.SetSiblingIndex(i);
            sortedItemSlots[i].gameObject.SetActive(true);
        }
    }
    public void ChosenItem(ItemType type, ItemInBag data)
    {
        if (_slotNone1.itemData == null)
        {
            _slotNone1.ItemType = type;
            var slot = _dicItemEquip1[type];
            if (slot.isChosen) UnChosenItem(type, data);
            slot.Init(data);
        }
        else
                if (_slotNone1.itemData != null && _slotNone2.itemData == null)
        {
            _slotNone2.ItemType = type;
            var slot = _dicItemEquip2[type];
            if (slot.isChosen) UnChosenItem(type, data);
            slot.Init(data);
        }
        else
                if (_slotNone1.itemData != null
                    && _slotNone2.itemData != null
                    && _slotNone3.itemData == null)
        {
            _slotNone3.ItemType = type;
            var slot = _dicItemEquip3[type];
            if (slot.isChosen) UnChosenItem(type, data);
            slot.Init(data);
        }
        CheckUpdate();
    }
    public bool UnableChosen()
    {
        if (_slotNone1.itemData != null
         && _slotNone2.itemData != null
         && _slotNone3.itemData != null)
        {
            return false;
        }
        return true;
    }
    public void UnChosenItem(ItemType type, ItemInBag data)
    {
        if (data == _slotNone1.itemInBag)
        {
            _dicItemEquip1[type].ResetData();
            Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer)
                .TryGetComponent(out ItemSlotCVUI item);
            item.Init(data, this);
        }
        if (data == _slotNone2.itemInBag)
        {
            _dicItemEquip2[type].ResetData();
            Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer)
                .TryGetComponent(out ItemSlotCVUI item);
            item.Init(data, this);
        }
        if (data == _slotNone3.itemInBag)
        {
            _dicItemEquip3[type].ResetData();
            Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer)
                .TryGetComponent(out ItemSlotCVUI item);
            item.Init(data, this);
        }
    }
    public void Open()
    {

    }
    public void Close()
    {
        Destroy(gameObject);
    }
    protected int GetItemTypeOrder(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon: return 0;
            case ItemType.Armor: return 1;
            case ItemType.Shoes: return 2;
            case ItemType.Gloves: return 3;
            case ItemType.Hat: return 4;
            case ItemType.Ring: return 5;
            default: return int.MaxValue;
        }

    }
    
    
    
    public void ConvertItem()
    {
        if (UnableChosen()) return;
        if (_slotNone1.ItemType == _slotNone2.ItemType
            && _slotNone1.ItemType == _slotNone3.ItemType
            && _slotNone1.itemInBag.rank == _slotNone2.itemInBag.rank
            && _slotNone1.itemInBag.rank == _slotNone3.itemInBag.rank
            && maxRank())
        {
            if (Earthpunch.IsActionSuccessful(1 - ((float)_slotNone1.itemInBag.rank - 1) * (1f / 7f)))
            {
                app.models.dataPlayerModel.AddItemEquipToBag(_slotNone1.itemInBag.id, _slotNone1.itemInBag.rank + 1, 1);
            }
            app.models.dataPlayerModel.RemoveItem(_slotNone1.itemInBag);
            app.models.dataPlayerModel.RemoveItem(_slotNone2.itemInBag);
            app.models.dataPlayerModel.RemoveItem(_slotNone3.itemInBag);
            _slotNone1.ResetData();
            _slotNone2.ResetData();
            _slotNone3.ResetData(); 
            ReloadItem();
        }
    }
    public void ConvertItemWithTalisman()
    {
        if (UnableChosen()) return;
        if (_slotNone1.ItemType == _slotNone2.ItemType
            && _slotNone1.ItemType == _slotNone3.ItemType
            && _slotNone1.itemInBag.rank == _slotNone2.itemInBag.rank
            && _slotNone1.itemInBag.rank == _slotNone3.itemInBag.rank && maxRank())
        {
            if (Earthpunch.IsActionSuccessful(1 - ((float)_slotNone1.itemInBag.rank - 1) * (1f / 30f)))
            {
                app.models.dataPlayerModel.AddItemEquipToBag(_slotNone1.itemInBag.id, _slotNone1.itemInBag.rank + 1, 1);
                app.models.dataPlayerModel.Talisman -= ((int)_slotNone1.itemData.rank + 1 * 1); app.models.dataPlayerModel.RemoveItem(_slotNone1.itemInBag);
                app.models.dataPlayerModel.RemoveItem(_slotNone2.itemInBag);
                app.models.dataPlayerModel.RemoveItem(_slotNone3.itemInBag);
                _slotNone1.ResetData();
                _slotNone2.ResetData();
                _slotNone3.ResetData();
            }
            else
            {
                app.models.dataPlayerModel.Talisman -= ((int)_slotNone1.itemData.rank + 1 * 1);
                _slotNone1.ResetData();
                _slotNone2.ResetData();
                _slotNone3.ResetData();
            }
            ReloadItem();
        }
    }
    public void CheckUpdate()
    {
        if (_slotNone1 != null && _slotNone2 != null && _slotNone3 != null)
        {
            _costUpdate = ((int)_slotNone1.itemData.rank+1 * 1);
            _currentCoin = app.models.dataPlayerModel.Talisman;
            var textCurrentCoin = _currentCoin < _costUpdate ? $"<color=red>{_currentCoin}</color>" : $"{_currentCoin}";
            _txtPriceUpdate.text = textCurrentCoin + $"/{_costUpdate}";
        }
        else
        {
            _costUpdate = 0;
            _currentCoin = app.models.dataPlayerModel.Talisman;
            var textCurrentCoin = _currentCoin < _costUpdate ? $"<color=red>{_currentCoin}</color>" : $"{_currentCoin}";
            _txtPriceUpdate.text = textCurrentCoin + $"/{_costUpdate}";
        }
    }

    public void ReloadItem()
    {
        foreach (var itemSlot in _listItemSlot)
        {
            Destroy(itemSlot);
        }
        _listItemSlot.Clear();

        // Reinstantiate items
        foreach (var item in app.models.dataPlayerModel.BagItem)
        {
            var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
            go.TryGetComponent(out ItemSlotCVUI item1);
            item1.Init(item, this);
            _listItemSlot.Add(go);
        }
    }

    public bool maxRank()
    {

        if ((int)_slotNone1.itemInBag.rank == 4 ||
        (int)_slotNone2.itemInBag.rank == 4 ||
        (int)_slotNone3.itemInBag.rank == 4 )
        {
            return false;
        }
        return true;
    }
}