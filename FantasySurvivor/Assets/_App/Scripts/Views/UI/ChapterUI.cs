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

	private GameObject _parent;

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

	public void Init(int chapter, int level, GameObject parent)
	{
		this.chapter = chapter;
		this.level = level;
		_parent = parent;
	}

	private void OnClickBtn()
	{
		Singleton<GameController>.instance.StartGame(chapter, level);
		Destroy(_parent);
		app.resourceManager.ForceClosePopup(PopupType.MainUI);
	}
}