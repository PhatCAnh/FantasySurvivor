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
using _App.Scripts.Enums;

public class DailyMissionPopup : View<GameApp>, IPopup
{

    [SerializeField] private Button _btnClose;
    [SerializeField] private Transform _goMainContent;
    [SerializeField] public DailyTaskItem[] _dailyTaskItems;

    private DataPlayerModel dataPlayerModel => Singleton<DataPlayerModel>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        Init();
        Open();
    }

    private void Init()
    {
        _btnClose.onClick.AddListener(Close);

        // Initialize the two tasks with descriptions and button click requirements
        _dailyTaskItems[0].Init("Nhấn 2 lần nút Setting", TaskStatus.Incomplete, this);

        // Update task statuses
        foreach (var taskItem in _dailyTaskItems)
        {
            taskItem.CheckTaskStatus();
        }
    }

    public void Open()
    {
        _goMainContent.localScale = Vector3.zero;
        _goMainContent.DOScale(Vector3.one, 0.25f);
    }

    public void Close()
    {
        _goMainContent.DOScale(Vector3.zero, 0.25f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    public void UpdateTaskStatus(int taskIndex, TaskStatus newStatus)
    {
        if (taskIndex >= 0 && taskIndex < _dailyTaskItems.Length)
        {
            _dailyTaskItems[taskIndex].UpdateTaskStatus(newStatus);
        }
    }

    public void SaveTaskStatus()
    {
        // Status already saved in DailyTaskItem.cs using PlayerPrefs
    }
}