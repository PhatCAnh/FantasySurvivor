using System;
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

	public void EquipItem(ItemInBag data, int value)
	{
		app.models.dataPlayerModel.EquipItem(data);
		var itemData = GetDataItem(data.id, data.rank, data.level);
		var model = app.models.characterModel;
		switch (itemData.dataConfig.type)
		{
			case ItemType.Weapon:
			case ItemType.Gloves:
			case ItemType.Ring:
				model.attackDamage += value; //cong them chi so cua level nua
				break;
			case ItemType.Armor:
			case ItemType.Shoes:
			case ItemType.Hat:
				model.maxHealthPoint += value;
				break;
		}
	}
	public void UnEquipItem(ItemInBag data, int value)
	{
		app.models.dataPlayerModel.UnEquipItem(data);
		var itemData = GetDataItem(data.id, data.rank, data.level);
		var model = app.models.characterModel;
		switch (itemData.dataConfig.type)
		{
			case ItemType.Weapon:
			case ItemType.Gloves:
				model.attackDamage -= value; //cong them chi so cua level nua
				break;
			case ItemType.Armor:
			case ItemType.Shoes:
				model.maxHealthPoint -= value;
				break;
		}
	}

	public void ClaimItem(ItemId id, ItemType type, ItemRank rank, int level, int quantity)
	{
		app.resourceManager.ShowPopup(PopupType.RewardPopup).TryGetComponent(out PopupReward popupReward);

		var listItem = new List<ItemInBag>
		{
			new ItemInBag(id.ToString(), rank.ToString(), 0, quantity),
		};
		
		popupReward.Init(listItem);

		LogicClaimItem(type, id, rank, level, quantity);
	}

	private void LogicClaimItem(ItemType type, ItemId id, ItemRank rank, int level, int quantity)
	{
		switch (type)
		{

			case ItemType.Piece:
				app.models.dataPlayerModel.AddItemPieceToBag(id, quantity);
				break;
			case ItemType.ETC:
				switch (id)
				{
					case ItemId.Gold:
						app.models.dataPlayerModel.Gold += quantity;
						break;
					case ItemId.Gem:
						app.models.dataPlayerModel.Gem += quantity;
						break;
				}
				break;
			case ItemType.Weapon:
			case ItemType.Armor:
			case ItemType.Shoes:
			case ItemType.Gloves:
			case ItemType.Hat:
			case ItemType.Ring:
				app.models.dataPlayerModel.AddItemEquipToBag(id, rank, level);
				break;
			default:
				break;
		}
	}

	// [ContextMenu("Test item piece")]
	// public void Test()
	// {
	// 	app.resourceManager.ShowPopup(PopupType.RewardPopup).TryGetComponent(out PopupReward popupReward);
	// 	popupReward.Init();
	// }
}