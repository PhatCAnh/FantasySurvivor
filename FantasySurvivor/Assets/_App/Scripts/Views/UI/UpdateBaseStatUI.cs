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
	[Required, SerializeField] private StatUI _statAttackDamage, _statAttackRange, _statAttackSpeed, _statHealth, _statCriticalRate, _statCriticalDamage;

	[Required, SerializeField] private TextMeshProUGUI _txtLabelAd, _txtLabelAr, _txtLabelAs, _txtLabelHealth, _txtCriticalRate, _txtCriticalDamage;

	[Required, SerializeField] private Toggle _toggleAtk, _toggleDef, _toggleElemental;

	[Required, SerializeField] private Image _imgAtk, _imgDef, _imgElemental;

	[Required, SerializeField] private GameObject _imgFocus, _containerStatATK, containerStatDef;
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

		var maxLevel = app.configs.dataLevelTowerOutGame.GetLengthConfig();
		_statAttackDamage.maxLevel = maxLevel;
		_statAttackRange.maxLevel = maxLevel;
		_statAttackSpeed.maxLevel = maxLevel;
		_statHealth.maxLevel = maxLevel;

		AddEventChangeStat();


	}

	private void OnClickBtnAttackDamage()
	{
		dataPlayer.coin -= _statAttackDamage.price;
		dataPlayer.levelAd++;
		_statAttackDamage.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnAttackRange()
	{
		dataPlayer.coin -= _statAttackRange.price;
		dataPlayer.levelAr++;
		_statAttackRange.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnAttackSpeed()
	{
		dataPlayer.coin -= _statAttackSpeed.price;
		dataPlayer.levelAs++;
		_statAttackSpeed.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnHealth()
	{
		dataPlayer.coin -= _statHealth.price;
		dataPlayer.levelHealth++;
		_statHealth.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}
	
	private void OnClickBtnCriticalRate()
	{
		dataPlayer.coin -= _statCriticalRate.price;
		dataPlayer.levelCr++;
		_statCriticalRate.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}
	
	private void OnClickBtnCriticalDamage()
	{
		dataPlayer.coin -= _statCriticalDamage.price;
		dataPlayer.levelCd++;
		_statCriticalDamage.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
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
				var levelStat = dataPlayer.levelAd;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackDamage);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackDamage}";
				UpdateStatUI(_statAttackDamage, dataCurrent, levelStat, TypeStatTower.AttackDamage);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAd), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelArValue", _txtLabelAr, (control, e) =>
			{
				var levelStat = dataPlayer.levelAr;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackRange);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackRange}";
				UpdateStatUI(_statAttackRange, dataCurrent, levelStat, TypeStatTower.AttackRange);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAr), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-LevelAsValue", _txtLabelAs, (control, e) =>
			{
				var levelStat = dataPlayer.levelAs;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackSpeed);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackSpeed}";
				UpdateStatUI(_statAttackSpeed, dataCurrent, levelStat, TypeStatTower.AttackSpeed);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAs), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelHealthValue", _txtLabelHealth, (control, e) =>
			{
				var levelStat = dataPlayer.levelHealth;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.Health);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).health}";
				UpdateStatUI(_statHealth, dataCurrent, levelStat, TypeStatTower.Health);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelHealth), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelCriticalRateValue", _txtCriticalRate, (control, e) =>
			{
				var levelStat = dataPlayer.levelCr;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.CriticalRate);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).criticalRate}";
				UpdateStatUI(_statCriticalRate, dataCurrent, levelStat, TypeStatTower.CriticalRate);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelCr), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelCriticalDamageValue", _txtCriticalDamage, (control, e) =>
			{
				var levelStat = dataPlayer.levelCd;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.CriticalDamage);
				control.text = $"{dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).criticalDamage}";
				UpdateStatUI(_statCriticalDamage, dataCurrent, levelStat, TypeStatTower.CriticalDamage);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelCd), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-goldCoinValue", this, (control, e) =>
			{
				CheckInteractableBtnStat(_statHealth);
				CheckInteractableBtnStat(_statAttackDamage);
				CheckInteractableBtnStat(_statAttackSpeed);
				CheckInteractableBtnStat(_statAttackRange);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.coin), app.models.dataPlayerModel)
		);
	}

	private void CheckInteractableBtnStat(StatUI button)
	{
		button.button.interactable = app.models.dataPlayerModel.coin >= button.price && (button.currentLevel < button.maxLevel);
	}

	private void OnClickToggleAtk(bool result)
	{
		ClickedToggle(_imgAtk, result);
		_containerStatATK.SetActive(result);
	}

	private void OnClickToggleDef(bool result)
	{
		ClickedToggle(_imgDef, result);
		containerStatDef.SetActive(result);
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