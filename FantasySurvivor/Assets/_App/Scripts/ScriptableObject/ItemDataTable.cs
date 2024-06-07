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

public class ItemStat
{
	public string id;
	public DataItemEquipConfig dataStatConfig;

	public ItemStat(DataItemEquipConfig dataStatConfig)
	{
		this.id = dataStatConfig.id;
		this.dataStatConfig = dataStatConfig;
	}
}

public class ItemData
{
	public readonly ItemId id;
	public readonly ItemDataUI dataUi;
	public readonly DataItemConfig dataStat;

	public ItemData(ItemDataUI dataUI, DataItemConfig dataStat)
	{
		this.id = dataUI.id;
		this.dataUi = dataUI;
		this.dataStat = dataStat;
	}
}

public class ItemInBag
{
	public readonly ItemId id;
	public ItemRank rank;
	public int level;
	public int quantity;
	public ItemInBag(ItemId id, ItemRank rank, int level = 1)
	{
		this.id = id;
		this.rank = rank;
		this.level = level;
	}

	public ItemInBag(ItemId id, int quantity = 1)
	{
		this.id = id;
		this.quantity = quantity;
	}
}