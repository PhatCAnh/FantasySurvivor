using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ArbanFramework;
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
			this.Gold = 99;
			this.Gem = 10;
			this.LimitQuantityItemEquip = 50;
			var dateNow = Singleton<GameController>.instance.GetDateTimeNow();
			DateLoginGame = $"{dateNow.Item1}/{dateNow.Item2}/{dateNow.Item3}";
			DateStartDailyGift = $"{dateNow.Item1}/{dateNow.Item2}/{dateNow.Item3}";
			DataSaveClaimDailyGift = "Waiting-Waiting-Waiting-Waiting-Waiting-Waiting-Waiting";

			_id = "";
			
			_email = "";
			
			_password = "";

			_nameDisplay = "Guest";
			
			app.models.WriteModel<DataPlayerModel>();
		}
		
		[JsonProperty] private string _email;
		
		[JsonProperty] private string _password;
		
		[JsonProperty] private string _nameDisplay;
		
		[JsonProperty] private string _id;

		[JsonProperty] private int _gold;
		
		[JsonProperty] private int _gem;
		
		[JsonProperty] private int _limitQuantityItemEquip;

		[JsonProperty] private List<ItemInBag> _bagItem = new List<ItemInBag>();

		[JsonProperty] private List<ItemInBag> _listItemEquipped = new List<ItemInBag>();
		
		[JsonProperty] private List<SkillId> _skillSet = new List<SkillId>();

		[JsonProperty] private string _dateLoginGame;
		
		[JsonProperty] private string _dateStartDailyGift;
		
		[JsonProperty] private string _dataSaveClaimDailyGift;
		
		private ItemInBag item;
		
		public string Id
		{
			get => _id;
			set {
				_id = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}
		
		public string Email
		{
			get => _email;
			set {
				_email = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}
		
		public string Password
		{
			get => _password;
			set {
				_password = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}
		
		public string NameDisplay
		{
			get => _nameDisplay;
			set {
				_nameDisplay = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(NameDisplay));
			}
		}
		
		public string DataSaveClaimDailyGift
		{
			get => _dataSaveClaimDailyGift;
			set {
				_dataSaveClaimDailyGift = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}
		
		public string DateStartDailyGift
		{
			get => _dateStartDailyGift;
			set {
				_dateStartDailyGift = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}
		
		public string DateLoginGame
		{
			get => _dateLoginGame;
			set {
				_dateLoginGame = value;
				app.models.WriteModel<DataPlayerModel>();
			}
		}

		public int Gold
		{
			get => _gold;
			set {
				if(Gold == value) return;
				_gold = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(Gold));
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
		
		public int LimitQuantityItemEquip
		{
			get => _limitQuantityItemEquip;
			set {
				if(LimitQuantityItemEquip == value) return;
				_limitQuantityItemEquip = value;
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(LimitQuantityItemEquip));
			}
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
		public bool AddItemEquipToBag(ItemId id, ItemRank rank, int level)
		{
			if(_bagItem.Count >= LimitQuantityItemEquip)
			{
				if(!app.resourceManager.CheckExistPopup(PopupType.Warning))
				{
					app.resourceManager.ShowPopup(PopupType.Warning).TryGetComponent(out PopupWarning warning);
					warning.Init(
						"Your bag is full, no more items can be added. Please increase your bag limit"
					);
				}
				return false;
			}
			
			ItemInBag itemInBag = new ItemInBag(id.ToString(), rank.ToString(), level);
			_bagItem.Add(itemInBag);
			app.models.WriteModel<DataPlayerModel>();
			RaiseDataChanged(nameof(BagItem));
			return true;
		}
		
		public void AddItemPieceToBag(ItemId id, int quantity)
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
        public void AddCoins(int amount)
		{
			Gold += amount;
			app.models.WriteModel<DataPlayerModel>();

        }
        public void RemoveItem(ItemInBag item)
        {
            _bagItem.Remove(item);
            _bagItem = _bagItem.OrderBy(i => i.id).ToList();
            app.models.WriteModel<DataPlayerModel>();
            RaiseDataChanged(nameof(BagItem));
		
        }
		public void RemovePiece(ItemId id, int quantityToRemove)
		{
			var itemInBag = _bagItem.Find(item => item.id == id);
			if (itemInBag != null)
			{
				itemInBag.quantity -= quantityToRemove;
				if (itemInBag.quantity <= 0)
				{
					_bagItem.Remove(itemInBag);
				}
				_bagItem = _bagItem.OrderBy(i => i.id).ToList();
				app.models.WriteModel<DataPlayerModel>();
				RaiseDataChanged(nameof(BagItem));
			}
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

		public void Login(string email, string password)
		{
			Email = email;
			Password = password;
			NameDisplay = email;
			app.models.WriteModel<DataPlayerModel>();
		}
		
		public void Logout()
		{
			Email = "";
			Password = "";
			NameDisplay = "Guest";
			app.models.WriteModel<DataPlayerModel>();
		}
	}
}