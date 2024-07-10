using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class GoogleIntegration : MonoBehaviour
{
    public TextMeshProUGUI txtNoti, txtName, txtId;

    public string Token;
    public string Error;

    void Start()
    {
        //Initialize PlayGamesPlatform
        UnityServices.InitializeAsync();
        PlayGamesPlatform.Activate();
        LoginGooglePlayGames();
    }

    public async void LoginGooglePlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate((success) =>
        {
            if (success == SignInStatus.Success)
            {
                txtNoti.text = ("Login with Google Play games successful.");

                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    txtNoti.text =("Authorization code: " + code);
                    Token = code;
// This token serves as an example to be used for SignInWithGooglePlayGames
                });
            }
            else if (success == SignInStatus.Canceled)
            {
                Error = "Failed to retrieve Google play games authorization code";
                txtNoti.text =("Login Unsuccessful");
            }else if (success == SignInStatus.InternalError)
            {
                Error = "Failed to retrieve Google play games authorization code";
                txtNoti.text =("Login InternalError");
            }
        });

        await SignInWithGooglePlayGamesAsync(Token);
    }
    
    async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            txtNoti.text =("SignIn is successful.");
            txtId.text = AuthenticationService.Instance.PlayerId;
            txtName.text = AuthenticationService.Instance.PlayerName;
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            txtNoti.text = ex.ToString();
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            txtNoti.text = ex.ToString();
        }
    }
}
