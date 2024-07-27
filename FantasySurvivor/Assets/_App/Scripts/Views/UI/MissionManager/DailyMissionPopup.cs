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
        UpdateMissionStatuses();
        Open();
    }

    private void Init()
    {
        Debug.Log("DailyMissionPopup: Init");
        _btnClose.onClick.AddListener(Close);

        var arrDailyMissionItemDetail = new[]
        {
            new DailyMissionItemDetail("Enter the game", ItemId.Gold, ItemType.ETC, 100),
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
            // Tải trạng thái nhiệm vụ từ mô hình dữ liệu
            MissionStatus savedStatus = app.models.dataPlayerModel.GetMissionStatus(currentItem.GetTitle());
            currentItem.status = savedStatus;
            currentItem.SetData(); // Cập nhật UI
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
        // Logic để kiểm tra điều kiện hoàn thành nhiệm vụ
        // Ví dụ: kiểm tra số lần nhấn nút hoặc các điều kiện khác
        // Trả về true nếu nhiệm vụ đã hoàn thành
        return true; // Thay thế bằng điều kiện thực tế
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
        SaveMissionStatuses(); // Lưu trạng thái của các nhiệm vụ
    }

}
