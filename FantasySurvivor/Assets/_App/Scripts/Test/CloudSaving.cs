using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;

public class CloudSaving : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await SignInAnonymouslyAsync();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    async Task SignInAnonymouslyAsync()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            Debug.Log("Sign in anonymously succeeded!");
        
            // Shows how to get the playerID
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}"); 

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            Debug.LogException(ex);
        }
    }

    public static async void SaveSomeData<T>(T inData, string key)
    {
        var data = new Dictionary<string, object> {{key, inData}};
        await CloudSaveService.Instance.Data.Player.SaveAsync(data);
    }

    public static async Task<T> LoadSomeData<T>(string key)
    {
        Dictionary<string, string> savedData =
            await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {key});

        await Task.Delay(20000);

        var dataString = savedData[key];

        var data = JsonUtility.FromJson<T>(dataString);
        
        Debug.Log("Done load");
        return data;
    }
}
