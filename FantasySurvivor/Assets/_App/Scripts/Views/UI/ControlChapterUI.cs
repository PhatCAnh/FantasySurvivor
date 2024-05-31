using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ControlChapterUI : View<GameApp>
{
	[SerializeField] private TextMeshProUGUI _titleLvl1, _titleLvl2, _titleLvl3;

	[SerializeField] private Transform _containerLevelUI1, _containerLevelUI2, _containerLevelUI3;
	protected override void OnViewInit()
	{
		base.OnViewInit();

		InitChapterUI(_containerLevelUI1, 1, _titleLvl1);
		InitChapterUI(_containerLevelUI2, 2, _titleLvl2);
		InitChapterUI(_containerLevelUI3, 3, _titleLvl3);
	}

	private void InitChapterUI(Transform parent, int chapter, TextMeshProUGUI title)
	{
		title.text = "chua set title ne";
		for(int i = 0; i < parent.childCount; i++)
		{
			parent.GetChild(i).GetComponent<ChapterUI>().Init(chapter, i + 1);
		}
	}
}