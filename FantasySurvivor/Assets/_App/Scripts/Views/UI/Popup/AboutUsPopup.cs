using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutUsPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject aboutusPopupPrefab;
    private GameObject currentPopup;

    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Destroy(gameObject);
    }

    public void OpenSoundsPopup()
    {
        // Destroy the current popup if it exists
        if (currentPopup != null)
        {
            Destroy(currentPopup);
        }

        // Instantiate and store the sounds popup
        currentPopup = Instantiate(aboutusPopupPrefab, transform);
    }

    private void OnSoundsButtonClick()
    {
        OpenSoundsPopup();
        Debug.Log("Sounds button clicked");
    }
}
