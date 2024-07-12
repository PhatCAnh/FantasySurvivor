using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework.MVC;
using Newtonsoft.Json.Serialization;
using PlayFab;
using PlayFab.ClientModels;
using Unity.VisualScripting;
using UnityEngine;

public class PlayfabController : Controller<GameApp>
{
    private void Awake()
    {
        ArbanFramework.Singleton<PlayfabController>.Set(this);
    }
		
    protected override void OnDestroy()
    {
        base.OnDestroy();
        ArbanFramework.Singleton<PlayfabController>.Unset(this);
    }

    public void Register(string email, string password, Action<RegisterPlayFabUserResult> result, Action<PlayFabError> error)
    {
        if(password.Length < 6)
        {
            return;
        }
        
        var request = new RegisterPlayFabUserRequest()
        {
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, result, error);
    }

    public void Login(string email, string password, Action<LoginResult> result, Action<PlayFabError> error)
    {
        var request = new LoginWithEmailAddressRequest()
        {
            Email = email,
            Password = password,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, result, error);
    }
}
