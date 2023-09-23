using System;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Popup
{

	public class MainUIInGame : View<GameApp>, IPopup
	{
		[Required, SerializeField] private Sprite _spriteArrowUp, _spriteArrowDown;
		
		[Required, SerializeField] private Button _btnSetting;

		[Required, SerializeField] private RectTransform _interactContainer;
		
		[Required, SerializeField] private Image _imgArrow, _imgStat, _imgElemental;
		
		[Required, SerializeField] private Toggle _toggleStat, _toggleElemental, _toggleChangeStateInteract;

		[SerializeField] private TextMeshProUGUI _txtExpInMap, _txtLevel, _txtTimeMinutes, _txtTimeSeconds, _txtMonsterKilled;
		private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

		private MapModel mapModel => gameController.map.model;
		private TowerModel towerModel => gameController.tower.model;

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
			
			
			
			AddDataBinding("fieldMap-expInMapValue", _txtExpInMap, (control, e) =>
				{
					var coin = mapModel.expInGame;
					control.text = coin.ToString();
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.expInGame), mapModel)
			);
			
			AddDataBinding("fieldMap-levelInGameValue", _txtLevel, (control, e) =>
				{
					control.text = $"{mapModel.levelInGame}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.levelInGame), mapModel)
			);
			
			AddDataBinding("fieldMap-monsterKilledValue", _txtMonsterKilled, (control, e) =>
				{
					control.text = $"{mapModel.monsterKilled}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.monsterKilled), mapModel)
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
		
		private void OnClickToggleChangeStateInteract(bool value)
		{
			if(value)
			{
				Camera.main.transform.DOLocalMove(Vector3.back, 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, 25, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, -770), 1f);
				_imgArrow.sprite = _spriteArrowUp;
			}
			else
			{
				Camera.main.transform.DOLocalMove(new Vector3(0, -10, -1), 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, 30, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, -0), 1f);
				_imgArrow.sprite = _spriteArrowDown;
			}
		}
		

		private void OnClickBtnSetting()
		{
			app.resourceManager.ShowPopup(PopupType.Pause);
		}

		private void OnClickToggleStat(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			_imgStat.color = value ? new Color(1, 0.8313726f, 0.4196079f) : Color.white;
		}

		private void OnClickToggleElemental(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			_imgElemental.color = value ? new Color(1, 0.8313726f, 0.4196079f) : Color.white;
		}
	}
}