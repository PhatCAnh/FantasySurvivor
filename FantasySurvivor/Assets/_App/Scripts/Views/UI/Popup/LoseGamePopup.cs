using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseGamePopup : View<GameApp>, IPopup
{
    [SerializeField] private Image _imageBackGround;
    
    [SerializeField] private GameObject _title;
    
    [SerializeField] private TextMeshProUGUI _txtEnemyKilled, _txtWave;
    
    [SerializeField] private GameObject _leftArrow;
    [SerializeField] private GameObject _rightArrow;
    [SerializeField] private TextMeshProUGUI _txtReward;
    [SerializeField] private Transform _containerReward;
    
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnReward;
    [SerializeField] private Button _btnRevive;  // Thêm nút hồi sinh

    private Sequence _sequence;
    private GameController gameController => Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        foreach(var reward in gameController.map.dictionaryReward)
        {
            gameController.SpawnItemReward(reward.Key, reward.Value, _containerReward);
        }

        _btnHome.onClick.AddListener(OnClickBtnHome);
        
        _btnReward.onClick.AddListener(OnClickBtnReward);

        _btnRevive.onClick.AddListener(OnClickBtnRevive);  // Liên kết sự kiện

        if (gameController.isRevive)
        {
            _btnRevive.gameObject.SetActive(false);
        }

        _txtWave.text = $"Best wave: {gameController.map.model.WaveInGame}";

        _txtEnemyKilled.text = $"Monster killed: {gameController.map.model.monsterKilled}";

        Open();
    }


    public void Open()
    {
        SetAnimOpen();
    }
    public void Close()
    {
        gameController.ResetGame();
        gameController.ChangeSceneHome();
        gameController.isEndGame = false;
        Destroy(gameObject);
    }

    private void OnClickBtnHome()
    {
        gameController.ClearHealthBar();
        foreach (var reward in gameController.map.dictionaryReward)
        {
            gameController.ClaimReward(reward.Key, reward.Value);
        }

        gameController.isRevive = false;
        Close();
    }

    private void OnClickBtnReward()
    {   
        app.adsController.ShowReward(() =>
        {
            foreach(var reward in gameController.map.dictionaryReward)
            {
                gameController.ClaimReward(reward.Key, reward.Value * 2);
            }
            gameController.isRevive = false;
            Close();
        });
    }

    private void OnClickBtnRevive()
    {
        app.adsController.ShowReward(() =>
        {
            gameController.ReviveCharacter();
            gameController.isRevive = true;
            Destroy(gameObject);
        });
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
        _btnRevive.transform.localScale = Vector3.zero;  // Ẩn nút hồi sinh ban đầu


        _sequence
            .Append(_imageBackGround.DOFade(0.9f, duration * 2))
            .Join(_title.transform.DOScale(Vector3.one, duration * 2))
            .Append(_txtEnemyKilled.transform.DOScale(Vector3.one, duration))
            .Append(_txtWave.transform.DOScale(Vector3.one, duration))
            .Append(_txtReward.transform.DOScale(Vector3.one, duration))
            .Join(_leftArrow.transform.DOLocalMove(new Vector3(-470, 0), duration))
            .Join(_rightArrow.transform.DOLocalMove(new Vector3(470, 0), duration))
            .Join(_btnHome.transform.DOScale(Vector3.one, duration))
            .Join(_btnReward.transform.DOScale(Vector3.one, duration))
              .Join(_btnRevive.transform.DOScale(Vector3.one, duration));  // Hiển thị nút hồi sinh

        
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
