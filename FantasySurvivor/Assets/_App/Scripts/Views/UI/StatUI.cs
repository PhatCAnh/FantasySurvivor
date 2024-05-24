using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class StatUI : View<GameApp>
{
	[SerializeField] private TextMeshProUGUI _txtHealth, _txtMoveSpeed, _txtAtkDamage, _txtArmor;

	[SerializeField] private Transform _slotItemEquipContainer;

	[SerializeField] private GameObject _slotItemEquipPrefab;

	[SerializeField] private ItemSlotEquipUI _slotWeapon;

	private Dictionary<ItemEquipType, ItemSlotEquipUI> _dicItemEquip;

	private ItemController itemController => Singleton<ItemController>.instance;
	protected override void OnViewInit()
	{
		base.OnViewInit();

		InitDispatcher();

		Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item);
		item.Init(itemController.GetDataItemEquip(ItemEquipId.Item1), this);

		Instantiate(_slotItemEquipPrefab, _slotItemEquipContainer).TryGetComponent(out ItemSlotUI item2);
		item2.Init(itemController.GetDataItemEquip(ItemEquipId.Item2), this);

		_slotWeapon.Init(null, this);

		_dicItemEquip = new Dictionary<ItemEquipType, ItemSlotEquipUI>
		{
			{ItemEquipType.Weapon, _slotWeapon},
		};
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
	}
}