using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Controllers;
using ArbanFramework.MVC;
using FantasySurvivor;
using Newtonsoft.Json;
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

    public void Register(string email, string password, Action<RegisterPlayFabUserResult> result,
        Action<PlayFabError> error)
    {
        if (password.Length < 6)
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

    public void SaveData(Action<UpdateUserDataResult> result)
    {
        var request = new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>()
            {
                { GetKey(app.models.dataPlayerModel), PlayerPrefs.GetString(GetKey(app.models.dataPlayerModel), "") }
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result,
            (error) => { });
    }

    public void GetData(Action<GetUserDataResult> action)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            OnDataReceived(result);
            action(result);
        }, (error) => { });
    }

    public void SubmitNameButton(string name)
    {
        var popupLoading = app.resourceManager.ShowPopup(PopupType.LoadingPopup);
        popupLoading.GetComponent<LoadingPopup>().Init();

        var request = new UpdateUserTitleDisplayNameRequest()
        {
            DisplayName = name,
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            (result) =>
        {
            OnDisplayNameUpdateSuccessful(result, name);
            Destroy(popupLoading);
        },
            (error) => { });
    }

    private void OnDisplayNameUpdateSuccessful(UpdateUserTitleDisplayNameResult result, string name)
    {
        app.models.dataPlayerModel.NameDisplay = name;
        SaveData((action) => { });
    }

    private void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("Received data");
        if (result.Data != null)
        {
            app.models.dataPlayerModel =
                JsonConvert.DeserializeObject<DataPlayerModel>(result.Data[GetKey(app.models.dataPlayerModel)].Value);
        }
    }

    private string GetKey(ModelBase modelBase)
    {
        return string.Format("PYD_MVC_{0}", modelBase.GetType().Name.ToUpper());
    }
}