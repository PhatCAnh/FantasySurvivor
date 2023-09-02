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

		private TowerView _towerView => Singleton<GameController>.instance.tower;

		protected override void OnViewInit()
		{
			var model = _towerView.model;
			
			transform.SetParent(app.resourceManager.rootContainer);
			
			AddDataBinding("sldHealthBar-maxValue", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.maxValue = model.maxHealthPoint;
				},
				new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.maxHealthPoint), model)
			);
			
			AddDataBinding("sldHealthBar-value", _sldHealthPoint, (control, e) =>
				{
					_txtPoint.text = $"{model.currentHealthPoint} / {model.maxHealthPoint}";
					control.value = model.currentHealthPoint;
				},
				new DataChangedValue(TowerModel.dataChangedEvent, nameof(TowerModel.currentHealthPoint), model)
			);
			
			var position = _towerView.transform.position;
			var uiPos = new Vector2(position.x, position.y + 2);
			
			transform.position = Camera.main.WorldToScreenPoint(uiPos);
		}
	}
}