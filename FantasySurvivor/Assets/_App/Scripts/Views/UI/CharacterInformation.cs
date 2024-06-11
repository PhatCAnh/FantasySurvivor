//using System;
//using ArbanFramework.MVC;
//using FantasySurvivor;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.Serialization;
//using UnityEngine.UI;

//public class CharacterInformation : View<GameApp>, IPopup
//{
//	[SerializeField] private TextMeshProUGUI _txtHealth, _txtMoveSpeed, _txtAtkDamage, _txtArmor;

//	[SerializeField] private Transform _slotItemEquipContainer;

//	[SerializeField] private GameObject _slotItemEquipPrefab;

//	[SerializeField] private ItemSlotEquipUI _slotWeapon, _slotArmor, _slotHat, _slotRing, _slotShoes, _slotGloves;

//	[SerializeField] private Button _btnBack;

//	private Dictionary<ItemType, ItemSlotEquipUI> _dicItemEquip;

//	private ItemController itemController => ArbanFramework.Singleton<ItemController>.instance;

//	private List<GameObject> _listItemSlot = new List<GameObject>();

//	protected override void Start()
//	{
//		base.Start();

//		//fix cho nay
//		Task.Delay(500);

//		InitDispatcher();
//	}

//	protected override void OnViewInit()
//	{
//		base.OnViewInit();

//		_slotWeapon.Init(null, this);
//		_slotArmor.Init(null, this);
//		_slotHat.Init(null, this);
//		_slotRing.Init(null, this);
//		_slotShoes.Init(null, this);
//		_slotGloves.Init(null, this);

//		_dicItemEquip = new Dictionary<ItemType, ItemSlotEquipUI>
//		{
//			{ItemType.Weapon, _slotWeapon},
//			{ItemType.Armor, _slotArmor},
//			{ItemType.Hat, _slotHat},
//			{ItemType.Ring, _slotRing},
//			{ItemType.Shoes, _slotShoes},
//			{ItemType.Gloves, _slotGloves},
//		};

//		_btnBack.onClick.AddListener(Close);

//		//app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe, ItemRank.Legendary, 2);

//		foreach (var item in app.models.dataPlayerModel.BagItem)
//		{
//			var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
//			go.TryGetComponent(out ItemSlotUI item1);
//			item1.Init(item, this);
//			_listItemSlot.Add(go);
//		}

//		foreach (var item in app.models.dataPlayerModel.ListItemEquipped)
//		{
//			var data = itemController.GetDataItem(item.id, item.rank, item.level);
//			var slot = _dicItemEquip[data.dataConfig.type];
//			if (slot.isEquip) UnEquipItem(data.dataConfig.type, item);
//			slot.Init(item);
//		}
//	}

//	[ContextMenu("Test item piece")]
//	public void Test()
//	{
//		app.models.dataPlayerModel.AddItemEquipToBag(ItemId.PieceFire, 2);
//	}

//	[ContextMenu("Test item Equip")]
//	public void TestEquip()
//	{
//		app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe, ItemRank.Legendary, 5);
//	}

//	public void EquipItem(ItemType type, ItemInBag data)
//	{
//		var slot = _dicItemEquip[type];
//		if (slot.isEquip) UnEquipItem(type, data);
//		slot.Init(data);
//		itemController.EquipItem(data);
//	}

//	public void UnEquipItem(ItemType type, ItemInBag data)
//	{
//		_dicItemEquip[type].ResetData();
//		itemController.UnEquipItem(data);
//		Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
//		item.Init(data, this);
//	}

//	private void InitDispatcher()
//	{
//		AddDataBinding("fieldCharacterModel-healthValue", _txtHealth, (control, e) =>
//			{
//				control.text = $"{app.models.characterModel.maxHealthPoint}";
//			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.maxHealthPoint), app.models.characterModel)
//		);

//		AddDataBinding("fieldCharacterModel-moveSpeedValue", _txtMoveSpeed, (control, e) =>
//			{
//				control.text = $"{app.models.characterModel.moveSpeed}";
//			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.moveSpeed), app.models.characterModel)
//		);

//		AddDataBinding("fieldCharacterModel-atkDamageValue", _txtAtkDamage, (control, e) =>
//			{
//				control.text = $"{app.models.characterModel.attackDamage}";
//			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackDamage), app.models.characterModel)
//		);

//		AddDataBinding("fieldCharacterModel-armorValue", _txtArmor, (control, e) =>
//		{
//			float armorPercent = app.models.characterModel.armor / 100f;
//			control.text = $"{armorPercent * 100f}%";
//		}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.armor), app.models.characterModel));

//		AddDataBinding("fieldDataPlayerModel-BagItemEquipValue", this, (control, e) =>
//			{
//				foreach (var itemSlot in _listItemSlot.ToList())
//				{
//					Destroy(itemSlot);
//				}
//				_listItemSlot.Clear();

//				foreach (var itemSlot in app.models.dataPlayerModel.BagItem)
//				{
//					var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
//					go.TryGetComponent(out ItemSlotUI item1);
//					item1.Init(itemSlot, this);
//					_listItemSlot.Add(go);
//				}

//				//             var dataStat = app.models.dataPlayerModel.GetFirstItemEquipAdded();
//				// if (dataStat == null) return;
//				//             Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
//				//             item.Init(dataStat, this);
//			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.BagItem), app.models.dataPlayerModel)
//		);
//	}
//	public void Open()
//	{

//	}
//	public void Close()
//	{
//		Destroy(gameObject);
//	}
//}
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

        // Sort the bag items and equipped items based on the desired order
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

    [ContextMenu("Test item piece")]
    public void Test()
    {
        app.models.dataPlayerModel.AddItemEquipToBag(ItemId.PieceFire, 2);
    }

    [ContextMenu("Test item Equip")]
    public void TestEquip()
    {
        app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe, ItemRank.Legendary, 5);
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
