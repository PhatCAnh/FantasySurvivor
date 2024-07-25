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

public class DailyTaskItem : MonoBehaviour
{

    [SerializeField] private Button _btnIncomplete;
    [SerializeField] private Button _btnComplete;
    [SerializeField] private Button _btnClaimed;

    public TaskStatus status { get; private set; }
    private DailyMissionPopup _popup;

    public void Init(string description, TaskStatus status, DailyMissionPopup popup)
    {
        this.status = status;
        _popup = popup;

        _btnIncomplete.onClick.RemoveAllListeners();
        _btnComplete.onClick.RemoveAllListeners();
        _btnClaimed.onClick.RemoveAllListeners();

        _btnIncomplete.onClick.AddListener(OnIncompleteTask);
        _btnComplete.onClick.AddListener(OnCompleteTask);
        _btnClaimed.onClick.AddListener(OnClaimedTask);

        CheckTaskStatus();
        UpdateUI();
    }

    public void CheckTaskStatus()
    {
        string taskStatus = PlayerPrefs.GetString("Task1Status", TaskStatus.Incomplete.ToString());
        status = (TaskStatus)Enum.Parse(typeof(TaskStatus), taskStatus);
        UpdateUI();
    }

    private void OnIncompleteTask()
    {
        CheckTaskStatus();
    }

    private void OnCompleteTask()
    {
        if (status == TaskStatus.Incomplete)
        {
            status = TaskStatus.Complete;
            UpdateUI();
            PlayerPrefs.SetString("Task1Status", status.ToString());
        }
        else if (status == TaskStatus.Complete)
        {
            status = TaskStatus.Claimed;
            UpdateUI();
            PlayerPrefs.SetString("Task1Status", status.ToString());
        }
    }

    private void OnClaimedTask()
    {
        Debug.Log("Reward already claimed!");
    }

    public void UpdateUI()
    {
        _btnIncomplete.gameObject.SetActive(status == TaskStatus.Incomplete);
        _btnComplete.gameObject.SetActive(status == TaskStatus.Complete);
        _btnClaimed.gameObject.SetActive(status == TaskStatus.Claimed);
    }

    public void UpdateTaskStatus(TaskStatus newStatus)
    {
        status = newStatus;
        UpdateUI();
        PlayerPrefs.SetString("Task1Status", status.ToString());
    }
}