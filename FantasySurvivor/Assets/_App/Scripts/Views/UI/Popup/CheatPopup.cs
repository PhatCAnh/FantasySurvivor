using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

public class CheatPopup : View<GameApp>, IPopup
{
	[SerializeField] private Transform container;
	[SerializeField] private GameObject iconSkillPrefab;
	[SerializeField] private Button closeBtn;

	private List<SkillData> _listSkill = new List<SkillData>();

	protected override void OnViewInit()
	{
		base.OnViewInit();
		_listSkill = Singleton<SkillController>.instance.GetListSkillDataTable();

		foreach(var skill in _listSkill)
		{
			Instantiate(iconSkillPrefab, container).TryGetComponent(out IconSkill icon);
			icon.skillData = skill;
		}

		closeBtn.onClick.AddListener(Close);

		Open();
	}

	public void Open()
	{
		transform.localScale = Vector3.zero;
		transform.DOScale(Vector3.one, 0.35f);
	}
	public void Close()
	{
		transform.DOScale(Vector3.zero, 0.35f).OnComplete(() => { Destroy(gameObject); });
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