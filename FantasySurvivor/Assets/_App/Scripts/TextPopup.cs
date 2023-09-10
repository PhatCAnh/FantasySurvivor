using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TextPopup : MonoBehaviour
{
	[SerializeField] private TextMeshPro _textMesh;

	[SerializeField] private int _fontSize;

	private const float DisappearTimeMax = 1f;

	private float _disappearTimer;

	private Color _textColor;

	private Vector3 _moveVector;

	private static int _sortingOrder;

	public void Setup(string text)
	{
		_textMesh.SetText(text);
		_sortingOrder++;
		_textMesh.sortingOrder = _sortingOrder;
		_disappearTimer = DisappearTimeMax;
		_moveVector = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f) * 10f;
	}

	public void Create(string value, TextPopupType type)
	{
		string textValue = "";

		switch (type)
		{
			case TextPopupType.GoldCoin:
				textValue = String.Format($"+ {value} <sprite index=0>");
				_textMesh.color = new Color(1, 0.8470589f, 0.08627451f);
				_textMesh.fontSize = 5;
				break;
			case TextPopupType.Damage:
				textValue = value;
				_textMesh.color = new Color(0.8207547f, 0, 0.007291546f);
				_textMesh.fontSize = 7.5f;
				break;
		}
		
		Setup(textValue);
	}

	private void Update()
	{
		transform.position += _moveVector * Time.deltaTime;
		_moveVector -= 8f * Time.deltaTime * _moveVector;

		if(_disappearTimer > DisappearTimeMax * 0.5f)
		{
			float increaseScaleAmount = 1f;
			transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
		}
		else
		{
			float decreaseScaleAmount = 1f;
			transform.localScale -= decreaseScaleAmount * Time.deltaTime * Vector3.one;
		}
		_disappearTimer -= Time.deltaTime;
		if(_disappearTimer < 0)
		{
			float disappearSpeed = 3f;
			_textColor.a -= disappearSpeed * Time.deltaTime;
			_textMesh.color = _textColor;

			if(_textColor.a < 0)
			{
				Singleton<PoolTextPopup>.instance.RemoveObjectToPool(gameObject, _textMesh);
			}
		}
	}
}