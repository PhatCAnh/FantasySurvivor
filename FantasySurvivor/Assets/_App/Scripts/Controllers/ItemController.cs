﻿using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
public class ItemController : Controller<GameApp>
{
	[SerializeField] private ItemDataTable _itemDataUITable;

	[SerializeField] private Sprite _spriteNormal, _spriteRare, _spriteEpic, _spriteUnique, _spriteLegendary;

	private Dictionary<ItemRank, Sprite> _dicRankItemEquip;

	private List<ItemInBag> listItemEquipped; //fix it, chuyen cai nay vao đataplayermodel

	private void Awake()
	{
		Singleton<ItemController>.Set(this);
		_dicRankItemEquip = new Dictionary<ItemRank, Sprite>
		{
			{ItemRank.Normal, _spriteNormal},
			{ItemRank.Rare, _spriteRare},
			{ItemRank.Epic, _spriteEpic},
			{ItemRank.Unique, _spriteUnique},
			{ItemRank.Legendary, _spriteLegendary},
		};

		listItemEquipped = new List<ItemInBag>();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<ItemController>.Unset(this);
	}

	public Sprite GetSpriteRank(ItemRank rank)
	{
		return _dicRankItemEquip[rank];
	}

	public ItemData GetDataItem(ItemId id, ItemRank rank, int level)
	{
		var dataUI = _itemDataUITable.listItemEquipData.FirstOrDefault(item => item.id == id);
		var data = app.configs.dataItem.GetConfig(id);
		return new ItemData(dataUI, data, rank);
	}

	public void EquipItem(ItemInBag data)
	{
		listItemEquipped.Add(data);
		var itemData = GetDataItem(data.id, data.rank, data.level);
		var model = app.models.characterModel;
		switch (itemData.dataConfig.type)
		{
			case ItemType.Weapon:
			case ItemType.Gloves:
				model.attackDamage += itemData.dataConfig.baseValue; //cong them chi so cua level nua
				break;
			case ItemType.Armor:
			case ItemType.Shoes:
				model.maxHealthPoint += itemData.dataConfig.baseValue;
				break;
		}
	}
	public void UnEquipItem(ItemInBag data)
	{
		if(!listItemEquipped.Contains(data)) return;
		var item = listItemEquipped.Remove(data);
		var itemData = GetDataItem(data.id, data.rank, data.level);
		var model = app.models.characterModel;
		switch (itemData.dataConfig.type)
		{
			case ItemType.Weapon:
			case ItemType.Gloves:
				model.attackDamage -= itemData.dataConfig.baseValue; //cong them chi so cua level nua
				break;
			case ItemType.Armor:
			case ItemType.Shoes:
				model.maxHealthPoint -= itemData.dataConfig.baseValue;
				break;
		}
	}
}