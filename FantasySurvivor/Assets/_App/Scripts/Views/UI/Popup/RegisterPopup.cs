using ArbanFramework;
using ArbanFramework.MVC;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class RegisterPopup : View<GameApp>, IPopup
{
	

	private PlayfabController playfab => Singleton<PlayfabController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
	}

	

	public void Open()
	{
	}

	public void Close()
	{
		Destroy(gameObject);
	}

}