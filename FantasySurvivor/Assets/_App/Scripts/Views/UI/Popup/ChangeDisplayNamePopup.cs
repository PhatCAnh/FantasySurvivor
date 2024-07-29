using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDisplayNamePopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimer, _btnClose, _btnSave;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Transform _goMainContent;

    private PlayfabController playfabController => Singleton<PlayfabController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        
        _btnClose.onClick.AddListener(Close);
        _btnDimer.onClick.AddListener(Close);
        _btnSave.onClick.AddListener(OnClickBtnSave);
        
        Open();
    }

    private void OnClickBtnSave()
    {
        playfabController.SubmitNameButton(_inputField.text);
        Destroy(gameObject);
    }

    public void Open()
    {
        _goMainContent.localScale = Vector3.zero;

        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
