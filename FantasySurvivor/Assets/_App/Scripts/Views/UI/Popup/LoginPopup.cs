using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LoginPopup : View<GameApp>, IPopup
{
	[SerializeField] private GameObject _goLogin, _goRegister;

	[SerializeField] private TMP_InputField _inputEmail, _inputPassword;
	[SerializeField] private Button _btnLogin, _btnRegister;
	[SerializeField] private TextMeshProUGUI _txtError;

	[SerializeField] private TMP_InputField _inputFieldEmailRegister, _inputFieldPasswordRegister, _inputFieldConfirm;
	[SerializeField] private Button _btnRegisterAccount, _btnBack;
	[SerializeField] private TextMeshProUGUI _txtErrorRegister;

	private PlayfabController playfab => Singleton<PlayfabController>.instance;

	protected override void OnViewInit()
	{
		base.OnViewInit();
		_btnLogin.onClick.AddListener(OnClickBtnLogin);
		_btnRegister.onClick.AddListener(OnClickBtnSignIn);
		_btnRegisterAccount.onClick.AddListener(OnClickBtnRegister);
		Open();
	}

	public void Open()
	{

	}

	public void Close()
	{
		Destroy(gameObject);
	}

	private void OnClickBtnRegister()
	{
		if(_inputFieldPasswordRegister.text.Length < 6)
		{
			_txtErrorRegister.text = "Lam on nhap mat khau dai hon 6 ky tu";
			return;
		}
		else if(!_inputFieldPasswordRegister.text.Equals(_inputFieldConfirm.text))
		{
			_txtErrorRegister.text = "Password and Confirm password are not same";
			return;
		}

		playfab.Register(_inputFieldEmailRegister.text, _inputFieldPasswordRegister.text, OnRegisterSuccess, ErrorRegister);
		Close();
	}

	private void ErrorRegister(PlayFabError error)
	{
		_txtErrorRegister.text = "Input wrong email or password!";
	}

	private void OnRegisterSuccess(RegisterPlayFabUserResult result)
	{
		playfab.Login(_inputFieldEmailRegister.text, _inputFieldEmailRegister.text, OnLoginSuccess, ErrorLogin);
	}

	private void OnClickBtnLogin()
	{
		playfab.Login(_inputEmail.text, _inputPassword.text, OnLoginSuccess, ErrorLogin);
	}

	private void OnClickBtnSignIn()
	{
		_goLogin.SetActive(false);
		_goRegister.SetActive(true);
		Close();
	}

	private void ErrorLogin(PlayFabError error)
	{
		_txtError.text = "Input wrong email or password!";
	}

	private void OnLoginSuccess(LoginResult result)
	{
		Singleton<GameController>.instance.ChangeSceneHome();
	}
}