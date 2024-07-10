using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissiosPopup : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    private GameObject currentPopup;

    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        Destroy(gameObject);
    }
}
