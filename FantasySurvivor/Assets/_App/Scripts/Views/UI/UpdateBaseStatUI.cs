using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public partial class UpdateBaseStatUI : View<GameApp>
{
	[Serializable]
	public class StatUI
	{
		public Button button;
		public TextMeshProUGUI txtCurrent, txtNext, txtCoin;
		public GameObject goContent;

		[HideInInspector] public int currentLevel = 1, maxLevel = 10, price = 0;
	}

	public StatUI statAttackDamage, statAttackRange, statAttackSpeed, statAttackHealth;

	public TextMeshProUGUI txtLabelAd, txtLabelAr, txtLabelAs, txtLabelHealth;
	private GameController gameController => Singleton<GameController>.instance;
	private DataPlayerModel dataPlayer => app.models.dataPlayerModel;
	private TowerModel towerModel => gameController.tower.model;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		statAttackDamage.button.onClick.AddListener(OnClickBtnAttackDamage);
		// statAttackRange.button.onClick.AddListener(OnClickBtnAttackRange);
		// statAttackSpeed.button.onClick.AddListener(OnClickBtnAttackSpeed);
		// statAttackHealth.button.onClick.AddListener(OnClickBtnHealth);
		AddEventChangeStat();
	}

	public void OnClickBtnAttackDamage()
	{
		dataPlayer.levelAd++;
		statAttackDamage.currentLevel++;
		Debug.Log("Update level");
		app.models.WriteAll();
	}

	private void OnClickBtnAttackRange()
	{

	}

	private void OnClickBtnAttackSpeed()
	{

	}

	private void OnClickBtnHealth()
	{

	}

	private void AddEventChangeStat()
	{
		AddDataBinding("fieldPlayerTower-levelAdValue", txtLabelAd, (control, e) =>
		{
			var levelStat = dataPlayer.levelAd;
			var dataCurrent = app.configs.dataUpBaseStatTower.GetConfig(levelStat);
			var value = dataCurrent.dataAttackDamage.value;
			control.text = $"{GameConst.iconAd} {value + app.configs.dataStatTower.GetConfig(TowerType.Basic).attackDamage}";
				if(statAttackDamage.currentLevel < statAttackDamage.maxLevel - 1)
				{
					var dataNext = app.configs.dataUpBaseStatTower.GetConfig(levelStat + 1);
					statAttackDamage.txtCurrent.text = $"{value}";
					statAttackDamage.txtCoin.text = $"{GameConst.iconCoin} {dataCurrent.dataAttackDamage.price}";
					statAttackDamage.txtNext.text = $"{dataNext.dataAttackDamage.value}";
					statAttackDamage.price = dataCurrent.dataAttackDamage.price;
					Debug.Log("Update UI");
				}
				else
				{
					statAttackDamage.button.interactable = false;
					statAttackDamage.goContent.SetActive(false);
					statAttackDamage.txtCoin.text = "<size=300%> MAX";
				}
			}, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.levelAd), dataPlayer)
		);

		// AddDataBinding("fieldPlayerTower-attackRangeValue", txtLabelAr, (control, e) =>
		// 	{
		//
		// 	}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackRange), towerModel)
		// );
		//
		// AddDataBinding("fieldPlayerTower-attackSpeedValue", txtLabelAs, (control, e) =>
		// 	{
		//
		// 	}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackSpeed), towerModel)
		// );
		//
		// AddDataBinding("fieldTower-attackDamageValue", txtLabelHealth, (control, e) =>
		// 	{
		//
		// 	}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.maxHealthPoint), towerModel)
		// );
	}

	private void CheckInteractableBtnStat(StatUI button, int coin)
	{
		button.button.interactable = coin >= button.price && (button.currentLevel <= button.maxLevel);
	}
}