using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class GoogleIntegration : MonoBehaviour
{
    public string googlePlayToken;
    public string googlePlayError;

    private async void Start()
    {
        await Authenticate();
    }

    public async Task Authenticate()
    {
        PlayGamesPlatform.Activate();
        await UnityServices.InitializeAsync();
        
        PlayGamesPlatform.Instance.Authenticate((success)
            =>
        {
            if (success == SignInStatus.Success)
            {
                Debug.Log("Login with google was successful.");
                PlayGamesPlatform.Instance.RequestServerSideAccess(true,
                    code =>
                    {
                        Debug.Log($"Auth code is {code}");
                        googlePlayToken = code;
                    });
            }
            else
            {
                googlePlayError = "Failed to retrieve GPG auth code";
                Debug.LogError("Login Failed");
            }
        });

        await AuthenticateWithUnity();
    }

    private async Task AuthenticateWithUnity()
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGoogleAsync(googlePlayToken);
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
            throw;
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
            throw;
        }
    }
}
