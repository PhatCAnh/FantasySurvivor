using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UpdateBaseStatUiInGame : View<GameApp>
{
	[Required, SerializeField] private StatUI _attackDamage, _attackRange, _attackSpeed, _health, _criticalRate, _criticalDamage;

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
		_criticalRate.button.onClick.AddListener(OnClickBtnUpCriticalRate);
		_criticalDamage.button.onClick.AddListener(OnClickBtnUpCriticalDamage);

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
				var value = ChangeStatEvent(TypeStatTower.AttackDamage, towerModel.levelAd, towerModel.attackDamage, _attackDamage);
				towerModel.attackDamage += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAd), towerModel)
		);

		AddDataBinding("fieldTower-levelArValue", _txtAttackRange, (control, e) =>
			{
				towerModel.attackRange += ChangeStatEvent(TypeStatTower.AttackRange, towerModel.levelAr, towerModel.attackRange, _attackRange);
				
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAr), towerModel)
		);

		AddDataBinding("fieldTower-levelAsValue", _txtAttackSpeed, (control, e) =>
			{
				towerModel.attackSpeed += ChangeStatEvent(TypeStatTower.AttackSpeed, towerModel.levelAs, towerModel.attackSpeed, _attackSpeed);

			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAs), towerModel)
		);

		AddDataBinding("fieldTower-levelHealthValue", _txtHealth, (control, e) =>
			{
				towerModel.maxHealthPoint += ChangeStatEvent(TypeStatTower.Health, towerModel.levelHealth, towerModel.maxHealthPoint, _health);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelHealth), towerModel)
		);
		
		AddDataBinding("fieldTower-levelCriticalRateValue", _txtHealth, (control, e) =>
			{
				var value = ChangeStatEvent(TypeStatTower.CriticalRate, towerModel.levelCr, towerModel.criticalRate, _criticalRate);
				towerModel.criticalRate += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelCr), towerModel)
		);
		
		AddDataBinding("fieldTower-levelCriticalDamageValue", _txtHealth, (control, e) =>
			{
				var value = ChangeStatEvent(TypeStatTower.CriticalDamage, towerModel.levelCd, towerModel.criticalDamage, _criticalDamage);
				towerModel.criticalDamage += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelCd), towerModel)
		);

		AddDataBinding("fieldMap-expInMapValue", this, (control, e) =>
			{
				var coin = mapModel.expInGame;
				CheckInteractableBtnStat(_attackDamage, coin);
				CheckInteractableBtnStat(_attackSpeed, coin);
				CheckInteractableBtnStat(_attackRange, coin);
				CheckInteractableBtnStat(_health, coin);
				CheckInteractableBtnStat(_criticalRate, coin);
				CheckInteractableBtnStat(_criticalDamage, coin);
			}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.expInGame), mapModel)
		);
	}

	private float ChangeStatEvent(TypeStatTower type, int level, float stat, StatUI statUI)
	{
		var dataCurrent = app.configs.dataLevelTowerInGame.GetConfigStat(level, type);
		var dataNext = app.configs.dataLevelTowerInGame.GetConfigStat(level + 1, type);

		stat += dataCurrent.value;

		var value = stat;
				
		statUI.txtCurrent.text = String.Format($"{value}");
		statUI.txtNext.text = String.Format($"{value + dataNext.value}");
		statUI.txtCoin.text = String.Format($"{GameConst.iconExp} {dataNext.price}");

		statUI.price = dataNext.price;
		CheckInteractableBtnStat(statUI, mapModel.expInGame);
		return dataCurrent.value;
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
	
	private void OnClickBtnUpCriticalRate()
	{
		mapModel.expInGame -= _health.price;
		towerModel.levelCr++;
	}
	
	private void OnClickBtnUpCriticalDamage()
	{
		mapModel.expInGame -= _health.price;
		towerModel.levelCd++;
	}

	private void CheckInteractableBtnStat(StatUI button, int coin)
	{
		button.button.interactable = coin >= button.price && (button.currentLevel <= button.maxLevel);
	}
}