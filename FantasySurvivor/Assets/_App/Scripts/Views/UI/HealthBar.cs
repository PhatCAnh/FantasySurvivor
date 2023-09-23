using System;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace FantasySurvivor
{
	public class HealthBar : View<GameApp>
	{
		[Required, SerializeField] private Slider _sldHealthPoint;

		[Required, SerializeField] private TextMeshProUGUI _txtPoint;

		private TowerView _towerView;

		private Vector2 _uiPos;

		protected override void OnViewInit()
		{
			var model = _towerView.model;
			
			AddDataBinding("sldHealthBar-maxValue", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.maxValue = model.maxHealthPoint;
				},
				new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.maxHealthPoint), _towerView.model)
			);
			
			AddDataBinding("sldHealthBar-value", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.value = model.currentHealthPoint;
				},
				new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.currentHealthPoint), _towerView.model)
			);
			
			var position = _towerView.transform.position;
			_uiPos = new Vector2(position.x, position.y + 3);
		}

		private void LateUpdate()
		{
			transform.position = Camera.main.WorldToScreenPoint(_uiPos);
		}

		public void Init(TowerView towerView)
		{
			_towerView = towerView;
		}
		
		
	}
}