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

namespace FantasySurvivor
{

	public class MainUIInGame : View<GameApp>, IPopup
	{
		[Required, SerializeField] private Sprite _spriteArrowUp, _spriteArrowDown;

		[Required, SerializeField] private Button _btnSetting;

		[Required, SerializeField] private Slider _sldExpCharacter;

		[Required, SerializeField] private RectTransform _interactContainer;

		[Required, SerializeField] private Image _imgArrow, _imgStat, _imgElemental;

		[Required, SerializeField] private Toggle _toggleStat, _toggleDef, _toggleChangeStateInteract;

		[Required, SerializeField] private GameObject _containerStatAtk, _containerStatDef;

		[SerializeField] private TextMeshProUGUI _txtLevelCharacter, _txtLevel, _txtTimeMinutes, _txtTimeSeconds, _txtMonsterKilled;
		public GameController gameController => Singleton<GameController>.instance;
		private MapModel mapModel => gameController.map.model;
		private CharacterModel towerModel => gameController.character.model;

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
			_toggleDef.onValueChanged.AddListener(OnClickToggleDEF);

			AddDataBinding("fieldMap-expCurrentValue", _sldExpCharacter, (control, e) =>
				{
					
					control.value = (float) mapModel.ExpCurrent / mapModel.ExpMax;
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.ExpCurrent), mapModel)
			);
			
			AddDataBinding("fieldMap-levelCharacterValue", _txtLevelCharacter, (control, e) =>
				{
					control.text = $"{mapModel.LevelCharacter}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.LevelCharacter), mapModel)
			);

			AddDataBinding("fieldMap-levelInGameValue", _txtLevel, (control, e) =>
				{
					control.text = $"WAVE: {mapModel.levelInGame}";
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
			var endValue = Camera.main.orthographicSize;
			if(value)
			{
				Camera.main.transform.DOLocalMove(Vector3.back, 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, endValue - 5, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, -1050), 1f);
				_imgArrow.sprite = _spriteArrowUp;
			}
			else
			{
				Camera.main.transform.DOLocalMove(new Vector3(0, -10, -1), 1f);
				DOTween.To(() => Camera.main.orthographicSize, value => Camera.main.orthographicSize = value, endValue + 5, 1f);
				_interactContainer.DOLocalMove(new Vector3(0, 0), 1f);
				_imgArrow.sprite = _spriteArrowDown;
			}
		}


		private void OnClickBtnSetting()
		{
			gameController.ChangeScene("scn_Game", null);
			//app.resourceManager.ShowPopup(PopupType.Pause);
		}

		private void OnClickToggleStat(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			_imgStat.color = value ? new Color(1, 0.8313726f, 0.4196079f) : Color.white;
			_containerStatAtk.SetActive(value);
		}

		private void OnClickToggleDEF(bool value)
		{
			_toggleChangeStateInteract.isOn = false;
			_imgElemental.color = value ? new Color(1, 0.8313726f, 0.4196079f) : Color.white;
			_containerStatDef.SetActive(value);
		}
	}
}