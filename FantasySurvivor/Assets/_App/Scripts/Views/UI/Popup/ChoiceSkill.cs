using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSkill : View<GameApp>, IPopup
{
	public int skillNumber = 3;

	[SerializeField] private GameObject _slotSkillContainer;

	[SerializeField] private SkillUI _slotSKill;
	public ToggleGroup group;

	private SkillUI[] _listSkillUI;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		_listSkillUI = new SkillUI[skillNumber];
		// for(int i = 0; i < skillNumber; i++)
		// {
			var slot = Instantiate(_slotSKill, _slotSkillContainer.transform);
			slot.Init(app.resourceManager.GetSkill(SkillType.SharkSkill), this);
			_listSkillUI[0] = slot;
			
			var slot2 = Instantiate(_slotSKill, _slotSkillContainer.transform);
			slot2.Init(app.resourceManager.GetSkill(SkillType.FireBallSkill), this);
			_listSkillUI[1] = slot2;
			
			var slot3 = Instantiate(_slotSKill, _slotSkillContainer.transform);
			slot3.Init(app.resourceManager.GetSkill(SkillType.TwinSkill), this);
			_listSkillUI[2] = slot3;
		//}
		Open();
	}

	public void Open()
	{
		foreach(var slot in _listSkillUI)
		{
			slot.objectParent.position = new Vector3(0, 3000);
		}

		Sequence sequence = DOTween.Sequence();
		sequence.Join(_listSkillUI[0].objectParent.DOLocalMove(Vector3.zero, 0.75f).SetEase(Ease.OutBack));

		for(int i = 1; i < _listSkillUI.Length; i++)
		{
			sequence.Join(_listSkillUI[i].objectParent
				.DOLocalMove(Vector3.zero, 0.75f)
				.SetEase(Ease.OutBack)
				.SetDelay(0.1f));
		}
	}

	public void Selected(SkillUI skillUI)
	{
		Sequence sequence = DOTween.Sequence();
		
		sequence
			.Join(
				skillUI.objectChain
					.DOLocalMove(new Vector3(0, 3000), 0.75f)
					.SetEase(Ease.Linear))
			.Join(
				skillUI.objectContent
					.DOLocalMove(new Vector3(0, -3000), 1.25f)
					.SetEase(Ease.Linear));
		
		foreach(var skill in _listSkillUI)
		{
			if(!skill.Equals(skillUI))
			{
				sequence.Join(
					skill.objectParent
						.DOLocalMove(new Vector3(0, 3000), 1f)
						.SetEase(Ease.InBack)
						.SetDelay(0.1f));
			}
		}
		sequence.OnComplete(Close);
	}

	public void Close()
	{
		Destroy(gameObject);
	}
}