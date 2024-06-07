using System;
using System.Collections;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemPieceData", menuName = "ScriptableObjects/ItemPieceData", order = 3)]
public class ItemPieceDataTable : ScriptableObject
{
	public List<ItemPieceDataUI> listItemPieceData;
}

[Serializable]
public class ItemPieceDataUI
{
	public ItemPieceId id;
	public string itemName;
	public ItemPieceType type;
	public Sprite skin;
	public ItemRank rank;
}

public class ItemPieceData
{
	public ItemPieceId id;
	public readonly ItemPieceDataUI dataUi;
	public readonly Sprite spriteRank;

	public ItemPieceData(ItemPieceDataUI dataUI, Sprite spriteRank)
	{
		this.id = dataUI.id;
		this.dataUi = dataUI;
		this.spriteRank = spriteRank;
	}
}
public class ItemPieceInBag
{
	public ItemPieceId id;
	public int quantity;
	public ItemPieceInBag(ItemPieceId id, int quantity)
	{
		this.id = id;
		this.quantity = quantity;
	}
}