using System;
using System.Collections;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemEquipData", order = 4)]
public class ItemDataTable : ScriptableObject
{
	public List<ItemDataUI> listItemEquipData;
}

[Serializable]
public class ItemDataUI
{
	public ItemId id;
	public Sprite skin;
}

public class ItemData
{
	public readonly ItemId id;
	public readonly ItemRank rank;
	public readonly ItemDataUI dataUi;
	public readonly DataItemConfig dataConfig;

	public ItemData(ItemDataUI dataUI, DataItemConfig dataConfig, ItemRank rank = ItemRank.Normal)
	{
		this.id = dataUI.id;
		this.dataUi = dataUI;
		this.dataConfig = dataConfig;
		this.rank = rank;
	}
}

[Serializable]
public class ItemInBag
{
	public ItemId id;
	public ItemRank rank;
	public int level;
	public int quantity;
	
	public ItemInBag(string id, string rank, int level = 1, int quantity = 0)
	{
		this.id = (ItemId)Enum.Parse(typeof(ItemId), id);
		this.rank = (ItemRank)Enum.Parse(typeof(ItemRank), rank);
		this.level = level;
		this.quantity = quantity;
	}

	// public ItemInBag(ItemId id, int quantity)
	// {
	// 	this.id = id;
	// 	this.quantity = quantity;
	// }
	
	// public ItemInBag(ItemId id, ItemRank rank, int level = 1, int quantity = 0)
	// {
	// 	this.id = id.ToString();
	// 	this.rank = rank.ToString();
	// 	this.level = level;
	// 	this.quantity = quantity;
	// }
	//
	// public ItemInBag(ItemId id, ItemRank rank, int level = 1)
	// {
	// 	this.id = id;
	// 	this.rank = rank;
	// 	this.level = level;
	// }
	//
	// public ItemInBag(ItemId id, int quantity = 1)
	// {
	// 	this.id = id;
	// 	this.quantity = quantity;
	// }
}