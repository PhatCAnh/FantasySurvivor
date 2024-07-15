using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : View<GameApp>, IPopup
{
	[Serializable]
	private class ItemToggle
	{
		public Toggle toggle;
		public Image image;
		public bool isLock;
		
	}

	[SerializeField] private ItemToggle _itemHome, _itemElemental, _itemShop, _itemUpdate, _itemLock;
	[SerializeField] private GameObject _goLock, _goUpdateStat, _goHome,_goListSkill;
	[SerializeField] private Image _imgLineFocus;
	[SerializeField] private Button _btnBattle, _btnCheat, _btnTest, _btnDailyGift;
	[SerializeField] private TextMeshProUGUI _txtGoldCoin;
	private float _durationAnim = 0.3f;

	private GameController gameController => Singleton<GameController>.instance;
	private PopupChoiceSkill popupChoiceSkill => Singleton<PopupChoiceSkill>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		_itemHome.toggle.onValueChanged.AddListener(OnClickTglHome);
		_itemElemental.toggle.onValueChanged.AddListener(OnClickTglElemental);
		_itemShop.toggle.onValueChanged.AddListener(OnClickTglShop);
		_itemUpdate.toggle.onValueChanged.AddListener(OnClickTglUpdate);
		_itemLock.toggle.onValueChanged.AddListener(OnClickTglLock);
		_btnBattle.onClick.AddListener(OnClickBtnBattle);
		_btnTest.onClick.AddListener(Test);
		
		//_btnDailyGift.onClick.AddListener(() =>
		//{
		//	app.resourceManager.ShowPopup(PopupType.DailyGift);
		//});

		_btnCheat.onClick.AddListener(() =>
		{
			app.resourceManager.ShowPopup(PopupType.Cheat);
		});

		AddEventChangeStat();

	}

	public void Open()
	{
	}
	public void Close()
	{
		Destroy(gameObject);
	}

	public void Test()
	{
		app.resourceManager.ShowPopup(PopupType.CharacterInformation);
	}

	private void MoveLineFocus(Vector3 pos)
	{
		var trans = _imgLineFocus.transform;
		trans.DOLocalMove(new Vector3(pos.x, trans.localPosition.y), _durationAnim);
	}

	private void ChangeAnimToggle(ItemToggle itemToggle)
	{
		if(itemToggle.toggle.isOn)
		{
			MoveLineFocus(itemToggle.toggle.transform.localPosition);
			itemToggle.image.transform.DOScale(Vector3.one * 1.25f, _durationAnim);
			itemToggle.image.transform.DOLocalMove(new Vector3(0, 30), _durationAnim);
		}
		else
		{
			itemToggle.image.transform.DOScale(Vector3.one, _durationAnim);
			itemToggle.image.transform.DOLocalMove(Vector3.zero, _durationAnim);
		}
	}

	private void OnClickTglHome(bool value)
	{
		ChangeAnimToggle(_itemHome);
		_goHome.SetActive(value);
		
	}

	public void OnClickTglElemental(bool value)
	{
       // app.resourceManager.ShowPopup(PopupType.Choicelistskill);
        ChangeAnimToggle(_itemElemental);
		_goListSkill.SetActive(_itemElemental.isLock && value);
		//_itemElemental.isLock=value;
		//_goLock.SetActive(_itemElemental.isLock && value);
		//app.resourceManager.ShowPopup(PopupType.Choicelistskill);
	}

	private void OnClickTglShop(bool value)
	{
		ChangeAnimToggle(_itemShop);
		_goLock.SetActive(_itemShop.isLock && value);
	}

	private void OnClickTglUpdate(bool value)
	{
		ChangeAnimToggle(_itemUpdate);
		_goUpdateStat.SetActive(value);
	}

	private void OnClickTglLock(bool value)
	{
		ChangeAnimToggle(_itemLock);
		_goLock.SetActive(_itemLock.isLock && value);
	}

	private void OnClickBtnBattle()
	{
		app.resourceManager.ShowPopup(PopupType.ChoiceSkillOutGame);

	}


	private void AddEventChangeStat()
	{
		AddDataBinding("fieldDataPlayerModel-goldValue", _txtGoldCoin, (control, e) =>
			{
				control.text = $"{app.models.dataPlayerModel.Gold}";
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Gold), app.models.dataPlayerModel)
		);
	}
}