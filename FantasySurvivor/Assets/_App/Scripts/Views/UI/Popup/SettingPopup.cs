using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnUserInfo,_btnLogout,_btnExit, _btnClose, _btnDimer, _btnSound, _btnAbout, _btnRate, _btnLanguage;

    [SerializeField] private Transform _goMainContent;

 

    protected override void OnViewInit()
    {
        _btnUserInfo.onClick.AddListener(OnClickBtnUserInfo);
        _btnLogout.onClick.AddListener(OnClickBtnLogout);
        _btnExit.onClick.AddListener(OnClickBtnExit);
        _btnClose.onClick.AddListener(Close);
        _btnDimer.onClick.AddListener(Close);
        _btnSound.onClick.AddListener(OnClickBtnSound); 
        _btnAbout.onClick.AddListener(OnClickBtnAbout); 
        _btnRate.onClick.AddListener(OnClickBtnRate);
        _btnLanguage.onClick.AddListener(OnClickBtnlanguage);
        Open();


       
    }

    void OnClickBtnUserInfo()
    {
        app.resourceManager.ShowPopup(PopupType.UserInfo);
        Destroy(gameObject);
    }

    void OnClickBtnSound()
    {
        app.resourceManager.ShowPopup(PopupType.SoundPopup);
        Destroy(gameObject);
    }

    void OnClickBtnAbout()
    {
        app.resourceManager.ShowPopup(PopupType.AboutUsPopup);
        Destroy(gameObject);
    }

    void OnClickBtnRate()
    {
        app.resourceManager.ShowPopup(PopupType.RatePopup);
        Destroy(gameObject);
    }
    void OnClickBtnlanguage()
    {
        app.resourceManager.ShowPopup(PopupType.LanguagePopup);
        Destroy(gameObject);
    }
    void OnClickBtnLogout()
    {
        var go = app.resourceManager.ShowPopup(PopupType.ConfirmPopup).GetComponent<ConfirmPopup>();
        go.Init("You want to logout?", () =>
        {
            app.models.dataPlayerModel.Logout();
            app.resourceManager.CloseAllPopup();
            Destroy(gameObject);
            app.resourceManager.ShowPopup(PopupType.AccountPopup);
        });
    }

    void OnClickBtnExit()
    {
        var go = app.resourceManager.ShowPopup(PopupType.ConfirmPopup).GetComponent<ConfirmPopup>();
        go.Init("Do you want to Exit game?", () =>
        {
            Application.Quit();
#if UNITY_EDITOR
            // Chỉ dùng cho Unity Editor để thoát play mode
            UnityEditor.EditorApplication.isPlaying = false;
#endif
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
            .OnComplete(() => { Destroy(gameObject); });
    }
}