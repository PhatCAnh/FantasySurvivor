using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadMailPopup : View<GameApp>, IPopup
{
    [SerializeField] private TextMeshProUGUI _txtTitle, _txtDescription;

    [SerializeField] private ItemSlotUI _item;

    [SerializeField] private Button _btnClaim, _btnClose;
    
    [SerializeField] private Transform _goMainContent, _goContainer;

    public void Init(ItemMail itemMail)
    {
        _txtTitle.text = itemMail.title;
        _txtDescription.text = itemMail.description;
        
        _btnClose.onClick.AddListener(Close);

        if (itemMail.listReward.Count == 0)
        {
            _btnClaim.gameObject.SetActive(false);
            return;
        }
        
        foreach (var item in itemMail.listReward)
        {
            Instantiate(_item, _goContainer).TryGetComponent(out ItemSlotUI itemSlotUI);
            itemSlotUI.Init(item);
        }
        
        _btnClaim.onClick.AddListener(() =>
        {
            app.models.dataPlayerModel.ReadMail(itemMail.id);
            app.resourceManager.ShowPopup(PopupType.RewardGetPopup).TryGetComponent(out PopupReward rewardGetPopup);
            rewardGetPopup.Init(itemMail.listReward);
            _btnClaim.interactable = false;
        });

        if (app.models.dataPlayerModel.listMailRead.Contains(itemMail.id))
        {
            _btnClaim.interactable = false;
        }
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
