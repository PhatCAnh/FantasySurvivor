using System;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace FantasySurvivor
{
	public class HealthBar : View<GameApp>
	{
		[SerializeField] private Slider _sldHealthPoint;

		[SerializeField] private TextMeshProUGUI _txtPoint;

		private Character _character;
		public GameController gameController => Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			var model = _character.model;
			
			AddDataBinding("sldHealthBar-maxValue", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.maxValue = model.maxHealthPoint;
				},
				new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.maxHealthPoint), _character.model)
			);
			
			AddDataBinding("sldHealthBar-value", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.value = model.currentHealthPoint;
				},
				new DataChangedValue(CharacterModel.dataChangedEvent, nameof(CharacterModel.currentHealthPoint), _character.model)
			);
		}

		private void LateUpdate()
		{
			if(gameController.isStop) return;
			transform.position = Camera.main.WorldToScreenPoint(Vector3.up * 1.5f + _character.transform.position);
		}

		public void Init(Character character)
		{
			_character = character;
		}
		
		
	}
}