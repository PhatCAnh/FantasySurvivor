using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MR
{
	public class WinGamePopup : View<GameApp>, IPopup
	{
		public Transform containerReward;

		[SerializeField] private Image _background;

		//[SerializeField] private Transform _mainContent;

		[SerializeField] private Transform _icon;

		[SerializeField] private Transform _title;

		[SerializeField] private Transform _txtReward;

		[SerializeField] private Transform _deco1;

		[SerializeField] private Transform _deco2;

		[SerializeField] private Button _claimBtn;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			Open();

			_claimBtn.onClick.AddListener(ClaimReward);
		}

		public void Open()
		{
			_background.color = new Color(1, 1, 1, 0);
			_icon.localScale = Vector3.zero;
			_title.localScale = Vector3.zero;
			_txtReward.localScale = Vector3.zero;
			_deco1.localPosition = new Vector3(-350, 0, 0);
			_deco2.localPosition = new Vector3(350, 0, 0);
			_claimBtn.transform.localScale = Vector3.zero;

			var sequence = DOTween.Sequence();

			sequence
				.Append(_background.DOColor(Color.white, 0.5f))
				.Append(_icon.DOScale(1, 0.25f).SetEase(Ease.OutBack))
				.Append(_title.DOScale(1, 0.5f).SetEase(Ease.OutBack))
				.Join(_deco1.DOLocalMove(new Vector3(-190, 0, 0), 0.25f))
				.Join(_deco2.DOLocalMove(new Vector3(190, 0, 0), 0.25f))
				.Join(_txtReward.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			for(var i = 0; i < containerReward.childCount; i++)
			{
				var item = containerReward.GetChild(i);
				item.localScale = Vector3.zero;
				sequence.Append(containerReward.GetChild(i).DOScale(1, 0.25f).SetEase(Ease.OutBack));
			}
			sequence.Append(_claimBtn.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack));
			sequence.OnComplete(() =>
			{
				sequence.Kill();
			});
		}

		public void Close()
		{
			// _mainContent.DOScale(0.5f, 0.25f).SetEase(Ease.InBack).OnComplete(() =>
			// {
			//     
			// });

			Singleton<GameController>.instance.RestartGame();
			Destroy(gameObject);
		}

		private void ClaimReward()
		{
			Close();
		}
	}
}