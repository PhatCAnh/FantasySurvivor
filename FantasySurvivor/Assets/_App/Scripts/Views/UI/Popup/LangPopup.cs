using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject languagePopupPrefab; 
    private GameObject currentPopup;

    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Destroy(gameObject);
    }

    public void OpenLanguagePopup() // Thay ??i tên thành OpenLanguagePopup
    {
        // Destroy the current popup if it exists
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }

        // Instantiate and store the language popup
        currentPopup = Instantiate(languagePopupPrefab, transform);
    }

    private void OnLanguageButtonClick() // Thay ??i tên thành OnLanguageButtonClick
    {
        OpenLanguagePopup();
        Debug.Log("Language button clicked");
    }
}
