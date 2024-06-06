using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject soundsPopupPrefab;
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
        currentPopup = Instantiate(soundsPopupPrefab, transform);
    }

    private void OnSoundsButtonClick()
    {
        OpenSoundsPopup();
        Debug.Log("Sounds button clicked");
    }
}
