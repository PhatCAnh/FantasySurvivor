using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelTutorial : View<GameApp>
{
	[SerializeField] private GameObject _hand;

	[SerializeField] private GameObject[] _handUi;

	[SerializeField] private Button _buttonAd;

	[SerializeField] private Toggle _tglChangeState;

	private int _numberHand = 0;

	private int _numberClickUI = 0;

	private DataPlayerModel _dataPlayer => app.models.dataPlayerModel;

	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();

		if(_dataPlayer.firstTouchHand)
		{
			Singleton<PoolGemExp>.instance.onSpawnGemExp += OnMonsterDie;
		}
		if(_dataPlayer.firstTutorialHandUi)
		{
			ClickFollowTutorial();
			_handUi[0].SetActive(true);
		}
	}

	private void OnMonsterDie(GemExp gemExp)
	{
		Instantiate(_hand, gemExp.transform.position, Quaternion.identity);
		_numberHand++;
		if(_numberHand >= 4)
		{
			Singleton<PoolGemExp>.instance.onSpawnGemExp -= OnMonsterDie;
			_dataPlayer.firstTouchHand = false;
			app.models.WriteModel<DataPlayerModel>();
		}
	}

	private void ClickFollowTutorial()
	{
		_handUi[Mathf.Max(_numberClickUI - 1, 0)].SetActive(false);
		if(_numberClickUI >= _handUi.Length)
		{
			_tglChangeState.onValueChanged.RemoveListener(OnClickToggle);
			_dataPlayer.firstTutorialHandUi = false;
			app.models.WriteModel<DataPlayerModel>();
			return;
		}
		_handUi[_numberClickUI].SetActive(true);
		switch (_numberClickUI)
		{
			case 0:
				_tglChangeState.onValueChanged.AddListener(OnClickToggle);
				break;
			case 1:
				_tglChangeState.onValueChanged.RemoveListener(OnClickToggle);
				_buttonAd.onClick.AddListener(ClickFollowTutorial);
				break;
			case 3:
				_buttonAd.onClick.RemoveListener(ClickFollowTutorial);
				_tglChangeState.onValueChanged.AddListener(OnClickToggle);
				break;
		}
		_numberClickUI++;
	}

	//hàm này đc tạo ra mục đích là 1 hàm cố định để remove listener ở hàm trên
	private void OnClickToggle(bool value)
	{
		ClickFollowTutorial();
	}
}