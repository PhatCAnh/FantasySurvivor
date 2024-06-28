using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DailyGiftPopup : View<GameApp>, IPopup
{
	private class DailyGiftItemDetail
	{
		public ItemId id;
		public int quantity;
		public ItemType type;

		public DailyGiftItemDetail(ItemId id, ItemType type, int quantity)
		{
			this.id = id;
			this.type = type;
			this.quantity = quantity;
		}
	}


	[SerializeField] private DailyGiftItem[] _arrDailyGiftItems;

	[SerializeField] private Button _btnClose;

	[SerializeField] private Transform _goMainContent;

	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		Init();
		Open();
	}

	private void Init()
	{
		// _arrDailyGiftItems[0].Init(ItemId.Gold, 1000, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[1].Init(ItemId.Gem, 100, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[2].Init(ItemId.SilverKey, 10, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[3].Init(ItemId.Gold, 2000, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[4].Init(ItemId.Gem, 200, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[5].Init(ItemId.SilverKey, 10, DailyGiftStatus.Waiting);
		// _arrDailyGiftItems[6].Init(ItemId.GoldKey, 10, DailyGiftStatus.Waiting);

		_btnClose.onClick.AddListener(Close);

		var dataSave = app.models.dataPlayerModel.DataSaveClaimDailyGift.Split("-");

		var distanceDay = gameController.CalculateTimeDailyGift();

		var arrDailyGiftItemDetail = new[]
		{
			new DailyGiftItemDetail(ItemId.Gold, ItemType.ETC, 2000),
			new DailyGiftItemDetail(ItemId.Gem, ItemType.ETC, 100),
			new DailyGiftItemDetail(ItemId.SilverKey, ItemType.Piece, 10),
			new DailyGiftItemDetail(ItemId.Gold, ItemType.ETC, 4000),
			new DailyGiftItemDetail(ItemId.Gem, ItemType.ETC, 200),
			new DailyGiftItemDetail(ItemId.SilverKey, ItemType.Piece, 10),
			new DailyGiftItemDetail(ItemId.GoldKey, ItemType.Piece, 10),
		};

		for(int i = 0; i < dataSave.Length; i++)
		{
			var type = (DailyGiftStatus) Enum.Parse(typeof(DailyGiftStatus), dataSave[i]);
			if(i < distanceDay)
			{
				_arrDailyGiftItems[i].Init(arrDailyGiftItemDetail[i].id, arrDailyGiftItemDetail[i].type, arrDailyGiftItemDetail[i].quantity, type == DailyGiftStatus.Claimed ? DailyGiftStatus.Claimed : DailyGiftStatus.Missing, this);
			}
			else if(i == distanceDay)
			{
				_arrDailyGiftItems[i].Init(arrDailyGiftItemDetail[i].id, arrDailyGiftItemDetail[i].type, arrDailyGiftItemDetail[i].quantity, type == DailyGiftStatus.Claimed ? DailyGiftStatus.Claimed : DailyGiftStatus.Ready, this);
			}
			else if(i == distanceDay + 1)
			{
				_arrDailyGiftItems[i].Init(arrDailyGiftItemDetail[i].id, arrDailyGiftItemDetail[i].type, arrDailyGiftItemDetail[i].quantity, DailyGiftStatus.Waiting, this);
			}
			else
			{
				_arrDailyGiftItems[i].Init(arrDailyGiftItemDetail[i].id, arrDailyGiftItemDetail[i].type, arrDailyGiftItemDetail[i].quantity, DailyGiftStatus.NotReady, this);
			}
		}

	}

	public void Open()
	{
		_goMainContent.localScale = Vector3.zero;

		_goMainContent.DOScale(Vector3.one, 0.15f);
	}
	public void Close()
	{
		_goMainContent.DOScale(Vector3.zero, 0.15f)
			.OnComplete(() =>
			{
				Destroy(gameObject);
			});
	}


	public void OnClickClaim()
	{
		string saveData = "";
		for(int i = 0; i < _arrDailyGiftItems.Length; i++)
		{
			saveData += _arrDailyGiftItems[i].status;
			if(i != _arrDailyGiftItems.Length - 1)
			{
				saveData += "-";
			}
		}
		app.models.dataPlayerModel.DataSaveClaimDailyGift = saveData;
	}
}