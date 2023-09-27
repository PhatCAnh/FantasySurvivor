using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UpdateBaseStatUiInGame : View<GameApp>
{
	[Required, SerializeField] private StatUI _attackDamage, _attackRange, _attackSpeed, _health, _criticalRate, _criticalDamage, _regenHp;
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
		_regenHp.button.onClick.AddListener(OnClickBtnUpRegenHp);

		var maxLevel = app.configs.dataLevelTowerInGame.GetLengthConfig();
		_attackDamage.maxLevel = maxLevel;
		_attackRange.maxLevel = maxLevel;
		_attackSpeed.maxLevel = maxLevel;
		_health.maxLevel = maxLevel;
		_criticalRate.maxLevel = maxLevel;
		_criticalDamage.maxLevel = maxLevel;
		_regenHp.maxLevel = maxLevel;

		AddEventChangeStat();
	}

	private void AddEventChangeStat()
	{
		AddDataBinding("fieldTower-levelAdValue", this, (control, e) =>
			{
				var value = ChangeStatEvent(TypeStatTower.AttackDamage, towerModel.levelAd, _attackDamage);
				towerModel.attackDamage += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAd), towerModel)
		);

		AddDataBinding("fieldTower-levelArValue", this, (control, e) =>
			{
				towerModel.attackRange += ChangeStatEvent(TypeStatTower.AttackRange, towerModel.levelAr, _attackRange);
				
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAr), towerModel)
		);

		AddDataBinding("fieldTower-levelAsValue", this, (control, e) =>
			{
				towerModel.attackSpeed += ChangeStatEvent(TypeStatTower.AttackSpeed, towerModel.levelAs, _attackSpeed);

			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelAs), towerModel)
		);

		AddDataBinding("fieldTower-levelHealthValue", this, (control, e) =>
			{
				towerModel.maxHealthPoint += ChangeStatEvent(TypeStatTower.Health, towerModel.levelHealth, _health);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelHealth), towerModel)
		);
		
		AddDataBinding("fieldTower-levelCriticalRateValue", this, (control, e) =>
			{
				var value = ChangeStatEvent(TypeStatTower.CriticalRate, towerModel.levelCr, _criticalRate);
				towerModel.criticalRate += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelCr), towerModel)
		);
		
		AddDataBinding("fieldTower-levelCriticalDamageValue", this, (control, e) =>
			{
				var value = ChangeStatEvent(TypeStatTower.CriticalDamage, towerModel.levelCd, _criticalDamage);
				towerModel.criticalDamage += Mathf.RoundToInt(value);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelCd), towerModel)
		);

		AddDataBinding("fieldTower-levelRegenHpValue", this, (control, e) =>
			{
				towerModel.regenHp = ChangeStatEvent(TypeStatTower.RegenHp, towerModel.levelRegenHp, _regenHp);
			}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.levelRegenHp), towerModel)
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
				CheckInteractableBtnStat(_regenHp, coin);
			}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.expInGame), mapModel)
		);
	}

	private float ChangeStatEvent(TypeStatTower type, int level, StatUI statUI)
	{
		var dataBase = gameController.tower.GetBaseStat(type);
		var dataCurrent = app.configs.dataLevelTowerInGame.GetConfigStat(level, type);
		var dataNext = app.configs.dataLevelTowerInGame.GetConfigStat(level + 1, type);

		var value = dataBase + dataCurrent.value;
				
		statUI.txtCurrent.text = String.Format($"{value}");
		statUI.txtNext.text = String.Format($"{dataBase + dataNext.value}");
		statUI.txtCoin.text = String.Format($"{GameConst.iconExp} {dataNext.price}");

		statUI.price = dataNext.price;
		CheckInteractableBtnStat(statUI, mapModel.expInGame);
		return value;
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
		mapModel.expInGame -= _criticalRate.price;
		towerModel.levelCr++;
	}
	
	private void OnClickBtnUpCriticalDamage()
	{
		mapModel.expInGame -= _criticalDamage.price;
		towerModel.levelCd++;
	}
	
	private void OnClickBtnUpRegenHp()
	{
		mapModel.expInGame -= _regenHp.price;
		towerModel.levelRegenHp++;
	}

	private void CheckInteractableBtnStat(StatUI button, int coin)
	{
		button.button.interactable = coin >= button.price && (button.currentLevel <= button.maxLevel);
	}
}