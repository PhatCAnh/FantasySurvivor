using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnUserInfo,_btnLogout,_btnExit, _btnClose, _btnDimer;

    [SerializeField] private Transform _goMainContent;

    /*public Button languageButton;
    public Button soundsButton;
    public Button rateButton;
    public Button aboutUsButton;*/
    
    /*public GameObject languagePopupPrefab;
    public GameObject soundsPopupPrefab;
    public GameObject ratePopupPrefab;
    public GameObject aboutUsPopupPrefab;
    public GameObject exitGamePopupPrefab;*/

    protected override void OnViewInit()
    {
        _btnUserInfo.onClick.AddListener(OnClickBtnUserInfo);
        _btnLogout.onClick.AddListener(OnClickBtnLogout);
        _btnExit.onClick.AddListener(OnClickBtnExit);
        _btnClose.onClick.AddListener(Close);
        _btnDimer.onClick.AddListener(Close);
        
        Open();


        /*languageButton.onClick.AddListener(OpenLanguagePopup);
        soundsButton.onClick.AddListener(OpenSoundsPopup);
        rateButton.onClick.AddListener(OpenRatePopup);
        aboutUsButton.onClick.AddListener(OpenAboutUsPopup);
        closeButton.onClick.AddListener(OnCloseButtonClick);*/
    }

    void OnClickBtnUserInfo()
    {
        app.resourceManager.ShowPopup(PopupType.UserInfo);
        Destroy(gameObject);
    }

    /*void OpenLanguagePopup()
    {
        OpenPopup(languagePopupPrefab);
    }

    void OpenSoundsPopup()
    {
        OpenPopup(soundsPopupPrefab);
    }

    void OpenRatePopup()
    {
        OpenPopup(ratePopupPrefab);
    }

    void OpenAboutUsPopup()
    {
        OpenPopup(aboutUsPopupPrefab);
    }*/

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