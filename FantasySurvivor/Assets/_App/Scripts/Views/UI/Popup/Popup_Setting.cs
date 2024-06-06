using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupSetting : MonoBehaviour
{
    public GameObject settingsPopupPrefab;
    public Button openSettingsButton;
    private GameObject instantiatedPopup;

    void Start()
    {
        // Ensure the button and prefab are assigned
        if (openSettingsButton != null && settingsPopupPrefab != null)
        {
            // Add a listener to the button to call the OpenSettingsPopup method when clicked
            openSettingsButton.onClick.AddListener(OpenSettingsPopup);
        }
        else
        {
            Debug.LogError("Please assign the button and the settings popup prefab in the inspector.");
        }
    }

    // Method to instantiate the settings popup
    void OpenSettingsPopup()
    {
        // Check if a popup is already instantiated
        if (instantiatedPopup == null)
        {
            // Instantiate the settings popup prefab
            instantiatedPopup = Instantiate(settingsPopupPrefab, transform);
        }
        else
        {
            Debug.Log("Popup is already open.");
        }
    }
}
