using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class StatUI : View<GameApp>
{
	[SerializeField] private TextMeshProUGUI _txtHealth, _txtMoveSpeed, _txtAtkDamage, _txtArmor;

	[SerializeField] private Transform _slotItemEquipContainer;

	[SerializeField] private GameObject _slotItemEquipPrefab;

	[SerializeField] private ItemSlotEquipUI _slotWeapon;

	private Dictionary<ItemEquipType, ItemSlotEquipUI> _dicItemEquip;

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

		_dicItemEquip = new Dictionary<ItemEquipType, ItemSlotEquipUI>
		{
			{ItemEquipType.Weapon, _slotWeapon},
		};
		
		//app.models.dataPlayerModel.AddItemEquipToBag(itemController.GetDataItemEquip(ItemEquipId.Item1).dataStat);

		foreach(var item in app.models.dataPlayerModel.BagItemEquip)
		{
			Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item1);
			var id =itemController.GetDataItemEquip((ItemEquipId) Enum.Parse(typeof(ItemEquipId), item.id));
			var data = new ItemEquipData(id.dataUi, item, id.spriteRank);
			item1.Init(data, this);
		}
		
		
	}

	public void EquipItem(ItemEquipType type, ItemEquipData data)
	{
		var slot = _dicItemEquip[type];
		if(slot.isEquip) UnEquipItem(type, slot.data);
		slot.Init(data);
		itemController.EquipItem(data);

	}

	public void UnEquipItem(ItemEquipType type, ItemEquipData data)
	{
		_dicItemEquip[type].ResetData();
		itemController.UnEquipItem(data);
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
			float armorPercent = (float) app.models.characterModel.armor / 100f;
			control.text = $"{armorPercent * 100f}%";
		}, new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.armor), app.models.characterModel));
		
		AddDataBinding("fieldDataPlayerModel-BagItemEquipValue", this, (control, e) =>
			{
                var dataStat = app.models.dataPlayerModel.GetFirstItemEquipAdded();
				if (dataStat == null) return;
                Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
				var id =itemController.GetDataItemEquip((ItemEquipId) Enum.Parse(typeof(ItemEquipId), dataStat.id));
				var data = new ItemEquipData(id.dataUi, dataStat, id.spriteRank);
				item.Init(data, this);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.BagItemEquip), app.models.dataPlayerModel)
		);
	}
}