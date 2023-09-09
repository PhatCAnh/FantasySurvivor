using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;
namespace Popup
{
	public class ChoiceMapPopup : View<GameApp>, IPopup
	{
		[Required, SerializeField] private GameObject _title;
		[Required, SerializeField] private TextMeshProUGUI _txtTitle;
		[Required, SerializeField] private Button _btnClose;
		[Required, SerializeField] private GameObject _icon;
		[Required, SerializeField] private GameObject _leftArrow;
		[Required, SerializeField] private GameObject _rightArrow;
		[Required, SerializeField] private TextMeshProUGUI _txtChapterName;
		[Required, SerializeField] private Slider _sldLevel;
		
		[Required, SerializeField] private GameObject _textContent;
		[Required, SerializeField] private TextMeshProUGUI _txtPlayed;
		[Required, SerializeField] private TextMeshProUGUI _txtBestWave;
		
		[Required, SerializeField] private Button _btnClaimGift;
		[Required, SerializeField] private Button _btnPlay;


		private Sequence _sequence;

		private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			
			_btnPlay.onClick.AddListener(OnClickBtnPlay);
			
			
			Open();
		}

		public void Open()
		{
			SetAnimOpen();
		}
		public void Close()
		{
			Destroy(gameObject);
		}

		private void OnClickBtnPlay()
		{
			var load = SceneManager.LoadSceneAsync("scn_Game", LoadSceneMode.Single);
			load.completed += o => gameController.StartGame();
			Close();
		}

		private void SetAnimOpen()
		{
			_sequence = DOTween.Sequence();
			var duration = 0.25f;
			
			_title.transform.localScale = Vector3.zero;
			_btnClose.transform.localScale = Vector3.zero;
			_icon.transform.localScale = Vector3.zero;
			
			_leftArrow.transform.localScale = Vector3.zero;
			_rightArrow.transform.localScale = Vector3.zero;
			_leftArrow.transform.localPosition = new Vector3(0, 200);
			_rightArrow.transform.localPosition = new Vector3(0, 200);
			_txtChapterName.transform.localScale = Vector3.zero;
			_sldLevel.transform.localScale = Vector3.zero;
			_sldLevel.value = 0;
			
			_textContent.transform.localScale = Vector3.zero;
			_btnClaimGift.transform.localScale = Vector3.zero;
			_btnPlay.transform.localScale = Vector3.zero;
			
			
			
			_sequence
				.Append(_title.transform.DOScale(Vector3.one, duration))
				.Append(_btnClose.transform.DOScale(Vector3.one, duration))
				.Append(_icon.transform.DOScale(Vector3.one, duration))
				.Append(_leftArrow.transform.DOLocalMove(new Vector3(-300, 200), duration))
				.Join(_rightArrow.transform.DOLocalMove(new Vector3(300, 200), duration))
				.Join(_leftArrow.transform.DOScale(Vector3.one, duration))
				.Join(_rightArrow.transform.DOScale(Vector3.one, duration))
				.Join(_txtChapterName.transform.DOScale(Vector3.one, duration))
				.Append(_sldLevel.transform.DOScale(Vector3.one, duration).OnComplete(() =>
				{
					DOTween.To(() => _sldLevel.value, value => _sldLevel.value = value, 30, duration * 3);
				}))
				.Append(_textContent.transform.DOScale(Vector3.one, duration))
				.Append(_btnClaimGift.transform.DOScale(Vector3.one, duration))
				.Join(_btnPlay.transform.DOScale(Vector3.one, duration));
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			_sequence.Kill();
		}
	}
}
