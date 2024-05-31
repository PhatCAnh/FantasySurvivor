using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;
namespace FantasySurvivor
{
	[Serializable]
	public class DataUiChapter
	{
		public string name;
		public Sprite image;
	}
	
	public class ChoiceMapPopup : View<GameApp>, IPopup
	{
		[SerializeField] private GameObject _title;
		[SerializeField] private TextMeshProUGUI _txtTitle;
		[SerializeField] private Button _btnClose;
		[SerializeField] private GameObject _icon;
		[SerializeField] private Button _leftArrow;
		[SerializeField] private Button _rightArrow;
		[SerializeField] private TextMeshProUGUI _txtChapterName;
		[SerializeField] private Slider _sldLevel;
		
		[SerializeField] private GameObject _textContent;
		[SerializeField] private TextMeshProUGUI _txtPlayed;
		[SerializeField] private TextMeshProUGUI _txtBestWave;
		
		[SerializeField] private Button _btnClaimGift;
		[SerializeField] private Button _btnPlay;

		[SerializeField] private Image _imgChapter;
		[SerializeField] private DataUiChapter[] _uiChapter;

		private int _currentChapter = 1;

		private Sequence _sequence;

		private GameController gameController => ArbanFramework.Singleton<GameController>.instance;

		protected override void OnViewInit()
		{
			base.OnViewInit();
			
			_btnPlay.onClick.AddListener(OnClickBtnPlay);
			_leftArrow.onClick.AddListener(OnClickLeftArrow);
			_rightArrow.onClick.AddListener(OnClickRightArrow);
			
			Open();
		}

		public void Open()
		{
			OpenUI(_currentChapter);
		}
		public void Close()
		{
			Destroy(gameObject);
		}

		private void OnClickLeftArrow()
		{
			_currentChapter--;
			OpenUI(_currentChapter);
		}
		
		private void OnClickRightArrow()
		{
			_currentChapter++;
			OpenUI(_currentChapter);
		}

		private void OpenUI(int chapter)
		{
			_leftArrow.gameObject.SetActive(_currentChapter > 1);
			_rightArrow.gameObject.SetActive(_currentChapter < _uiChapter.Length);

			var chapterIndex = _uiChapter[chapter - 1];
			_txtTitle.text = $"CHAPTER {_currentChapter}";
			_imgChapter.sprite = chapterIndex.image;
			_txtChapterName.text = chapterIndex.name;
			// SetAnimOpen();
		}

		private void OnClickBtnPlay()
		{
			/*var load = SceneManager.LoadSceneAsync(GameConst.nameScene_Game, LoadSceneMode.Single);
			load.completed += o => gameController.StartGame(_currentChapter);
			Close();*/
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
			_leftArrow.transform.localPosition = Vector3.zero;
			_rightArrow.transform.localPosition = Vector3.zero;
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
				.Append(_leftArrow.transform.DOLocalMove(new Vector3(-500, 0), duration))
				.Join(_rightArrow.transform.DOLocalMove(new Vector3(500, 0), duration))
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
