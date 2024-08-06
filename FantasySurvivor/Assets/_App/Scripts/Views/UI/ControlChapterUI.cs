using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlChapterUI : View<GameApp>, IPopup
{
	[SerializeField] private TextMeshProUGUI _titleLvl1, _titleLvl2;

	[SerializeField] private Transform _containerLevelUI1, _containerLevelUI2;

	[SerializeField] private Button _btnBack;
	protected override void OnViewInit()
	{
		base.OnViewInit();

		InitChapterUI(_containerLevelUI1, 1, _titleLvl1);
		InitChapterUI(_containerLevelUI2, 2, _titleLvl2);
		
	_btnBack.onClick.AddListener(Close);
	}

	private void InitChapterUI(Transform parent, int chapter, TextMeshProUGUI title)
	{
		title.text = "CHAPTER " + chapter;
		for(int i = 0; i < parent.childCount; i++)
		{
			int level = i + 1;
			parent.GetChild(i).GetComponent<ChapterUI>().Init(chapter, level, gameObject, (chapter - 1) * 3 + level - 1 <= app.models.dataPlayerModel.DataLevelPlayed);
		}
	}
	
	//(chapter - 1) * 3 + level

	public void Open()
	{
		
	}

	public void Close()
	{
		Destroy(gameObject);
	}
}