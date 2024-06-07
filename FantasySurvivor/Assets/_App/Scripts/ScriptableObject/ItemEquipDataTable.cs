using System;
using System.Collections;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemEquipData", menuName = "ScriptableObjects/ItemEquipData", order = 2)]
public class ItemEquipDataTable : ScriptableObject
{
	public List<ItemEquipDataUI> listItemEquipData;
}

[Serializable]
public class ItemEquipDataUI
{
	public ItemEquipId id;
	public string itemName;
	public ItemEquipType type;
	public Sprite skin;
	public ItemRank rank;
}

public class ItemEquipData
{
	public readonly ItemEquipDataUI dataUi;
	public readonly ItemEquipStat dataStat;
	public readonly Sprite spriteRank;

	public ItemEquipData(ItemEquipDataUI dataUI, ItemEquipStat dataStat, Sprite spriteRank)
	{
		this.dataUi = dataUI;
		this.dataStat = dataStat;
		this.spriteRank = spriteRank;
	}
}

public class ItemEquipStat
{
	public string id;
	public string idOwner;
	public DataItemEquipConfig dataStatConfig;

	public ItemEquipStat(DataItemEquipConfig dataStatConfig, string idOwner)
	{
		this.id = dataStatConfig.id;
		this.idOwner = idOwner;
		this.dataStatConfig = dataStatConfig;
	}
}

public class ItemEquipInBag
{
	public ItemEquipStat itemEquipStat;
	public bool isEquip;
	public ItemEquipInBag(ItemEquipStat itemEquipStat)
	{
		this.itemEquipStat = itemEquipStat;
	}
}

public class ItemETC
{

}