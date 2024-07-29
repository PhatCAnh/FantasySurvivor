using System;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimmer, _btnYes, _btnNo;

    [SerializeField] private TextMeshProUGUI _txtDescription;
    
    [SerializeField] private Transform _goMainContent;

    public void Init(string description, Action callBackYes)
    {
        _txtDescription.text = description;
        _btnYes.onClick.AddListener(() => callBackYes());
        _btnNo.onClick.AddListener(Close);
        _btnDimmer.onClick.AddListener(Close);
        Open();
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
