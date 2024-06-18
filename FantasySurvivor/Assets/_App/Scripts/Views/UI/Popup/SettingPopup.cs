using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    public GameObject userIdPopupPrefab;
    public GameObject languagePopupPrefab;
    public GameObject soundsPopupPrefab;
    public GameObject ratePopupPrefab;
    public GameObject aboutUsPopupPrefab;
    public GameObject logoutPopupPrefab;
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
        if (userIdButton != null)
            userIdButton.onClick.AddListener(OpenUserIdPopup);
        if (languageButton != null)
            languageButton.onClick.AddListener(OpenLanguagePopup);
        if (soundsButton != null)
            soundsButton.onClick.AddListener(OpenSoundsPopup);
        if (rateButton != null)
            rateButton.onClick.AddListener(OpenRatePopup);
        if (aboutUsButton != null)
            aboutUsButton.onClick.AddListener(OpenAboutUsPopup);
        if (logoutButton != null)
            logoutButton.onClick.AddListener(OpenLogoutPopup);
        if (exitGameButton != null)
            exitGameButton.onClick.AddListener(OpenExitGamePopup);
        if (closeButton != null)
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

    void OpenLogoutPopup()
    {
        OpenPopup(logoutPopupPrefab);
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

   

}
