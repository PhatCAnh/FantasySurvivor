using System;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.Serialization;
public class ItemController : Controller<GameApp>
{
	[SerializeField] private ItemEquipDataTable _equipDataTable;
	
	[SerializeField] private ItemPieceDataTable _pieceDataTable;

	[SerializeField] private Sprite _spriteNormal, _spriteRare, _spriteEpic, _spriteUnique, _spriteLegendary;
	
	private Dictionary<ItemRank, Sprite> _dicRankItemEquip;

	private List<ItemEquipData> listItemEquipped;
	
	

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

		listItemEquipped = new List<ItemEquipData>();
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		Singleton<ItemController>.Unset(this);
	}

	public ItemEquipData GetDataItemEquip(ItemEquipId id)
	{
		// var dataUI = _equipDataTable.listItemEquipData.Find(item => item.id == id);
		// var dataStat = new ItemEquipStat(app.configs.dataItemEquip.GetConfig(id), app.models.dataPlayerModel.GetNumberItemEquipCreated());
		// return new ItemEquipData(dataUI, dataStat, _dicRankItemEquip[dataUI.rank]);
		return null;
	}
	
	public ItemPieceData GetDataItemPiece(ItemPieceId id)
	{
		var dataUI = _pieceDataTable.listItemPieceData.Find(item => item.id == id);
		return new ItemPieceData(dataUI, _dicRankItemEquip[dataUI.rank]);
	}

	public void EquipItem(ItemEquipData data)
	{
		listItemEquipped.Add(data);
		var stat = data.dataStat.dataStatConfig;
		var model = app.models.characterModel;
		model.maxHealthPoint += stat.hp;
		model.moveSpeed += stat.moveSpeed;
		model.attackDamage += stat.damage;
		model.itemAttractionRange += stat.attackRange;
		model.armor += stat.armor;
		model.attackRange += stat.attackRange;
	}

	public void UnEquipItem(ItemEquipData data)
	{
		if(!listItemEquipped.Contains(data)) return;
		listItemEquipped.Remove(data);
		var stat = data.dataStat.dataStatConfig;
		var model = app.models.characterModel;
		model.maxHealthPoint -= stat.hp;
		model.moveSpeed -= stat.moveSpeed;
		model.attackDamage -= stat.damage;
		model.itemAttractionRange -= stat.attackRange;
		model.armor -= stat.armor;
        model.attackRange -= stat.attackRange;
    }
}