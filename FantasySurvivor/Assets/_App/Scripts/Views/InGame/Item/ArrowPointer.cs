using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : View<GameApp>
{
	[SerializeField] private Camera uiCamera;
	[SerializeField] private Sprite arrowSprite;
	[SerializeField] private Sprite crossSprite;
	
	[SerializeField] private Transform _targetPosition;
	[SerializeField] private RectTransform _pointerRectTransform;
	[SerializeField] private Image pointerImage;
	
	private readonly float _borderSize = 100f;
	private Vector3 toPosition => _targetPosition.position;

	private void FixedUpdate()
	{
		var targetPositionScreenPoint = Camera.main.WorldToScreenPoint(_targetPosition.position);
		var width = Screen.width - _borderSize;
		var height = Screen.height - _borderSize;
		var isOffScreen = targetPositionScreenPoint.x <= _borderSize
		                   || targetPositionScreenPoint.x >= width
		                   || targetPositionScreenPoint.y <= _borderSize
		                   || targetPositionScreenPoint.y >= height;

		if(isOffScreen)
		{
			RotatePointerTowardsTargetPosition();
			Show();
			var cappedTargetScreenPosition = targetPositionScreenPoint;
			if(cappedTargetScreenPosition.x <= _borderSize) cappedTargetScreenPosition.x = _borderSize;
			if(cappedTargetScreenPosition.x >= Screen.width) cappedTargetScreenPosition.x = width;
			if(cappedTargetScreenPosition.y <= _borderSize) cappedTargetScreenPosition.y = _borderSize;
			if(cappedTargetScreenPosition.y >= Screen.height) cappedTargetScreenPosition.y = height;

			_pointerRectTransform.position =  uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
			var localPosition = _pointerRectTransform.localPosition;
			_pointerRectTransform.localPosition =  new Vector3(localPosition.x,localPosition.y, 0f);
		}
		else
		{
			Hide();
			_pointerRectTransform.position = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
			var localPosition = _pointerRectTransform.localPosition;
			_pointerRectTransform.localPosition =  new Vector3(localPosition.x,localPosition.y, 0f);
			_pointerRectTransform.localEulerAngles = Vector3.zero;
		}

	}
	private void RotatePointerTowardsTargetPosition()
	{
		var formPosition = Camera.main.transform.position;
		formPosition.z = 0f;
		var dir = (toPosition - formPosition).normalized;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		if(angle < 0) angle += 360;
		_pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
	}

	public void Hide()
	{
		_pointerRectTransform.gameObject.SetActive(false);
	}

	public void Show()
	{
		_pointerRectTransform.gameObject.SetActive(true);
	}
}