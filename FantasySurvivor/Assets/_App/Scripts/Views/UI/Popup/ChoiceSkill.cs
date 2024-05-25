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

	// private const int maxUniqueSkills = 4;

	[SerializeField] private GameObject _slotSkillContainer;

	[SerializeField] private SkillUI _slotSKill;
	public ToggleGroup group;

	private SkillUI[] _listSkillUI;
	private List<SkillData> selectedSkills = new List<SkillData>();
	public GameController gameController => Singleton<GameController>.instance;


	protected override void OnViewInit()
	{
		base.OnViewInit();
		var skillData = gameController.map.GetRandomSkill();
		_listSkillUI = new SkillUI[skillData.Count];
		for (int i = 0; i < skillData.Count; i++)
		{
			var slot = Instantiate(_slotSKill, _slotSkillContainer.transform);
			slot.Init(skillData[i], this);
			_listSkillUI[i] = slot;
		}
		Open();
	}



	public void Open()
	{
		gameController.isStopGame = true;

		foreach (var slot in _listSkillUI)
		{
			if (slot == null) return;
			slot.objectParent.position = new Vector3(0, 3000);
		}

		Sequence sequence = DOTween.Sequence();
		sequence.Join(_listSkillUI[0].objectParent.DOLocalMove(Vector3.zero, 0.75f).SetEase(Ease.OutBack));

		for (int i = 1; i < _listSkillUI.Length; i++)
		{
			if (_listSkillUI[i] == null) return;
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

		foreach (var skill in _listSkillUI)
		{
			if (skill == null) return;
			if (!skill.Equals(skillUI))
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
		gameController.isStopGame = false;
		Destroy(gameObject);
	}
}
