using System;
using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionPopup : View<GameApp>, IPopup
{
    private class DailyMissionItemDetail
    {
        public string title;
        public ItemId id;
        public int quantity;
        public ItemType type;

        public DailyMissionItemDetail(string title, ItemId id, ItemType type, int quantity)
        {
            this.title = title;
            this.id = id;
            this.type = type;
            this.quantity = quantity;
        }
    }

    [SerializeField] private DailyMissionItem[] _arrDailyMissionItems;
    [SerializeField] private Button _btnClose;
    [SerializeField] private Transform _goMainContent;

    private GameController gameController => Singleton<GameController>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        Debug.Log("DailyMissionPopup: OnViewInit");
        Init();
        CheckAndResetDailyMissions();
        UpdateMissionStatuses();
        Open();
    }

    private void Init()
    {
        Debug.Log("DailyMissionPopup: Init");
        _btnClose.onClick.AddListener(Close);

        var arrDailyMissionItemDetail = new[]
        {
            new DailyMissionItemDetail("Enter the game", ItemId.Gold, ItemType.ETC, 300),
            new DailyMissionItemDetail("Play 3 games", ItemId.Gold, ItemType.ETC, 1000),
        };

        for (int i = 0; i < arrDailyMissionItemDetail.Length; i++)
        {
            _arrDailyMissionItems[i].Init(arrDailyMissionItemDetail[i].title, arrDailyMissionItemDetail[i].id, arrDailyMissionItemDetail[i].type, arrDailyMissionItemDetail[i].quantity, MissionStatus.Incomplete, this);
        }
    }

    private void UpdateMissionStatuses()
    {
        for (int i = 0; i < _arrDailyMissionItems.Length; i++)
        {
            var currentItem = _arrDailyMissionItems[i];
            MissionStatus savedStatus = app.models.dataPlayerModel.GetMissionStatus(currentItem.GetTitle());
            currentItem.status = savedStatus;
            if (currentItem.status == MissionStatus.Incomplete && currentItem.CheckCondition())
            {
                currentItem.status = MissionStatus.Complete;
            }
            currentItem.SetData(); 
        }
    }
    private void SaveMissionStatuses()
    {
        string saveData = "";
        for (int i = 0; i < _arrDailyMissionItems.Length; i++)
        {
            saveData += _arrDailyMissionItems[i].status;
            if (i != _arrDailyMissionItems.Length - 1)
            {
                saveData += "-";
            }
        }
        app.models.dataPlayerModel.DataSaveClaimMissionGift = saveData;
    }
    private bool CheckCondition(int index)
    {
        return true;
    }
    public void Open()
    {
        Debug.Log("DailyMissionPopup: Open");
        _goMainContent.localScale = Vector3.zero;
        _goMainContent.DOScale(Vector3.one, 0.15f);
    }

    public void Close()
    {
        Debug.Log("DailyMissionPopup: Close");
        _goMainContent.DOScale(Vector3.zero, 0.15f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public void OnClickClaim()
    {
        Debug.Log("DailyMissionPopup: OnClickClaim - Save mission status");
        SaveMissionStatuses();
    }
    private void CheckAndResetDailyMissions()
    {
        DateTime currentDate = DateTime.Now.Date;
        if (currentDate > app.models.dataPlayerModel.LastResetDate)
        {
            ResetDailyMissions();
            app.models.dataPlayerModel.LastResetDate = currentDate;
            app.models.dataPlayerModel.Save();
        }
    }

    private void ResetDailyMissions()
    {
        foreach (var missionItem in _arrDailyMissionItems)
        {
            if (missionItem.status == MissionStatus.Claimed)
            {
                Debug.Log($"Mission '{missionItem.GetTitle()}' was claimed and is now resetting.");
            }
            missionItem.ResetMission();
        }

        app.models.dataPlayerModel.DailyGamePlays = 0; 
        app.models.dataPlayerModel.Save();
    }
}
