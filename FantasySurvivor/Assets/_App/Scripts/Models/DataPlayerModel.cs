using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ArbanFramework.MVC;
using Newtonsoft.Json;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
namespace FantasySurvivor
{
	public class DataPlayerModel : Model<GameApp>
	{
		public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(DataPlayerModel) + ".dataChanged");

		public DataPlayerModel(EventTypeBase eventType) : base(dataChangedEvent)
		{
		}

		public DataPlayerModel() : base(dataChangedEvent)
		{

		}

		public override void InitBaseData()
		{
			this.Coin = 99;
			this.Gem = 10;

			app.models.WriteModel<DataPlayerModel>();
			numberItemCreated = 0;
		}

		[JsonProperty] private int _coin;
		[JsonProperty] private int _gem;

		[JsonProperty] private List<ItemInBag> _bagItem = new List<ItemInBag>();

		[JsonProperty] private List<ItemInBag> _listItemEquipped = new List<ItemInBag>();
		
		[JsonProperty] private List<SkillId> _skillSet = new List<SkillId>();

		[JsonProperty] private int numberItemCreated;

		public int Coin
		{
			get => _coin;
			set {
				if(Coin == value) return;
				_coin = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(Coin));
			}
		}

		public int Gem
		{
			get => _gem;
			set {
				if(Gem == value) return;
				_gem = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(Gem));
			}
		}

		public string GetNumberItemEquipCreated()
		{
			return "Goty_" + numberItemCreated;
		}

		public List<ItemInBag> BagItem
		{
			get => _bagItem;
		}
		
		public List<ItemInBag> ListItemEquipped 
		{
			get => _listItemEquipped;
		}

		//fix it
		public void AddItemEquipToBag(ItemId id, ItemRank rank, int level)
		{
			ItemInBag itemInBag = new ItemInBag(id.ToString(), rank.ToString(), level);
			_bagItem.Add(itemInBag);
			app.models.WriteModel<DataPlayerModel>();
			RaiseDataChanged(nameof(BagItem));
		}
		
		public void AddItemEquipToBag(ItemId id, int quantity)
		{
			var check = _bagItem.Find(item => item.id == id);
			if(check == null)
			{
				_bagItem.Add(new ItemInBag(id.ToString(), "Rare", 0, quantity));
			}
			else
			{
				check.quantity += quantity;
			}
			RaiseDataChanged(nameof(BagItem));
			app.models.WriteModel<DataPlayerModel>();
		}


		public void EquipItem(ItemInBag item)
		{
			if(_listItemEquipped.Contains(item)) return;
			_bagItem.Remove(item);
			_listItemEquipped.Add(item);
			app.models.WriteModel<DataPlayerModel>();
		}

		public void UnEquipItem(ItemInBag item)
		{
			_bagItem.Add(item);
			_listItemEquipped.Remove(item);
			app.models.WriteModel<DataPlayerModel>();
		}

		public ItemInBag GetFirstItemEquipAdded()
		{
			return _bagItem.LastOrDefault();
		}

		public void AddSkillSet(SkillId id)
		{
			_skillSet.Add(id);
			app.models.WriteModel<DataPlayerModel>();
		}
		
		public void RemoveSkillSet(SkillId id)
		{
			_skillSet.Remove(id);
			app.models.WriteModel<DataPlayerModel>();
		}

		public void UpdateItem(ItemInBag item)
		{
			if(item.level >= app.configs.dataStatRankItemEquip.GetConfig(item.rank).levelLimit) return;
			item.level += 1;
			app.models.WriteModel<DataPlayerModel>();
		}

		public List<SkillId> GetSkillSet()
		{
			return _skillSet;
		}
	}
}