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

			_bagItemEquip = new List<ItemEquipInBag>();

			app.models.WriteModel<DataPlayerModel>();

			numberItemCreated = 0;
		}

		[JsonProperty] private int _coin;
		[JsonProperty] private int _gem;

		[JsonProperty] private List<ItemEquipInBag> _bagItemEquip = new List<ItemEquipInBag>();
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

		public List<ItemEquipInBag> BagItemEquip
		{
			get => _bagItemEquip;
		}

		public void AddItemEquipToBag(ItemEquipStat item)
		{
			ItemEquipInBag itemInBag = new ItemEquipInBag(item);
			numberItemCreated++;
			_bagItemEquip.Add(itemInBag);
			app.models.WriteModel<DataPlayerModel>();
			RaiseDataChanged(nameof(BagItemEquip));
		}

		public void EquipItemInToBag(ItemEquipStat data, bool value)
		{
			foreach(var item in BagItemEquip)
			{
				if(item.itemEquipStat.idOwner == data.idOwner)
				{
					item.isEquip = value;
					break;
				}
			}
			app.models.WriteModel<DataPlayerModel>();
		}

		public ItemEquipInBag GetFirstItemEquipAdded()
		{
			return _bagItemEquip.LastOrDefault();
		}
	}
}