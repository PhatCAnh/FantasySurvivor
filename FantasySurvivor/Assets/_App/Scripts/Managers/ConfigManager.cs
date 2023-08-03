using ArbanFramework.Config;
using MR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ConfigManager : ConfigManagerBase
{
    //Configs
    public ConstConfigTable constConfig;

    //private bool _isCanLoadRemoteConfig = false;
    public void Init()
    {
        //Register
        Register(out constConfig);
        LoadConfigs();
  }

    //public Task FetchDataAsync()
    //{
    //    Debug.Log("Fetching data...");
    //    System.Threading.Tasks.Task fetchTask =
    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
    //        TimeSpan.Zero);
    //    return fetchTask.ContinueWith(FetchComplete);
    //}

    //void FetchComplete(Task fetchTask)
    //{
    //    if (fetchTask.IsCanceled)
    //    {
    //        Debug.Log("Fetch canceled.");
    //    }
    //    else if (fetchTask.IsFaulted)
    //    {
    //        Debug.Log("Fetch encountered an error.");
    //    }
    //    else if (fetchTask.IsCompleted)
    //    {
    //        Debug.Log("Fetch completed successfully!");
    //    }

    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:
    //            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
    //            .ContinueWith(task =>
    //            {
    //                Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
    //                               info.FetchTime));

    //                _isCanLoadRemoteConfig = true;
    //            });

    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    Debug.Log("Fetch failed for unknown reason");
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            Debug.Log("Latest Fetch call still pending.");
    //            break;
    //    }
    //}

    //public void LoadFromRemoteConfig()
    //{
    //    if (!_isCanLoadRemoteConfig)
    //        return;

    //    var valueDic = FirebaseRemoteConfig.DefaultInstance.AllValues;
    //    if(valueDic.TryGetValue(constConfig.FileName, out ConfigValue constConfigText))
    //    {
    //        constConfig.Load(constConfigText.StringValue);
    //    }

    //    if (valueDic.TryGetValue(GameConst.GetNameWithVersion(orderConfig.FileName), out ConfigValue orderConfigText))
    //    {
    //        orderConfig.Load(orderConfigText.StringValue);
    //    }

    //    _isCanLoadRemoteConfig = false;
    //}
}
