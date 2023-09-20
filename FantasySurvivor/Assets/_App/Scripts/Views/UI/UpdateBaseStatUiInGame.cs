using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using Popup;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateBaseStatUiInGame : View<GameApp>
{

	[Required, SerializeField] private StatUI _attackDamage, _attackRange, _attackSpeed, _health;

	[Required, SerializeField] private TextMeshProUGUI _txtAttackDamage, _txtAttackRange, _txtAttackSpeed, _txtHealth;
	

	private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

	private MapModel mapModel => gameController.map.model;
	private TowerModel towerModel => gameController.tower.model;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		
		_attackDamage.button.onClick.AddListener(OnClickBtnUpAttackDamage);
		_attackRange.button.onClick.AddListener(OnClickBtnUpAttackRange);
		_attackSpeed.button.onClick.AddListener(OnClickBtnUpAttackSpeed);
		_health.button.onClick.AddListener(OnClickBtnUpHealth);

		var maxLevel = app.configs.dataLevelTowerInGame.GetLengthConfig();
		_attackDamage.maxLevel = maxLevel;
		_attackRange.maxLevel = maxLevel;
		_attackSpeed.maxLevel = maxLevel;
		_health.maxLevel = maxLevel;

		AddEventChangeStat();
	}

	private void AddEventChangeStat()
	{
		AddDataBinding("fieldTower-levelAdValue", _txtAttackDamage, (control, e) =>
			{
				ChangeStatEvent(TypeStatTower.AttackDamage, towerModel.levelAd, towerModel.attackDamage, _attackDamage, GameConst.iconAd, control);

			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAd), towerModel)
		);

		AddDataBinding("fieldTower-levelArValue", _txtAttackRange, (control, e) =>
			{
				ChangeStatEvent(TypeStatTower.AttackRange, towerModel.levelAr, towerModel.attackRange, _attackRange, GameConst.iconAr, control);
				
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAr), towerModel)
		);

		AddDataBinding("fieldTower-levelAsValue", _txtAttackSpeed, (control, e) =>
			{
				ChangeStatEvent(TypeStatTower.AttackSpeed, towerModel.levelAs, towerModel.attackSpeed, _attackSpeed, GameConst.iconAs, control);

			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAs), towerModel)
		);

		AddDataBinding("fieldTower-levelHealthValue", _txtHealth, (control, e) =>
			{
				ChangeStatEvent(TypeStatTower.Health, towerModel.levelHealth, towerModel.maxHealthPoint, _health, GameConst.iconHealth, control);
				
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelHealth), towerModel)
		);

		AddDataBinding("fieldMap-expInMapValue", this, (control, e) =>
			{
				var coin = mapModel.expInGame;
				CheckInteractableBtnStat(_attackDamage, coin);
				CheckInteractableBtnStat(_attackSpeed, coin);
				CheckInteractableBtnStat(_attackRange, coin);
				CheckInteractableBtnStat(_health, coin);
			}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.expInGame), mapModel)
		);
	}

	private void ChangeStatEvent(TypeStatTower type, int level, float stat, StatUI statUI, string icon, TextMeshProUGUI label)
	{
		var dataCurrent = app.configs.dataLevelTowerInGame.GetConfigStat(level, type);
		var dataNext = app.configs.dataLevelTowerInGame.GetConfigStat(level + 1, type);

		stat += Mathf.RoundToInt(dataCurrent.value);

		var value = stat;
				
		label.text = String.Format($"{icon} {value}");
		statUI.txtCurrent.text = String.Format($"{value}");
		statUI.txtNext.text = String.Format($"{value + dataNext.value}");
		statUI.txtCoin.text = String.Format($"{GameConst.iconExp} {dataNext.price}");

		statUI.price = dataNext.price;
		CheckInteractableBtnStat(statUI, mapModel.expInGame);
	}

	private void OnClickBtnUpAttackDamage()
	{
		mapModel.expInGame -= _attackDamage.price;
		towerModel.levelAd++;
	}

	private void OnClickBtnUpAttackRange()
	{
		mapModel.expInGame -= _attackRange.price;
		towerModel.levelAr++;
	}

	private void OnClickBtnUpAttackSpeed()
	{
		mapModel.expInGame -= _attackSpeed.price;
		towerModel.levelAs++;
	}

	private void OnClickBtnUpHealth()
	{
		mapModel.expInGame -= _health.price;
		towerModel.levelHealth++;
	}

	private void CheckInteractableBtnStat(StatUI button, int coin)
	{
		button.button.interactable = coin >= button.price && (button.currentLevel <= button.maxLevel);
	}
}