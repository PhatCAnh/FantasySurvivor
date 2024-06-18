using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatInPopupDescription : View<GameApp>
{
	[SerializeField] private TextMeshProUGUI _txtNameStat, _txtNumber;
	[SerializeField] private Image _img;

	public void Init(string name, int number, Sprite sprite)
	{
		_txtNameStat.text = name;
		_txtNumber.text = $"+ {number}";
		_img.sprite = sprite;
	}
}