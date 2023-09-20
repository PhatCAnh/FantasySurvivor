using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DataConfig;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public class StatUI
{
	public Button button;
	public TextMeshProUGUI txtCurrent, txtNext, txtCoin;

	[HideInInspector] public int currentLevel = 0, maxLevel = 10, price = 0;
}

public partial class UpdateBaseStatUI : View<GameApp>
{
	

	public StatUI statAttackDamage, statAttackRange, statAttackSpeed, statHealth;

	public TextMeshProUGUI txtLabelAd, txtLabelAr, txtLabelAs, txtLabelHealth;
	private GameController gameController => Singleton<GameController>.instance;
	private DataPlayerModel dataPlayer => app.models.dataPlayerModel;
	private TowerModel towerModel => gameController.tower.model;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		statAttackDamage.button.onClick.AddListener(OnClickBtnAttackDamage);
		statAttackRange.button.onClick.AddListener(OnClickBtnAttackRange);
		statAttackSpeed.button.onClick.AddListener(OnClickBtnAttackSpeed);
		statHealth.button.onClick.AddListener(OnClickBtnHealth);
		
		var maxLevel = app.configs.dataLevelTowerOutGame.GetLengthConfig();
		statAttackDamage.maxLevel = maxLevel;
		statAttackRange.maxLevel = maxLevel;
		statAttackSpeed.maxLevel = maxLevel;
		statHealth.maxLevel = maxLevel;
		
		AddEventChangeStat();
	}

	public void OnClickBtnAttackDamage()
	{
		dataPlayer.coin -= statAttackDamage.price;
		dataPlayer.levelAd++;
		statAttackDamage.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnAttackRange()
	{
		dataPlayer.coin -= statAttackRange.price;
		dataPlayer.levelAr++;
		statAttackRange.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnAttackSpeed()
	{
		dataPlayer.coin -= statAttackSpeed.price;
		dataPlayer.levelAs++;
		statAttackSpeed.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void OnClickBtnHealth()
	{
		dataPlayer.coin -= statHealth.price;
		dataPlayer.levelHealth++;
		statHealth.currentLevel++;
		app.models.WriteModel<DataPlayerModel>();
	}

	private void UpdateStatUI(StatUI statUI, DataLevelTowerConfig dataCurrent,  int currentLevel, TypeStatTower type)
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
		AddDataBinding("fieldPlayerTower-levelAdValue", txtLabelAd, (control, e) =>
			{
				var levelStat = dataPlayer.levelAd;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackDamage);
				control.text = $"{GameConst.iconAd} {dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackDamage}";
				UpdateStatUI(statAttackDamage, dataCurrent, levelStat, TypeStatTower.AttackDamage);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAd), dataPlayer)
		);

		AddDataBinding("fieldPlayerTower-levelArValue", txtLabelAr, (control, e) =>
			{
				var levelStat = dataPlayer.levelAr;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackRange);
				control.text = $"{GameConst.iconAr} {dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackRange}";
				UpdateStatUI(statAttackRange, dataCurrent, levelStat, TypeStatTower.AttackRange);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAr), dataPlayer)
		);
		
		AddDataBinding("fieldPlayerTower-LevelAsValue", txtLabelAs, (control, e) =>
			{
				var levelStat = dataPlayer.levelAs;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.AttackSpeed);
				control.text = $"{GameConst.iconAs} {dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackSpeed}";
				UpdateStatUI(statAttackSpeed, dataCurrent, levelStat, TypeStatTower.AttackSpeed);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAs), dataPlayer)
		);
		
		AddDataBinding("fieldPlayerTower-levelHealthValue", txtLabelHealth, (control, e) =>
			{
				var levelStat = dataPlayer.levelHealth;
				var dataCurrent = app.configs.dataLevelTowerOutGame.GetConfigStat(levelStat, TypeStatTower.Health);
				control.text = $"{GameConst.iconHealth} {dataCurrent.value + app.configs.dataStatTower.GetConfig(TowerType.Basic).health}";
				UpdateStatUI(statHealth, dataCurrent, levelStat, TypeStatTower.Health);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelHealth), dataPlayer)
		);
		
		AddDataBinding("fieldPlayerTower-goldCoinValue", this, (control, e) =>
			{
				CheckInteractableBtnStat(statHealth);
				CheckInteractableBtnStat(statAttackDamage);
				CheckInteractableBtnStat(statAttackSpeed);
				CheckInteractableBtnStat(statAttackRange);
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.coin), app.models.dataPlayerModel)
		);
	}

	private void CheckInteractableBtnStat(StatUI button)
	{
		button.button.interactable = app.models.dataPlayerModel.coin >= button.price && (button.currentLevel < button.maxLevel);
	}
}