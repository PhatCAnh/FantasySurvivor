using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_ClickBullet : View<GameApp>, IPopup
{
	[SerializeField] private Image _background;

	[SerializeField] private Button _btnContinue;

	[SerializeField] private Transform _popup;

	[SerializeField] private Transform _continue;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		Singleton<GameController>.instance.isStopGame = true;
		_btnContinue.onClick.AddListener(Close);
		Open();
	}

	public void Open()
	{
	

		_popup.localScale = Vector3.zero;
		_background.color = Color.clear;

		_background.DOColor(Color.white, 0.5f);
		
		_continue.DOScale(Vector3.one * 1.25f, 1.5f)
			.SetLoops(-1, LoopType.Yoyo);
		_popup.DOScale(Vector3.one, 0.75f).SetEase(Ease.OutBack);

	}
	public void Close()
	{
		_background.DOColor(Color.clear, 0.5f);
		_popup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() =>
		{
			Singleton<GameController>.instance.isStopGame = false;
			app.models.dataPlayerModel.firstSeeBulletInteract = false;
			Destroy(gameObject);
		});
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_popup.DOKill();
		_continue.DOKill();
		_background.DOKill();
	}
}