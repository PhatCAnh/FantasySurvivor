using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfoPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimmer, _btnCloudSave, _btnClose, _btnChangeNameDisplay;

    [SerializeField] private Transform _goMainContent;

    [SerializeField] private TextMeshProUGUI _txtNameTag, _txtNameDisplay;

    private void OnClickBtnCloudSave()
    {
        app.resourceManager.ShowPopup(PopupType.Cloud);
    }

    protected override void OnViewInit()
    {
        base.OnViewInit();
        Open();
    }

    public void Open()
    {
        var model = app.models.dataPlayerModel;

        _txtNameDisplay.text = model.NameDisplay;
        
        _txtNameTag.text =  model.Id;

        _btnClose.onClick.AddListener(Close);
        _btnDimmer.onClick.AddListener(Close);
        _btnCloudSave.onClick.AddListener(OnClickBtnCloudSave);
        _btnChangeNameDisplay.onClick.AddListener(OnClickBtnChangeNameDisplay);
        
        AddDataBinding("fieldDataPlayerModel-nameDisplayValue", _txtNameDisplay, (control, e) =>
            {
                control.text = model.NameDisplay;
            }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.NameDisplay), app.models.dataPlayerModel)
        );

        _goMainContent.localScale = Vector3.zero;

        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    private void OnClickBtnChangeNameDisplay()
    {
        app.resourceManager.ShowPopup(PopupType.ChangeDisplayName);
        Destroy(gameObject);
    }
    
    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() => { Destroy(gameObject); });
    }
}