using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms;
using Unity.Services.Authentication;
using Unity.Services.Authentication.PlayerAccounts;
using Unity.Services.Core;
using UnityEngine;

public class LoginController : MonoBehaviour
{
	public event Action<PlayerInfo, string> OnSignedIn;
	private PlayerInfo _playerInfo;

		async void Awake()
	{
		await UnityServices.InitializeAsync();
		PlayerAccountService.Instance.SignedIn += SingedIn;
	}

	private async void SingedIn()
	{
		try
		{
			var accessToken = PlayerAccountService.Instance.AccessToken;
			await SignInWithUnityAsync(accessToken);
		}
		catch(Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task InitSignIn()
	{
		await PlayerAccountService.Instance.StartSignInAsync();
	}
	async Task SignInWithUnityAsync(string accessToken)
	{
		try
		{
			await AuthenticationService.Instance.SignInWithUnityAsync(accessToken);
			Debug.Log("SignIn is successful.");

			_playerInfo = AuthenticationService.Instance.PlayerInfo;

			var name = await AuthenticationService.Instance.GetPlayerNameAsync();
			
			OnSignedIn?.Invoke(_playerInfo, name);
		}
		catch(AuthenticationException ex)
		{
			// Compare error code to AuthenticationErrorCodes
			// Notify the player with the proper error message
			Debug.LogException(ex);
		}
		catch(RequestFailedException ex)
		{
			// Compare error code to CommonErrorCodes
			// Notify the player with the proper error message
			Debug.LogException(ex);
		}
	}
}