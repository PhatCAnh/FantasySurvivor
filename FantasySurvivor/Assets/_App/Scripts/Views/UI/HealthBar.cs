using System;
using ArbanFramework.MVC;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace FantasySurvivor
{
	public class HealthBar : View<GameApp>
	{
		[SerializeField] private Slider _sldHealthPoint;

		private void LateUpdate()
		{
			// if (_unit == null)
			// 	return;
			//
			// transform.position = Camera.main.WorldToScreenPoint(_unit.trfUI.position);
		}

		protected override void OnViewInit()
		{
			// AddDataBinding("sldOil-value", _sldHealthPoint, (control, e) =>
			// 	{
			// 		float value = _unit.model.currentHealthPoint / (float) _unit.model.maxHealthPoint;
			// 		_sldHealthPoint.value = value;
			// 	}, new DataChangedValue(
			// 		CharacterModel.dataChangedEvent,
			// 		nameof(CharacterModel.currentHealthPoint),
			// 		_unit.model
			// 	)
			// );
		}

		// public void Init(Character character)
		// {
		// 	_unit = character;
		// }
	}
}