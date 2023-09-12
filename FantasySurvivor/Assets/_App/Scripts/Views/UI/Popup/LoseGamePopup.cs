using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseGamePopup : View<GameApp>, IPopup
{
    [Required, SerializeField] private Image _imageBackGround;
    
    [Required, SerializeField] private GameObject _title;
    
    [Required, SerializeField] private TextMeshProUGUI _txtEnemyKilled;
    [Required, SerializeField] private TextMeshProUGUI _txtWave;
    
    [Required, SerializeField] private GameObject _leftArrow;
    [Required, SerializeField] private GameObject _rightArrow;
    [Required, SerializeField] private TextMeshProUGUI _txtReward;
    [Required, SerializeField] private Transform _containerReward;
    
    [Required, SerializeField] private Button _btnHome;
    [Required, SerializeField] private Button _btnReward;

    private Sequence _sequence;
    private GameController gameController => Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        _btnHome.onClick.AddListener(OnClickBtnHome);
        
        _btnReward.onClick.AddListener(OnClickBtnReward);
        
        Open();
    }


    public void Open()
    {
        SetAnimOpen();
    }
    public void Close()
    {
        gameController.ChangeSceneHome();
        gameController.isEndGame = false;
        Destroy(gameObject);
    }

    private void OnClickBtnHome()
    {
        Close();
    }

    private void OnClickBtnReward()
    {
        Close();
    }

    private void SetAnimOpen()
    {
        _sequence = DOTween.Sequence();
        var duration = 0.25f;
        _imageBackGround.color = Color.clear;
        _title.transform.localScale = Vector3.zero;
        _txtEnemyKilled.transform.localScale = Vector3.zero;
        _txtWave.transform.localScale = Vector3.zero;
        _txtReward.transform.localScale = Vector3.zero;
        _leftArrow.transform.localPosition = new Vector3(-1000, 0);
        _rightArrow.transform.localPosition = new Vector3(1000, 0);

        for(int i = 0; i < _containerReward.childCount; i++)
        {
            _containerReward.GetChild(i).localScale = Vector3.zero;
        }
        
        _btnHome.transform.localScale = Vector3.zero;
        _btnReward.transform.localScale = Vector3.zero;

        _sequence
            .Append(_imageBackGround.DOFade(0.9f, duration * 2))
            .Join(_title.transform.DOScale(Vector3.one, duration * 2))
            .Append(_txtEnemyKilled.transform.DOScale(Vector3.one, duration))
            .Append(_txtWave.transform.DOScale(Vector3.one, duration))
            .Append(_txtReward.transform.DOScale(Vector3.one, duration))
            .Join(_leftArrow.transform.DOLocalMove(new Vector3(-470, 0), duration))
            .Join(_rightArrow.transform.DOLocalMove(new Vector3(470, 0), duration))
            .Join(_btnHome.transform.DOScale(Vector3.one, duration))
            .Join(_btnReward.transform.DOScale(Vector3.one, duration));
        
        for(int i = 0; i < _containerReward.childCount; i++)
        {
            _sequence.Append(_containerReward.GetChild(i).DOScale(Vector3.one, duration / 2));
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _sequence.Kill();
    }
}
