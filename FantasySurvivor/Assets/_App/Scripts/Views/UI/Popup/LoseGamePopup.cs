using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MR
{
	public class LoseGamePopup : View<GameApp>, IPopup
	{
		[SerializeField] private Image _background;

		[SerializeField] private Transform _icon;

		[SerializeField] private Transform _txtReward;

		[SerializeField] private Transform _deco1;

		[SerializeField] private Transform _deco2;

		public Transform containerReward;

		[SerializeField] private Transform _txtRevenge;

		[SerializeField] private Button _btnHome;
		[SerializeField] private Button _btnRevenge;
		[SerializeField] private Button _btnWatchAds;

		private GameController _gameController => Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			_btnHome.onClick.AddListener(OnClickBtnHome);
			_btnRevenge.onClick.AddListener(OnClickBtnRevenge);
			_btnWatchAds.onClick.AddListener(OnClickBtnWatchAds);
			Open();
		}

		public void Open()
		{
			_background.color = new Color(1, 1, 1, 0);
			_icon.localScale = Vector3.zero;
			_txtReward.localScale = Vector3.zero;
			_deco1.localPosition = new Vector3(-350, 115, 0);
			_deco2.localPosition = new Vector3(350, 115, 0);
			_txtRevenge.localScale = Vector3.zero;
			_btnRevenge.transform.localScale = Vector3.zero;
			_btnWatchAds.transform.localScale = Vector3.zero;
			_btnHome.transform.localScale = Vector3.zero;

			var sequence = DOTween.Sequence();

			sequence
				.Append(_background.DOColor(Color.white, 0.5f))
				.Append(_icon.DOScale(1, 0.5f).SetEase(Ease.OutBack))
				.Join(_deco1.DOLocalMove(new Vector3(-190, 115, 0), 0.25f))
				.Join(_deco2.DOLocalMove(new Vector3(190, 115, 0), 0.25f))
				.Join(_txtReward.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			for(var i = 0; i < containerReward.childCount; i++)
			{
			    var item = containerReward.GetChild(i);
			    item.localScale = Vector3.zero;
			    sequence.Append(containerReward.GetChild(i).DOScale(1, 0.25f).SetEase(Ease.OutBack));
			}
			sequence.Append(_txtRevenge.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			sequence.Join(_btnRevenge.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			sequence.Join(_btnWatchAds.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			sequence.Join(_btnHome.transform.DOScale(1, 0.25f).SetEase(Ease.OutBack));
			sequence.OnComplete(() =>
			{
			    sequence.Kill();
			});
		}

		public void Close()
		{
			Destroy(gameObject);
		}

		private void OnClickBtnHome()
		{
			_gameController.RestartGame();
		}

		private void OnClickBtnRevenge()
		{

		}

		private void OnClickBtnWatchAds()
		{

		}
	}
}