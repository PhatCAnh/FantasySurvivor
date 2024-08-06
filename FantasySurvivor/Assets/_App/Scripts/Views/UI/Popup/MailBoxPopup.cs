using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMail
{
    public string id;
    public string title;
    public string description;
    public List<ItemInBag> listReward;
    public bool isClaimed;

    public ItemMail(string id, string title, string description, List<ItemInBag> listReward)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.listReward = listReward;
        isClaimed = false;
    }
}



public class MailBoxPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimer, _btnClose;

    [SerializeField] private Transform _goMainContent, _goContainer;

    [SerializeField] private GameObject _goPrefab;

    protected override void OnViewInit()
    {
        base.OnViewInit();

        foreach (var item in app.resourceManager.GetMail())
        {
            var value = item.Value;
            Instantiate(_goPrefab, _goContainer).TryGetComponent(out ItemMailUI mail);
            mail.Init(value);
        }
        
        _btnClose.onClick.AddListener(Close);
        _btnDimer.onClick.AddListener(Close);
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