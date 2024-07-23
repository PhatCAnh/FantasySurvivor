using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupChoiceSkill : View<GameApp>, IPopup
{
	[SerializeField] private TextMeshProUGUI NumberTaget;
	[SerializeField] private Transform container, containerchoose;
	[SerializeField] private GameObject iconSkillPrefab;
	[SerializeField] private Button GoBtn, Backbtn;

	private int currentNumberSkill = 0; // điếm số skill outr game
	private GameController gameController => Singleton<GameController>.instance;
	private SkillController skillController => Singleton<SkillController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		gameController._listSkill = skillController.GetListSkillDataTable();
		foreach(var skill in gameController._listSkill) // lấy list skill
		{
			Instantiate(iconSkillPrefab, container).TryGetComponent(out Icon_ChoiceSkill icon);

			var id = skill.id;
			var result = CheckSkillSet(id);

			if(result) currentNumberSkill += 1;

			icon.Init(skill.id, this, result);
		}
        foreach (var skill in gameController._listSkill)
        {
            var id = skill.id;

            if (CheckSkillSet(id) == true)
            {
                Instantiate(iconSkillPrefab, containerchoose).TryGetComponent(out Icon_ChoiceSkill icon);
                icon.ShowList(skill.id, this);
            }

        }
		Backbtn.onClick.AddListener(Close);
        Open();
		NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}"; //fix text numberskill khi mới vào
		if(currentNumberSkill < gameController.numberLimitChoiceSkill) //ẩn nút go
		{
			GoBtn.interactable = false;
		}
		GoBtn.onClick.AddListener(Close_Go);
		return;
	}
	public void Open()
	{
		transform.localScale = Vector3.zero;
		transform.DOScale(Vector3.one, 0.35f);
	}
	public void Close_Go()
	{
		transform.DOScale(Vector3.zero, 0.35f).OnComplete(() =>
		{
			app.resourceManager.ForceClosePopup(PopupType.MainUI);
			gameController.StartGame(1, 1);

			Destroy(gameObject);
		});

	}
	public void Close()
	{
		transform.DOScale(Vector3.zero, 0.35f).OnComplete(() =>
		{

			Destroy(gameObject);
		});

	}

	//public bool UpdateTextNumberChoiceSkill(SkillId id)
	//{
		//if(CheckSkillSet(id))
		//{
		//	currentNumberSkill -= 1;
		//	NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}";
		//	GoBtn.interactable = false;
           
  //          return false;
  //      }

		//if(currentNumberSkill < gameController.numberLimitChoiceSkill)
		//{
		//	currentNumberSkill += 1;
  //          NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}";

		//	GoBtn.interactable = currentNumberSkill == gameController.numberLimitChoiceSkill;
           
  //          return true;
		//}
       
 //       return false;
	//}

	private bool CheckSkillSet(SkillId id)
	{
		return app.models.dataPlayerModel.SkillSet.Contains(id);
	}
}