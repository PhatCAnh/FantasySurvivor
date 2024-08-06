using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LangPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnClose;
    [SerializeField] private Transform _goMainContent; 

    private void OnClickBtnClose()
    {
        Close();
    }

    protected override void OnViewInit()
    {
        base.OnViewInit();
        Open();
    }

    public void Open()
    {
        _btnClose.onClick.AddListener(Close);
        _goMainContent.localScale = Vector3.zero;
        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() => { Destroy(gameObject); });
    }

}
