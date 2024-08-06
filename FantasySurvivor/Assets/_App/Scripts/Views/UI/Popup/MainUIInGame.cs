using System;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FantasySurvivor
{

	public class MainUIInGame : View<GameApp>, IPopup
	{
		[SerializeField] private Sprite _spriteArrowUp, _spriteArrowDown;

		[SerializeField] private Button _btnSetting;

		[SerializeField] private Slider _sldExpCharacter;
		
		[SerializeField] private FloatingJoystick _floatingJoystick;

		[SerializeField] private TextMeshProUGUI _txtLevelCharacter, _txtLevel, _txtTimeMinutes, _txtTimeSeconds, _txtMonsterKilled, _txtGoldCollected;
		public GameController gameController => Singleton<GameController>.instance;
		private MapModel mapModel => gameController.map.model;
		private Character character => gameController.character;

		public void Open()
		{
		}
		public void Close()
		{
			Destroy(gameObject);
		}

		private void Update()
		{
			if (gameController.isStop) return;
			character.Controlled(new Vector2(_floatingJoystick.Horizontal, _floatingJoystick.Vertical));
		}

		protected override void OnViewInit()
		{
			base.OnViewInit();
            Singleton<GameApp>.instance.models.dataPlayerModel.IncrementDailyGamePlays();

            _btnSetting.onClick.AddListener(OnClickBtnSetting);

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
					control.text = $"WAVE: {mapModel.WaveInGame}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.WaveInGame), mapModel)
			);

			AddDataBinding("fieldMap-monsterKilledValue", _txtMonsterKilled, (control, e) =>
				{
					control.text = $"{mapModel.monsterKilled}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.monsterKilled), mapModel)
			);
			
			AddDataBinding("fieldMapModel-goldCoinCollectedValue", _txtGoldCollected, (control, e) =>
				{
					control.text = $"{mapModel.goldCoinCollected}";
				}, new DataChangedValue(MapModel.dataChangedEvent, nameof(MapModel.goldCoinCollected), mapModel)
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


		private void OnClickBtnSetting()
		{
			if(gameController.isStop) return;
			//gameController.ChangeScene(GameConst.nameScene_Game, null);
			app.resourceManager.ShowPopup(PopupType.Pause);
		}
	}
}