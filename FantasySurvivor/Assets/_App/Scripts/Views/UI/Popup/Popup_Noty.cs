using ArbanFramework.MVC;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Popup_Noty : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription;
    [SerializeField] private Button _btnClose, _btnOk;

    public void Open()
    {
    }
    public void Close()
    {
        Destroy(gameObject);
    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        _btnOk.onClick.AddListener(Close);
        _btnClose.onClick.AddListener(Close);
    }
}
