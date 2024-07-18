using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : MonoBehaviour
{
    [SerializeField] private Button loginButton;

    [SerializeField] private TextMeshProUGUI userIdText;

    [SerializeField] private Transform loginPanel, userPanel;

    [SerializeField] private LoginController loginController;

    private void OnEnable()
    {
        loginButton.onClick.AddListener(LoginBtnPressed);
        loginController.OnSignedIn += OnSignedIn;
    }

    private void OnDisable()
    {
	    loginButton.onClick.RemoveListener(LoginBtnPressed);
	    loginController.OnSignedIn -= OnSignedIn;
    }

    private void OnSignedIn(PlayerInfo playerInfo, string playerName)
    {
	    loginPanel.gameObject.SetActive(false);
	    userPanel.gameObject.SetActive(true);
	    userIdText.text = $"id: {playerInfo.Id}";
    }

    private async void LoginBtnPressed()
    {
       await loginController.InitSignIn();
    }
}
        
