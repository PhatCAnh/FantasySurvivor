using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ChapterUI : View<GameApp>
{
	[SerializeField] private Button _btn;

	[SerializeField] private TextMeshProUGUI _txtLevel;

	[SerializeField] private GameObject _goLock;
		
	private int chapter, level;

	private GameObject _parent;
	
	public void Init(int chapter, int level, GameObject parent, bool isLock)
	{
		this.chapter = chapter;
		this.level = level;
		_parent = parent;
		
		_goLock.SetActive(!isLock);

		_txtLevel.text = "Level " + level;

		if (isLock)
		{
			_btn.onClick.AddListener(OnClickBtn);
		}
	}

	private void OnClickBtn()
	{
		Singleton<GameController>.instance.StartGame(chapter, level);
		Destroy(_parent);
		app.resourceManager.ForceClosePopup(PopupType.MainUI);
	}
}