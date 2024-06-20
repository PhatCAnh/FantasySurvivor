using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWarning : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtValue;
    [SerializeField] private Button _btnClose, _btnOk, _btnAction;
    
    public void Open()
    {
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    
    public void Init(string description)
    {
        _txtDescription.text = description;
        _btnOk.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
    }

    public void Init(string description, string txtValue, UnityAction callBack = null, bool isOk = true)
    {
        _txtValue.text = txtValue;
        _txtDescription.text = description;
        _btnOk.gameObject.SetActive(isOk);
        _btnOk.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
        if(callBack != null)
        {
            _btnAction.gameObject.SetActive(true);
            _btnAction.onClick.AddListener(callBack);
        }
    }
    
    public void Init(string namePopup, string description, string txtValue, UnityAction callBack = null, bool isOk = true)
    {
        _txtValue.text = txtValue;
        _txtName.text = namePopup;
        _txtDescription.text = description;
        _btnOk.gameObject.SetActive(isOk);
        _btnOk.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
        if(callBack != null)
        {
            _btnAction.gameObject.SetActive(true);
            _btnAction.onClick.AddListener(callBack);
        }
    }
}
