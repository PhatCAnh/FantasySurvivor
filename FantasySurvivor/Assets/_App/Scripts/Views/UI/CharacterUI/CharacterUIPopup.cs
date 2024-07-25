using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterUIPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnClose;

    [SerializeField] private Transform _goMainContent;
    protected override void OnViewInit()
    {
        _btnClose.onClick.AddListener(Close);
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
            .OnComplete(() => { Destroy(gameObject); });
    }
}