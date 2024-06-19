using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ArbanFramework.MVC;
using FantasySurvivor;

public class CharacterInformation : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtHealth, _txtMoveSpeed, _txtAtkDamage, _txtArmor;
    [SerializeField] private Transform _slotItemEquipContainer;
    [SerializeField] private GameObject _slotItemEquipPrefab;
    [SerializeField] private ItemSlotEquipUI _slotWeapon, _slotArmor, _slotHat, _slotRing, _slotShoes, _slotGloves;
    [SerializeField] private Button _btnBack;
    [SerializeField] private Button _btnOkPopup;
    [SerializeField] private Button _btnBackPopup;
    [SerializeField] private Button _btnChooseCharacter;
    [SerializeField] private GameObject _chooseCharacterPrefab;
    [SerializeField] private Button _btnAll;
    [SerializeField] private GameObject _focusAll;
    [SerializeField] private Button _btnWeapon;
    [SerializeField] private GameObject _focusWeapon;
    [SerializeField] private Button _btnArmor;
    [SerializeField] private GameObject _focusArmor;
    [SerializeField] private Button _btnShoes;
    [SerializeField] private GameObject _focusShoes;
    [SerializeField] private Button _btnRing;
    [SerializeField] private GameObject _focusRing;
    [SerializeField] private Button _btnHat;
    [SerializeField] private GameObject _focusHat;
    [SerializeField] private Button _btnGlove;
    [SerializeField] private GameObject _focusGlove;
    [SerializeField] private GameObject _popupLimitItem;
    private int MaxItem = 50;
    private Dictionary<ItemType, ItemSlotEquipUI> _dicItemEquip;
    private ItemController itemController => ArbanFramework.Singleton<ItemController>.instance;
    private List<GameObject> _listItemSlot = new List<GameObject>();

    protected override async void Start()
    {
        base.Start();
        await Task.Delay(500); // Properly await the delay
        InitDispatcher();
    }

    protected override void OnViewInit()
    {
        base.OnViewInit();

        _slotWeapon.Init(null, this);
        _slotArmor.Init(null, this);
        _slotHat.Init(null, this);
        _slotRing.Init(null, this);
        _slotShoes.Init(null, this);
        _slotGloves.Init(null, this);

        _dicItemEquip = new Dictionary<ItemType, ItemSlotEquipUI>
        {
            {ItemType.Weapon, _slotWeapon},
            {ItemType.Armor, _slotArmor},
            {ItemType.Hat, _slotHat},
            {ItemType.Ring, _slotRing},
            {ItemType.Shoes, _slotShoes},
            {ItemType.Gloves, _slotGloves},
        };

        _btnBack.onClick.AddListener(Close);
        _btnAll.onClick.AddListener(() =>
        {
            ShowAll();
            ToggleFocus(_focusAll);
        });
        _btnWeapon.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Weapon);
            ToggleFocus(_focusWeapon);
            _focusAll.SetActive(false);
        });
        _btnArmor.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Armor);
            ToggleFocus(_focusArmor);
            _focusAll.SetActive(false);
        });
        _btnShoes.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Shoes);
            ToggleFocus(_focusShoes);
            _focusAll.SetActive(false);
        });
        _btnRing.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Ring);
            ToggleFocus(_focusRing);
            _focusAll.SetActive(false);
        });
        _btnHat.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Hat);
            ToggleFocus(_focusHat);
            _focusAll.SetActive(false);
        });
        _btnGlove.onClick.AddListener(() =>
        {
            ShowItemsByType(ItemType.Gloves);
            ToggleFocus(_focusGlove);
            _focusAll.SetActive(false);
        });

        _btnChooseCharacter.onClick.AddListener(() =>
        {
            SwitchToChooseCharacter();
        });

        ShowAll();
    }

    private void ShowItemsByType(ItemType type)
    {
        foreach (var itemSlot in _listItemSlot)
        {
            Destroy(itemSlot);
        }
        _listItemSlot.Clear();

        var filteredItems = app.models.dataPlayerModel.BagItem
            .Where(item => itemController.GetDataItem(item.id, item.rank, item.level).dataConfig.type == type)
            .ToList();

        foreach (var item in filteredItems)
        {
            var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
            if (go.TryGetComponent(out ItemSlotUI item1))
            {
                item1.Init(item, this);
                _listItemSlot.Add(go);
            }
        }
    }

    private void ShowAll()
    {
        foreach (var itemSlot in _listItemSlot)
        {
            Destroy(itemSlot);
        }
        _listItemSlot.Clear();

        var sortedBagItems = app.models.dataPlayerModel.BagItem
            .OrderBy(item => GetItemTypeOrder(itemController.GetDataItem(item.id, item.rank, item.level).dataConfig.type))
            .ToList();

        foreach (var item in sortedBagItems)
        {
            var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
            if (go.TryGetComponent(out ItemSlotUI item1))
            {
                item1.Init(item, this);
                _listItemSlot.Add(go);
            }
        }

        var sortedEquippedItems = app.models.dataPlayerModel.ListItemEquipped
            .OrderBy(item => GetItemTypeOrder(itemController.GetDataItem(item.id, item.rank, item.level).dataConfig.type))
            .ToList();

        foreach (var item in sortedEquippedItems)
        {
            var data = itemController.GetDataItem(item.id, item.rank, item.level);
            var slot = _dicItemEquip[data.dataConfig.type];
            if (slot.isEquip) UnEquipItem(data.dataConfig.type, item);
            slot.Init(item);
        }
    }

    private void ShowPopup()
    {
        _popupLimitItem.SetActive(true);
        _btnBackPopup.onClick.AddListener(ClosePopup);
        _btnOkPopup.onClick.AddListener(ClosePopup);
    }

    private void ClosePopup()
    {
        _popupLimitItem.SetActive(false);
    }

    private void ToggleFocus(GameObject focusObject)
    {
        if (_focusAll == null || _focusWeapon == null || _focusArmor == null || _focusShoes == null || _focusRing == null || _focusHat == null || _focusGlove == null)
        {
            return;
        }

        _focusAll.SetActive(focusObject == _focusAll);
        _focusWeapon.SetActive(focusObject == _focusWeapon);
        _focusArmor.SetActive(focusObject == _focusArmor);
        _focusShoes.SetActive(focusObject == _focusShoes);
        _focusRing.SetActive(focusObject == _focusRing);
        _focusHat.SetActive(focusObject == _focusHat);
        _focusGlove.SetActive(focusObject == _focusGlove);
    }

    private void SwitchToChooseCharacter()
    {
        // Hide current UI elements
        gameObject.SetActive(false);

        // Instantiate and show the choose character prefab
        var chooseCharacterInstance = Instantiate(_chooseCharacterPrefab, transform.parent);
        chooseCharacterInstance.SetActive(true);
    }

    private int GetItemTypeOrder(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Weapon: return 0;
            case ItemType.Armor: return 1;
            case ItemType.Shoes: return 2;
            case ItemType.Gloves: return 3;
            case ItemType.Hat: return 4;
            case ItemType.Ring: return 5;
            case ItemType.Piece: return 6;
            default: return int.MaxValue;
        }
    }

    public void EquipItem(ItemType type, ItemInBag data)
    {
        var slot = _dicItemEquip[type];
        if (slot.isEquip) UnEquipItem(type, data);
        slot.Init(data);
        itemController.EquipItem(data);
    }

    public void UnEquipItem(ItemType type, ItemInBag data)
    {
        _dicItemEquip[type].ResetData();
        itemController.UnEquipItem(data);
        var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
        if (go.TryGetComponent(out ItemSlotUI item))
        {
            item.Init(data, this);
            _listItemSlot.Add(go);
        }
    }

    [ContextMenu("Test item piece")]
    public void Test()
    {
        var sortedBagItems = app.models.dataPlayerModel.BagItem;
        if (sortedBagItems.Count > MaxItem)
        {
            ShowPopup();
            return;
        }
        else
        {
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.PieceFire, 2);
        }
    }

    [ContextMenu("Test item Equip")]
    public void TestEquip()
    {
        var sortedBagItems = app.models.dataPlayerModel.BagItem;
        if (sortedBagItems.Count > MaxItem)
        {
            ShowPopup();
            return;
        }
        else
        {
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe, ItemRank.Normal, 1);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Armor, ItemRank.Rare, 2);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Shoes, ItemRank.Epic, 3);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Gloves, ItemRank.Unique, 4);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Hat, ItemRank.Legendary, 5);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Ring, ItemRank.Epic, 6);

            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe1, ItemRank.Normal, 1);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Armor1, ItemRank.Rare, 2);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Shoes1, ItemRank.Epic, 3);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Gloves1, ItemRank.Unique, 4);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Hat1, ItemRank.Legendary, 5);
            app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Ring1, ItemRank.Epic, 6);
        }
    }

    private void InitDispatcher()
    {
        AddDataBinding("fieldCharacterModel-healthValue", _txtHealth, (control, e) =>
        {
            control.text = $"{app.models.characterModel.maxHealthPoint}";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.maxHealthPoint), app.models.characterModel));

        AddDataBinding("fieldCharacterModel-moveSpeedValue", _txtMoveSpeed, (control, e) =>
        {
            control.text = $"{app.models.characterModel.moveSpeed}";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.moveSpeed), app.models.characterModel));

        AddDataBinding("fieldCharacterModel-atkDamageValue", _txtAtkDamage, (control, e) =>
        {
            control.text = $"{app.models.characterModel.attackDamage}";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackDamage), app.models.characterModel));

        AddDataBinding("fieldCharacterModel-armorValue", _txtArmor, (control, e) =>
        {
            float armorPercent = app.models.characterModel.armor / 100f;
            control.text = $"{armorPercent * 100f}%";
        }, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.armor), app.models.characterModel));

        AddDataBinding("fieldDataPlayerModel-BagItemEquipValue", this, (control, e) =>
        {
            foreach (var itemSlot in _listItemSlot.ToList())
            {
                Destroy(itemSlot);
            }
            _listItemSlot.Clear();

            var sortedBagItems = app.models.dataPlayerModel.BagItem
                .OrderBy(item => GetItemTypeOrder(itemController.GetDataItem(item.id, item.rank, item.level).dataConfig.type))
                .ToList();
            foreach (var itemSlot in sortedBagItems)
            {
                var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
                if (go.TryGetComponent(out ItemSlotUI item1))
                {
                    item1.Init(itemSlot, this);
                    _listItemSlot.Add(go);
                }
            }
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.BagItem), app.models.dataPlayerModel));
    }

    public void Open()
    {
        // Implement the Open functionality if needed
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
