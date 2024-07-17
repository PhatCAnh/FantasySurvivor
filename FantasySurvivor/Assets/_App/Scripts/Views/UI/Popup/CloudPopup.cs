using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloudPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimer, _btnClose, _btnPush, _btnPull;

    [SerializeField] private TextMeshProUGUI _txtLastedPush;

    [SerializeField] private Transform _goMainContent;

    private PlayfabController playfabController => Singleton<PlayfabController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        _btnDimer.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
        
        _btnPush.onClick.AddListener(OnClickBtnPush);
        _btnPull.onClick.AddListener(OnClickBtnPull);
        
        
        Open();
    }

    public void OnClickBtnPush()
    {
        var go = app.resourceManager.ShowPopup(PopupType.ConfirmPopup).GetComponent<ConfirmPopup>();
        go.Init("Are you sure want to push? Data in cloud will be change.", () =>
        {
            var popupLoading = app.resourceManager.ShowPopup(PopupType.LoadingPopup);
            popupLoading.GetComponent<LoadingPopup>().Init();
            playfabController.SaveData(result => {Destroy(popupLoading);});
            Destroy(go.gameObject);
        });
    }

    public void OnClickBtnPull()
    {
        var go = app.resourceManager.ShowPopup(PopupType.ConfirmPopup).GetComponent<ConfirmPopup>();
        go.Init("Are you sure want to pull? Data in current will be change.", () =>
        {
            var popupLoading = app.resourceManager.ShowPopup(PopupType.LoadingPopup);
            popupLoading.GetComponent<LoadingPopup>().Init();
            playfabController.GetData(result => {Destroy(popupLoading);});
            Destroy(go.gameObject);
        });
    }

    public void Open()
    {
        _goMainContent.localScale = Vector3.zero;

        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
