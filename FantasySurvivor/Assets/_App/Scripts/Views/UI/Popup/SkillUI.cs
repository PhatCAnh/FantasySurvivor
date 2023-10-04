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

	[SerializeField] private Image _imgSkill;

	[SerializeField] private TextMeshProUGUI _txtNameSkill;

	[SerializeField] private TextMeshProUGUI _txtDescription;

	private ChoiceSkill _parent;
	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		buttonInteract.onClick.AddListener(SelectedSkill);
		_imgSkill.sprite = skillData.imgUI;
		_txtNameSkill.text = skillData.skillName;
		_txtDescription.text = skillData.description;
	}

	public void Init(SkillData data, ChoiceSkill parent)
	{
		skillData = data;
		_parent = parent;
	}

	private void SelectedSkill()
	{
		System.Type componentType = System.Type.GetType(skillData.type.ToString());
		gameController.character.AddProactiveSkill(componentType);
		_parent.Selected(this);
	}
}