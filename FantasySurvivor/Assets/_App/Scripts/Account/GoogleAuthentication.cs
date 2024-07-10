using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GoogleAuthentication : MonoBehaviour
{
	public string username, email;

	public TextMeshProUGUI txtUsername, txtEmail;

	public GameObject loginPanel, profilePanel;

	public string webClientId = "882490805819-nvc4j07i2hhdq786b45vs4llifckhnt9.apps.googleusercontent.com";

	private GoogleSignInConfiguration configuration;

	// Defer the configuration creation until Awake so the web Client ID
	// Can be set via the property inspector in the Editor.
	void Awake()
	{
		Debug.Log("Dang khoi tao");
		configuration = new GoogleSignInConfiguration
		{
			WebClientId = webClientId,
			RequestIdToken = true
		};
		Debug.Log("Da khoi tao" + configuration.IsUnityNull());
	}

	public void OnSignIn()
	{
		Debug.Log("da nhan signin");
		
		GoogleSignIn.Configuration = configuration;
		GoogleSignIn.Configuration.UseGameSignIn = false;
		GoogleSignIn.Configuration.RequestIdToken = true;
		GoogleSignIn.Configuration.RequestEmail = true;

		GoogleSignIn.DefaultInstance.SignIn().ContinueWith(
			OnAuthenticationFinished, TaskScheduler.Default);
	}

	internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
	{
		Debug.Log("Dang cho ket qua");
		if(task.IsFaulted)
		{
			using (IEnumerator<System.Exception> enumerator =
			       task.Exception.InnerExceptions.GetEnumerator())
			{
				if(enumerator.MoveNext())
				{
					GoogleSignIn.SignInException error =
						(GoogleSignIn.SignInException) enumerator.Current;
					Debug.LogError("Got Error: " + error.Status + " " + error.Message);
				}
				else
				{
					Debug.LogError("Got Unexpected Exception?!?" + task.Exception);
				}
			}
		}
		else if(task.IsCanceled)
		{
			Debug.LogError("Canceled");
		}
		else
		{
			Debug.Log(("Welcome: " + task.Result.DisplayName + "!"));

			txtUsername.text = "" + task.Result.DisplayName;
			txtEmail.text = "" + task.Result.Email;
			
			loginPanel.SetActive(false);
			profilePanel.SetActive(true);
		}
		Debug.Log("ket qua: " + task);
	}
	
	public void OnSignOut()
	{

		txtUsername.text = "";
		txtEmail.text = "";
			
		loginPanel.SetActive(true);
		profilePanel.SetActive(false);
		
		Debug.Log("Calling SignOut");
		GoogleSignIn.DefaultInstance.SignOut();
	}
}