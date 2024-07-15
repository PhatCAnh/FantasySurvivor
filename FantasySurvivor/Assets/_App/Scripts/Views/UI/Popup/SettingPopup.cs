using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button closeButton;
    public GameObject userIdPopupPrefab;
    public GameObject languagePopupPrefab;
    public GameObject soundsPopupPrefab;
    public GameObject ratePopupPrefab;
    public GameObject aboutUsPopupPrefab;
    public GameObject exitGamePopupPrefab;

    public Button userIdButton;
    public Button languageButton;
    public Button soundsButton;
    public Button rateButton;
    public Button aboutUsButton;
    public Button logoutButton;
    public Button exitGameButton;

    private GameObject currentPopup;

    void Start()
    {
        userIdButton.onClick.AddListener(OpenUserIdPopup);
        languageButton.onClick.AddListener(OpenLanguagePopup);
        soundsButton.onClick.AddListener(OpenSoundsPopup);
        rateButton.onClick.AddListener(OpenRatePopup);
        aboutUsButton.onClick.AddListener(OpenAboutUsPopup);
        logoutButton.onClick.AddListener(OnClickBtnLogout);
        exitGameButton.onClick.AddListener(OpenExitGamePopup);
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Destroy(gameObject);
    }

    void OpenUserIdPopup()
    {
        OpenPopup(userIdPopupPrefab);
    }

    void OpenLanguagePopup()
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
    }

    void OnClickBtnLogout()
    {
        app.models.dataPlayerModel.Logout();
        app.resourceManager.CloseAllPopup();
        Destroy(gameObject);
        app.resourceManager.ShowPopup(PopupType.AccountPopup);
    }

    void OpenExitGamePopup()
    {
        OpenPopup(exitGamePopupPrefab);
    }

    void OpenPopup(GameObject popupPrefab)
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }

        currentPopup = Instantiate(popupPrefab, transform);
    }

    public void Open()
    {
    }

    public void Close()
    {
    }
}