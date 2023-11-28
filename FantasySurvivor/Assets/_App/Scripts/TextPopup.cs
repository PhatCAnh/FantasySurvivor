using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using FantasySurvivor;
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
		_moveVector = new Vector3(0, Random.Range(0, 3f), 0f) * 10f;
	}

	public void Create(string value, TextPopupType type, bool isCritical = false)
	{
		string textValue = "";

		switch (type)
		{
			case TextPopupType.TowerDamage:
				textValue = value;
				_textMesh.color = Color.white;
				_textMesh.fontSize = 5;


				if(isCritical)
				{
					textValue = $"{value} <sprite=6>";
					_textMesh.color = new Color(1, 0.8f, 0);
					_textMesh.fontSize *= 1.5f;
				}
				break;
			case TextPopupType.MonsterDamage:
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
				Singleton<PoolController>.instance.ReturnObject(ItemPrefab.TextPopup, gameObject);
			}
		}
	}

	private void OnDisable()
	{
		_textMesh.color = new Color(_textMesh.color.r, _textMesh.color.g, _textMesh.color.b, 1f);
		_textMesh.transform.localScale = Vector3.one;
	}
}