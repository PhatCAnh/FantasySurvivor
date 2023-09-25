using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Pool;
using ArbanFramework;
using ArbanFramework.MVC;
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

	private GameController gameController => Singleton<GameController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		Singleton<PoolGemExp>.instance.onSpawnGemExp += OnMonsterDie;
		ClickFollowTutorial();
	}

	private void OnMonsterDie(GemExp gemExp)
	{
		Instantiate(_hand, gemExp.transform.position, Quaternion.identity);
		_numberHand++;
		if(_numberHand >= 4)
		{
			Singleton<PoolGemExp>.instance.onSpawnGemExp -= OnMonsterDie;
		}
	}

	private void ClickFollowTutorial()
	{
		_handUi[Mathf.Max(_numberClickUI - 1, 0)].SetActive(false);
		if(_numberClickUI >= _handUi.Length)
		{
			_tglChangeState.onValueChanged.RemoveListener(OnClickToggle);
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