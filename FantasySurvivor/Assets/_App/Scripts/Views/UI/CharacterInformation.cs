using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
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
		
		//app.models.dataPlayerModel.AddItemEquipToBag(itemController.GetDataItemEquip(ItemEquipId.Item1).dataConfig);

		foreach(var item in app.models.dataPlayerModel.BagItem)
		{
			
			//fix it
			/*if(item.isEquip)
			{
				var id = itemController.GetDataItemEquip((ItemEquipId) Enum.Parse(typeof(ItemEquipId), item.itemEquipStat.id));
				var data = new ItemEquipData(id.dataUi,  item.itemEquipStat, id.spriteRank);
				EquipItem(data.dataUi.type, id);
			}
			else
			{*/
				Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item1);
				//var data = itemController.GetDataItem(item.id, ItemRank.Epic, 5);
				item1.Init(item, this);
			//}
		}
		//item2.Init(data1, this);
	}

	public void EquipItem(ItemType type, ItemInBag data)
	{
		var slot = _dicItemEquip[type];
		if(slot.isEquip) UnEquipItem(type, data);
		var itemData = itemController.GetDataItem(data.id, data.rank, data.level);
		slot.Init(data);
		itemController.EquipItem(data);
		app.models.dataPlayerModel.EquipItemInToBag(itemData, true);
	}

	public void UnEquipItem(ItemType type, ItemInBag data)
	{
		_dicItemEquip[type].ResetData();
		itemController.UnEquipItem(data);
		var itemData = itemController.GetDataItem(data.id, data.rank, data.level);
		Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
		item.Init(data, this);
		app.models.dataPlayerModel.EquipItemInToBag(itemData, false);
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
			float armorPercent = (float) app.models.characterModel.armor / 100f;
			control.text = $"{armorPercent * 100f}%";
		}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.armor), app.models.characterModel));
		
		AddDataBinding("fieldDataPlayerModel-BagItemEquipValue", this, (control, e) =>
			{
                var dataStat = app.models.dataPlayerModel.GetFirstItemEquipAdded();
				if (dataStat == null) return;
                Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
                item.Init(dataStat, this);
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