using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbanFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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

	private int _numberValue;

	protected override void Start()
	{
		base.Start();

		//fix cho nay
		Task.Delay(500);

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

		//app.models.dataPlayerModel.AddItemEquipToBag(ItemId.Axe, ItemRank.Legendary, 2);

		foreach(var item in app.models.dataPlayerModel.BagItem)
		{
			var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
			go.TryGetComponent(out ItemSlotUI item1);
			item1.Init(item, this);
			_listItemSlot.Add(go);
		}

		foreach(var item in app.models.dataPlayerModel.ListItemEquipped)
		{
			var dataItem = itemController.GetDataItem(item.id, item.rank, item.level);
			var nameStat = Singleton<GameController>.instance.GetTypeStatItemEquip(dataItem.dataConfig.type);
			var data = Singleton<GameController>.instance.GetDataStat(nameStat, item.rank);
			_numberValue = dataItem.dataConfig.baseValue + data.Item2 * (item.level - 1);
			itemController.EquipItem(item, _numberValue);
			
			// var data = itemController.GetDataItem(item.id, item.rank, item.level);
			// var slot = _dicItemEquip[data.dataConfig.type];
			// if(slot.isEquip) UnEquipItem(data.dataConfig.type, item);
			// slot.Init(item);
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

	public void EquipItem(ItemType type, ItemInBag data, int value)
	{
		var slot = _dicItemEquip[type];
		if(slot.isEquip) UnEquipItem(type, data, value);
		slot.Init(data);
		itemController.EquipItem(data, value);
	}

	public void UnEquipItem(ItemType type, ItemInBag data, int value)
	{
		_dicItemEquip[type].ResetData();
		itemController.UnEquipItem(data, value);
		Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
		item.Init(data, this);
	}

	private void InitDispatcher()
	{
		AddDataBinding("fieldCharacterModel-healthValue", _txtHealth, (control, e) =>
			{
				control.text = $"{app.models.characterModel.maxHealthPoint}";
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.maxHealthPoint), app.models.characterModel)
		);

		AddDataBinding("fieldCharacterModel-moveSpeedValue", _txtMoveSpeed, (control, e) =>
			{
				control.text = $"{app.models.characterModel.moveSpeed}";
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.moveSpeed), app.models.characterModel)
		);

		AddDataBinding("fieldCharacterModel-atkDamageValue", _txtAtkDamage, (control, e) =>
			{
				control.text = $"{app.models.characterModel.attackDamage}";
			}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.attackDamage), app.models.characterModel)
		);

		AddDataBinding("fieldCharacterModel-armorValue", _txtArmor, (control, e) =>
		{
			float armorPercent = app.models.characterModel.armor / 100f;
			control.text = $"{armorPercent * 100f}%";
		}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.armor), app.models.characterModel));
		
		AddDataBinding("fieldDataPlayerModel-BagItemEquipValue", this, (control, e) =>
			{
				foreach(var itemSlot in _listItemSlot.ToList())
				{
					Destroy(itemSlot);
				}
				_listItemSlot.Clear();
				
				foreach(var itemSlot in app.models.dataPlayerModel.BagItem)
				{
					var go = Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer);
					go.TryGetComponent(out ItemSlotUI item1);
					item1.Init(itemSlot, this);
					_listItemSlot.Add(go);
				}
				
    //             var dataStat = app.models.dataPlayerModel.GetFirstItemEquipAdded();
				// if (dataStat == null) return;
    //             Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
    //             item.Init(dataStat, this);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.BagItem), app.models.dataPlayerModel)
		);
	}
	public void Open()
	{
		
	}
	public void Close()
	{
		Destroy(gameObject);
	}
}