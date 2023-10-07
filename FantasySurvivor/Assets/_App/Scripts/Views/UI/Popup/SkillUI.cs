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

	[SerializeField] private GameObject _fxBlue, _fxRed, _fxGreen;
	
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
		_txtDescription.text = skillData.description;

		var skill = gameController.character.GetSkill(skillData.name);
		if(skill != null) _level = skill.level;

		

		switch (skillData.typeElemental)
		{
			case SkillElementalType.Water:
				_txtNameSkill.color = new Color(0.2f, 0.3f, 0.7f);
				_imgBackgroundIcon.color = new Color(0.2f, 0.3f, 0.7f);

				if(_level > 5)
				{
					_fxBlue.SetActive(true);
				}
				
				break;
			case SkillElementalType.Fire:
				_txtNameSkill.color = new Color(0.7f, 0f, 0.1f);
				_imgBackgroundIcon.color = new Color(0.7f, 0f, 0.1f);
				
				if(_level > 5)
				{
					_fxRed.SetActive(true);
				}
				break;
			case SkillElementalType.Wind:
				_txtNameSkill.color = new Color(0, 0.55f, 0.15f);
				_imgBackgroundIcon.color = new Color(0, 0.55f, 0.15f);
				
				if(_level > 5)
				{
					_fxBlue.SetActive(true);
				}
				break;
			case SkillElementalType.Ground:
				
				if(_level > 5)
				{
					_fxGreen.SetActive(true);
				}
				break;
			case SkillElementalType.Electric:
				
				if(_level > 5)
				{
					_fxBlue.SetActive(true);
				}
				break;
			case SkillElementalType.Light:
				
				if(_level > 5)
				{
					_fxBlue.SetActive(true);
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
}