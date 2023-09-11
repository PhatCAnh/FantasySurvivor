using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Popup
{
	[Serializable]
	public class ButtonUpdateStat
	{
		[Required] public Button button;
		[Required] public TextMeshProUGUI coinToUpdate;

		public int currentLevel = 0;
		public int maxLevel = 10;
		public int price = 0;
	}

	public class MainUIInGame : View<GameApp>, IPopup
	{
		[Required, SerializeField] private TextMeshProUGUI _txtTimeMinutes;
		
		[Required, SerializeField] private TextMeshProUGUI _txtTimeSeconds;
		
		[Required, SerializeField] private Sprite _spriteArrowUp;

		[Required, SerializeField] private Sprite _spriteArrowDown;

		[Required, SerializeField] private Button _btnSetting;

		[Required, SerializeField] private TextMeshProUGUI _txtCoinInMap;

		[Required, SerializeField] private Toggle _toggleChangeStateInteract;

		[Required, SerializeField] private RectTransform _interactContainer;

		[Required, SerializeField] private Toggle _toggleStat;

		[Required, SerializeField] private Toggle _toggleElemental;

		[Required, SerializeField] private ButtonUpdateStat _attackDamage;

		[Required, SerializeField] private ButtonUpdateStat _attackRange;

		[Required, SerializeField] private ButtonUpdateStat _attackSpeed;

		[Required, SerializeField] private ButtonUpdateStat _health;

		[Required, SerializeField] private TextMeshProUGUI _txtAttackDamage;

		[Required, SerializeField] private TextMeshProUGUI _txtAttackRange;

		[Required, SerializeField] private TextMeshProUGUI _txtAttackSpeed;

		[Required, SerializeField] private TextMeshProUGUI _txtHealth;

		private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

		private MapModel mapModel => gameController.map.model;
		private TowerModel model => gameController.tower.model;

		public void Open()
		{
		}
		public void Close()
		{
			Destroy(gameObject);
		}

		protected override void OnViewInit()
		{
			base.OnViewInit();
			_btnSetting.onClick.AddListener(OnClickBtnSetting);
			_toggleChangeStateInteract.onValueChanged.AddListener(OnClickToggleChangeStateInteract);
			_toggleStat.onValueChanged.AddListener(OnClickToggleStat);
			_toggleElemental.onValueChanged.AddListener(OnClickToggleElemental);
			_attackDamage.button.onClick.AddListener(OnClickBtnUpAttackDamage);
			_attackRange.button.onClick.AddListener(OnClickBtnUpAttackRange);
			_attackSpeed.button.onClick.AddListener(OnClickBtnUpAttackSpeed);
			_health.button.onClick.AddListener(OnClickBtnUpHealth);

			LoadDataUpAttackDamage();
			LoadDataUpAttackRange();
			LoadDataUpAttackSpeed();
			LoadDataUpHealth();

			AddEventChangeStat();
		}
		private void OnClickToggleChangeStateInteract(bool value)
		{
			if(value)
			{
				Camera.main.transform.DOLocalMove(Vector3.back, 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, 25, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, -750), 1f);
				_toggleChangeStateInteract.GetComponent<Image>().sprite = _spriteArrowUp;
			}
			else
			{
				Camera.main.transform.DOLocalMove(new Vector3(0, -10, -1), 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, 30, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, -360), 1f);
				_toggleChangeStateInteract.GetComponent<Image>().sprite = _spriteArrowDown;
			}
		}

		private void OnClickToggleStat(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			ChangeColorToggle(_toggleStat, value);
		}

		private void OnClickToggleElemental(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			ChangeColorToggle(_toggleElemental, value);
		}

		private void ChangeColorToggle(Toggle toggle, bool value)
		{
			toggle.GetComponent<Image>().color = value ? new Color(1, 0.8313726f, 0.4196079f) : Color.white;
		}

		private void OnClickBtnSetting()
		{
			app.resourceManager.ShowPopup(PopupType.Pause);
		}

		private void AddEventChangeStat()
		{
			AddDataBinding("fieldTower-attackDamageValue", _txtAttackDamage, (control, e) =>
				{
					control.text = String.Format($"<sprite index=2> {model.attackDamage}");
				}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackDamage), model)
			);

			AddDataBinding("fieldTower-attackRangeValue", _txtAttackRange, (control, e) =>
				{
					control.text = String.Format($"<sprite index=4> {(float) Math.Round(model.attackRange, 2)}");
				}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackRange), model)
			);

			AddDataBinding("fieldTower-attackSpeedValue", _txtAttackSpeed, (control, e) =>
				{
					control.text = String.Format($"<sprite index=3> {(float) Math.Round(model.attackSpeed, 1)}");
				}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.attackSpeed), model)
			);

			AddDataBinding("fieldTower-healthValue", _txtHealth, (control, e) =>
				{
					control.text = String.Format($"<sprite index=5> {model.maxHealthPoint}");
				}, new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.maxHealthPoint), model)
			);

			AddDataBinding("fieldMap-coinInMapValue", _txtCoinInMap, (control, e) =>
				{
					var coin = mapModel.coinInGame;
					control.text = coin.ToString();
					CheckInteractableBtnStat(_attackDamage, coin);
					CheckInteractableBtnStat(_attackSpeed, coin);
					CheckInteractableBtnStat(_attackRange, coin);
					CheckInteractableBtnStat(_health, coin);

				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.coinInGame), mapModel)
			);
			
			AddDataBinding("fieldMap-timeInMapValue", _txtTimeMinutes, (control, e) =>
				{
					var value = mapModel.timeInGame;
					var minutes = Mathf.FloorToInt(value / 60f);
					var seconds = Mathf.FloorToInt(value - minutes * 60);
					var timeStr = $"{minutes:00}:{seconds:00}";
					control.text = timeStr;

					_txtTimeMinutes.text = $"{minutes:00}";
					_txtTimeSeconds.text = $"{seconds:00}";

				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.timeInGame), mapModel)
			);
		}

		private void CheckInteractableBtnStat(ButtonUpdateStat button, int coin)
		{
			button.button.interactable = coin >= button.price && (button.currentLevel <= button.maxLevel);
		}

		private void OnClickBtnUpAttackDamage()
		{
			mapModel.coinInGame -= _attackDamage.price;
			LoadDataUpAttackDamage();
		}

		private void LoadDataUpAttackDamage()
		{
			var button = _attackDamage;
			var data = app.configs.dataUpStatTowerInGameConfig.GetConfig(button.currentLevel).dataAttackDamage;
			if(button.currentLevel < button.maxLevel)
			{
				button.coinToUpdate.text = String.Format($"{data.price} <sprite index=0>");
			}
			else
			{
				button.coinToUpdate.text = "Max";
				button.button.interactable = false;
			}
			button.price = data.price;
			model.attackDamage += (int) data.value;
			button.currentLevel++;
		}

		private void OnClickBtnUpAttackRange()
		{
			mapModel.coinInGame -= _attackRange.price;
			LoadDataUpAttackRange();
		}

		private void LoadDataUpAttackRange()
		{
			var button = _attackRange;
			var data = app.configs.dataUpStatTowerInGameConfig.GetConfig(button.currentLevel).dataAttackRange;
			if(button.currentLevel < button.maxLevel)
			{
				button.coinToUpdate.text = String.Format($"{data.price} <sprite index=0>");
			}
			else
			{
				button.coinToUpdate.text = "Max";
				button.button.interactable = false;
			}
			button.price = data.price;
			model.attackRange += (int) data.value;
			button.currentLevel++;
		}

		private void OnClickBtnUpAttackSpeed()
		{
			mapModel.coinInGame -= _attackSpeed.price;
			LoadDataUpAttackSpeed();
		}

		private void LoadDataUpAttackSpeed()
		{
			var button = _attackSpeed;
			var data = app.configs.dataUpStatTowerInGameConfig.GetConfig(button.currentLevel).dataAttackSpeed;
			if(button.currentLevel < button.maxLevel)
			{
				button.coinToUpdate.text = String.Format($"{data.price} <sprite index=0>");
			}
			else
			{
				button.coinToUpdate.text = "Max";
				button.button.interactable = false;
			}
			button.price = data.price;
			model.attackSpeed += (int) data.value;
			button.currentLevel++;
		}

		private void OnClickBtnUpHealth()
		{
			mapModel.coinInGame -= _health.price;
			LoadDataUpHealth();
		}

		private void LoadDataUpHealth()
		{
			var button = _health;
			var data = app.configs.dataUpStatTowerInGameConfig.GetConfig(button.currentLevel).dataHealth;
			if(button.currentLevel < button.maxLevel)
			{
				button.coinToUpdate.text = String.Format($"{data.price} <sprite index=0>");
			}
			else
			{
				button.coinToUpdate.text = "Max";
				button.button.interactable = false;
			}
			button.price = data.price;
			model.AddMaxHealth((int) data.value);
			button.currentLevel++;
		}
	}
}