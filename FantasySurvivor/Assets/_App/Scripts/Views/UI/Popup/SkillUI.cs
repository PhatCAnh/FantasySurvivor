using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SkillUI : View<GameApp>
{
	public SkillData skillData;

	public Transform objectParent, objectChain, objectContent;

	[SerializeField] public Button buttonInteract;

	[SerializeField] private Image _imgSkill, _imgBackgroundIcon;

	[SerializeField] private TextMeshProUGUI _txtNameSkill;

	[SerializeField] private TextMeshProUGUI _txtDescription;
	
	[SerializeField] private Toggle[] _arrStarUI;

	[SerializeField] private GameObject _fxBlue, _fxRed, _fxGreen, _fxPurple, _fxYellow, _fxWhite;
	[SerializeField] private Color _colorBlue, _colorRed, _colorGreen, _colorPurple, _colorYellow, _colorWhite;
	
	[SerializeField] private GameObject _normalStar, _bigStar;

	private ChoiceSkill _parent;
	private int _level;

	
	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		buttonInteract.onClick.AddListener(SelectedSkill);
		_imgSkill.sprite = skillData.imgUI;
		_txtNameSkill.text = skillData.name.ToString();
		var skill = gameController.character.GetSkill(skillData.name);
		if(skill != null)
		{
			_level = skill.level;
		}
		
		_txtDescription.text = skillData.levelSkillData[_level + 1].description;

		switch (skillData.typeElemental)
		{
			case SkillElementalType.Water:
				UpdateColor(_colorBlue);

				if(_level >= 5)
				{
					_fxBlue.SetActive(true);
				}
				
				break;
			case SkillElementalType.Fire:
				UpdateColor(_colorRed);
				
				if(_level >= 5)
				{
					_fxRed.SetActive(true);
				}
				break;
			case SkillElementalType.Wind:
				UpdateColor(_colorGreen);
				
				if(_level >= 5)
				{
					_fxGreen.SetActive(true);
				}
				break;
			case SkillElementalType.Dark:
				
				UpdateColor(_colorPurple);
				
				if(_level >= 5)
				{
					_fxPurple.SetActive(true);
				}
				break;
			case SkillElementalType.Electric:
				
				UpdateColor(_colorYellow);
				
				if(_level >= 5)
				{
					_fxYellow.SetActive(true);
				}
				break;
			case SkillElementalType.Light:
				
				UpdateColor(_colorWhite);
				
				if(_level >= 5)
				{
					_fxWhite.SetActive(true);
				}
				break;
		}
		
		if(_level <= 4)
		{
			for(int i = 0; i < _level + 1; i++)
			{
				_arrStarUI[i].isOn = false;
			}
					
			_arrStarUI[_level].transform
				.DOScale(Vector3.one * 1.25f, 0.5f)
				.SetLoops(-1, LoopType.Yoyo);
		}
		else
		{
			_normalStar.SetActive(false);
			_bigStar.SetActive(true);
			_bigStar.transform
				.DOScale(Vector3.one * 1.25f, 0.5f)
				.SetLoops(-1, LoopType.Yoyo);
		}
	}
	
	public void Init(SkillData data, ChoiceSkill parent)
	{
		skillData = data;
		_parent = parent;
	}

	private void SelectedSkill()
	{
		gameController.character.AddProactiveSkill(skillData);
		_parent.Selected(this);
	}

	private void UpdateColor(Color color)
	{
		_txtNameSkill.color = color;
		_imgBackgroundIcon.color = color;
	}
}