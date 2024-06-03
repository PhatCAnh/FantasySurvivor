using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;
public class ChapterUI : View<GameApp>
{
	[SerializeField] private Button _btn;

	[SerializeField] private Transform _stars;

	[SerializeField] private Sprite _starOn, _starOff;

	private Image[] _imgStar;

	private int chapter, level;

	protected override void OnViewInit()
	{
		base.OnViewInit();
        
		_btn.onClick.AddListener(OnClickBtn);

		_imgStar = new Image[_stars.childCount];

		for(int i = 0; i < _stars.childCount; i++)
		{
			_imgStar[i] = _stars.GetChild(i).GetComponent<Image>();
		}
	}

	public void Init(int chapter, int level)
	{
		this.chapter = chapter;
		this.level = level;
	}

	private void OnClickBtn()
	{
		Singleton<GameController>.instance.StartGame(chapter, level);
		app.resourceManager.ForceClosePopup(PopupType.ChoiceMap);
		app.resourceManager.ForceClosePopup(PopupType.MainUI);
	}
}