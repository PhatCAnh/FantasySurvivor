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
	[SerializeField] private Transform container;
	[SerializeField] private GameObject iconSkillPrefab;
	[SerializeField] private Button closeBtn;
	[SerializeField] private Button GoBtn;

	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		gameController._listSkill = app.resourceManager.GetListSkill();
		foreach(var skill in gameController._listSkill) // lấy list skill
		{
			Instantiate(iconSkillPrefab, container).TryGetComponent(out Icon_ChoiceSkill icon);
			icon.Init(skill.id, this);
		}
		closeBtn.onClick.AddListener(Close);
		Open();
		NumberTaget.text = $"{gameController.currentNumberSkill}/{gameController.numberLimitChoiceSkill}"; //fix text numberskill khi mới vào
		if(gameController.currentNumberSkill < gameController.numberLimitChoiceSkill) //ẩn nút go
		{
			GoBtn.interactable = false;
		}
		GoBtn.onClick.AddListener(Close_Go);
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

	public void UpdateTextNumberChoiceSkill(bool value)
	{
		gameController.currentNumberSkill += value ? 1 : -1;

		NumberTaget.text = $"{gameController.currentNumberSkill}/{gameController.numberLimitChoiceSkill}";

		if(gameController.currentNumberSkill < gameController.numberLimitChoiceSkill)
		{

			GoBtn.interactable = false;
		}
		else
		{
			GoBtn.interactable = true;

		}
	}


	// private void OnClickBtnCoin()
	// {
	//     app.models.dataPlayerModel.Coin += 1000;
	// }
	//
	// private void OnClickBtnGem()
	// {
	//     GameConst.gemStartGame += 1000;
	// }
	//
	// private void OnClickBtnDeleteData()
	// {
	//     GameConst.gemStartGame = 100;
	//     PlayerPrefs.DeleteAll();
	//     app.models.dataPlayerModel.InitBaseData();
	//     app.resourceManager.CloseAllPopup();
	//     Singleton<GameController>.instance.ChangeSceneHome();
	// }
}