using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DataConfig;
using DG.Tweening;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Toggle = UnityEngine.UI.Toggle;

[Serializable]
public class StatUI
{
	public Button button;
	public TextMeshProUGUI txtCurrent, txtNext, txtCoin;

	[HideInInspector] public int currentLevel = 0, maxLevel = 10, price = 0;
}

public class UpdateBaseStatUI : View<GameApp>
{
	[Required, SerializeField] private StatUI
		_statAttackDamage,
		_statAttackRange,
		_statAttackSpeed,
		_statHealth,
		_statCriticalRate,
		_statCriticalDamage,
		_statRegenHp;

	[Required, SerializeField] private TextMeshProUGUI _txtLabelAd, _txtLabelAr, _txtLabelAs, _txtLabelHealth, _txtCriticalRate, _txtCriticalDamage, _txtRegenHp;

	[Required, SerializeField] private Toggle _toggleAtk, _toggleDef, _toggleElemental;

	[Required, SerializeField] private Image _imgAtk, _imgDef, _imgElemental;

	[Required, SerializeField] private GameObject _imgFocus;
	[Required, SerializeField] private GameObject _containerStatAtk, _containerStatDef;
	private GameController gameController => Singleton<GameController>.instance;
	private DataPlayerModel dataPlayer => app.models.dataPlayerModel;
	private TowerModel towerModel => gameController.tower.model;

	protected override void OnViewInit()
	{
		base.OnViewInit();

		_toggleAtk.onValueChanged.AddListener(OnClickToggleAtk);
		_toggleDef.onValueChanged.AddListener(OnClickToggleDef);
		_toggleElemental.onValueChanged.AddListener(OnClickToggleElemental);

		_statAttackDamage.button.onClick.AddListener(OnClickBtnAttackDamage);
		_statAttackRange.button.onClick.AddListener(OnClickBtnAttackRange);
		_statAttackSpeed.button.onClick.AddListener(OnClickBtnAttackSpeed);
		_statCriticalRate.button.onClick.AddListener(OnClickBtnCriticalRate);
		_statCriticalDamage.button.onClick.AddListener(OnClickBtnCriticalDamage);
		
		_statHealth.button.onClick.AddListener(OnClickBtnHealth);
		_statRegenHp.button.onClick.AddListener(OnClickBtnRegenHp);

		var maxLevel = app.configs.dataLevelTowerOutGame.GetLengthConfig();
		_statAttackDamage.maxLevel = maxLevel;
		_statAttackRange.maxLevel = maxLevel;
		_statAttackSpeed.maxLevel = maxLevel;
		_statHealth.maxLevel = maxLevel;

		AddEventChangeStat();
	}

	private void OnClickBtnAttackDamage()
	{
		dataPlayer.Coin -= _statAttackDamage.price;
		dataPlayer.LevelAd++;
		_statAttackDamage.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.AttackDamage, dataPlayer.LevelAd);
	}

	private void OnClickBtnAttackRange()
	{
		dataPlayer.Coin -= _statAttackRange.price;
		dataPlayer.LevelAr++;
		_statAttackRange.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.AttackRange, dataPlayer.LevelAr);
	}

	private void OnClickBtnAttackSpeed()
	{
		dataPlayer.Coin -= _statAttackSpeed.price;
		dataPlayer.LevelAs++;
		_statAttackSpeed.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.AttackSpeed, dataPlayer.LevelAs);
	}

	private void OnClickBtnHealth()
	{
		dataPlayer.Coin -= _statHealth.price;
		dataPlayer.LevelHealth++;
		_statHealth.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.Health, dataPlayer.LevelHealth);
	}
	
	private void OnClickBtnCriticalRate()
	{
		dataPlayer.Coin -= _statCriticalRate.price;
		dataPlayer.LevelCr++;
		_statCriticalRate.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.CriticalRate, dataPlayer.LevelCr);
	}
	
	private void OnClickBtnCriticalDamage()
	{
		dataPlayer.Coin -= _statCriticalDamage.price;
		dataPlayer.LevelCd++;
		_statCriticalDamage.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.CriticalDamage, dataPlayer.LevelCd);
	}
	
	private void OnClickBtnRegenHp()
	{
		dataPlayer.Coin -= _statRegenHp.price;
		dataPlayer.LevelRegenHp++;
		_statRegenHp.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
		//app.analytics.TrackUpStat(false, TypeStatTower.RegenHp, dataPlayer.LevelRegenHp);
	}

	private void UpdateStatUI(StatUI statUI, DataLevelTowerConfig dataCurrent, int currentLevel, TypeStatTower type)
	{
		statUI.currentLevel = currentLevel;
		if(statUI.currentLevel < statUI.maxLevel)
		{
			var dataNext = app.configs.dataLevelTowerOutGame.GetConfigStat(currentLevel + 1, type);
			statUI.txtCurrent.text = $"{dataCurrent.value}";
			statUI.txtCoin.text = $"{GameConst.iconCoin} {dataNext.price}";
			statUI.txtNext.text = $"{dataNext.value}";
			statUI.price = dataNext.price;
			CheckInteractableBtnStat(statUI);
		}
		else
		{
			statUI.button.interactable = false;
			statUI.txtNext.transform.parent.gameObject.SetActive(false);
			statUI.txtCoin.text = "<size=300%> MAX";
		}
	}

	private void AddEventChangeStat()
	{
		AddDataBinding("fieldPlayerTower-levelAdValue", _txtLabelAd, (control, e) =>
			{
				var levelStat = dataPlayer.LevelAd;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackDamage);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).AttackDamage}";
				UpdateStatUI(_statAttackDamage, dataCurrent, levelStat, TypeStatTower.AttackDamage);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelAd), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelArValue", _txtLabelAr, (control, e) =>
			{
				var levelStat = dataPlayer.LevelAr;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackRange);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).AttackRange}";
				UpdateStatUI(_statAttackRange, dataCurrent, levelStat, TypeStatTower.AttackRange);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelAr), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-LevelAsValue", _txtLabelAs, (control, e) =>
			{
				var levelStat = dataPlayer.LevelAs;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackSpeed);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).AttackSpeed}";
				UpdateStatUI(_statAttackSpeed, dataCurrent, levelStat, TypeStatTower.AttackSpeed);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelAs), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelHealthValue", _txtLabelHealth, (control, e) =>
			{
				var levelStat = dataPlayer.LevelHealth;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.Health);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).Health}";
				UpdateStatUI(_statHealth, dataCurrent, levelStat, TypeStatTower.Health);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelHealth), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelCriticalRateValue", _txtCriticalRate, (control, e) =>
			{
				var levelStat = dataPlayer.LevelCr;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.CriticalRate);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).CriticalRate}";
				UpdateStatUI(_statCriticalRate, dataCurrent, levelStat, TypeStatTower.CriticalRate);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelCr), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelCriticalDamageValue", _txtCriticalDamage, (control, e) =>
			{
				var levelStat = dataPlayer.LevelCd;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.CriticalDamage);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).CriticalDamage}";
				UpdateStatUI(_statCriticalDamage, dataCurrent, levelStat, TypeStatTower.CriticalDamage);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelCd), dataPlayer)
		);
		
		AddDataBinding("fieldPlayerTower-levelRegenHpValue", _txtRegenHp, (control, e) =>
			{
				var levelStat = dataPlayer.LevelRegenHp;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.RegenHp);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).RegenHp}";
				UpdateStatUI(_statRegenHp, dataCurrent, levelStat, TypeStatTower.RegenHp);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.LevelRegenHp), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-goldCoinValue", this, (control, e) =>
			{
				CheckInteractableBtnStat(_statAttackDamage);
				CheckInteractableBtnStat(_statAttackSpeed);
				CheckInteractableBtnStat(_statAttackRange);
				CheckInteractableBtnStat(_statCriticalRate);
				CheckInteractableBtnStat(_statCriticalDamage);
				
				CheckInteractableBtnStat(_statHealth);
				CheckInteractableBtnStat(_statRegenHp);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Coin), app.models.dataPlayerModel)
		);
	}

	private void CheckInteractableBtnStat(StatUI button)
	{
		button.button.interactable = app.models.dataPlayerModel.Coin >= button.price && (button.currentLevel < button.maxLevel);
	}

	private void OnClickToggleAtk(bool result)
	{
		ClickedToggle(_imgAtk, result);
		_containerStatAtk.SetActive(result);
	}

	private void OnClickToggleDef(bool result)
	{
		ClickedToggle(_imgDef, result);
		_containerStatDef.SetActive(result);
	}

	private void OnClickToggleElemental(bool result)
	{
		ClickedToggle(_imgElemental, result);
	}

	private void ClickedToggle(Image img, bool result)
	{
		if(result)
		{
			_imgFocus.transform.position = new Vector3(img.transform.position.x, _imgFocus.transform.position.y);
			_imgFocus.transform.localScale = Vector3.zero;

			_imgFocus.transform.DOScale(Vector3.one, 0.25f);
			img.DOColor(new Color(1, 0.8313726f, 0.4196079f), 0.5f);
			img.transform.DOScale(Vector3.one * 1.25f, 0.5f);
		}
		else
		{
			img.DOColor(Color.white, 0.5f);
			img.transform.DOScale(Vector3.one, 0.5f);
		}
	}
}