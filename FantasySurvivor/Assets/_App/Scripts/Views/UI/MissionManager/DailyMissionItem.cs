using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Enums;
using ArbanFramework;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DailyMissionItem : View<GameApp>
{
    [SerializeField] private TextMeshProUGUI _txtTitle;
    [SerializeField] private TextMeshProUGUI _txtValue;
    [SerializeField] private GameObject _goIncomplete, _goComplete, _goClaimed;
    [SerializeField] private Button _btnIncomplete, _btnComplete, _btnClaim;

    private string _title;
    private ItemId _id;
    private ItemType _type;
    private int _quantity;
    private DailyMissionPopup _parent;
    private ItemController itemController => Singleton<ItemController>.instance;

    public MissionStatus status;

    public void Init(string title, ItemId id, ItemType type, int quantity, MissionStatus status, DailyMissionPopup parent)
    {
        Debug.Log($"DailyMissionItem: Init - Title: {title}, ItemId: {id}, Quantity: {quantity}, Status: {status}");
        _title = title;
        _id = id;
        _type = type;
        _quantity = quantity;
        this.status = status;
        this._parent = parent;
        _txtTitle.text = title;
        _txtValue.text = $"{_quantity}";

        if (this.status == MissionStatus.Incomplete && CheckCondition())
        {
            this.status = MissionStatus.Complete;
        }

        SetData();
    }

    public void SetData()
    {
        Debug.Log($"DailyMissionItem: SetData - Status: {status}");
        _goIncomplete.SetActive(false);
        _goComplete.SetActive(false);
        _goClaimed.SetActive(false);

        _btnIncomplete.gameObject.SetActive(false);
        _btnComplete.gameObject.SetActive(false);
        _btnClaim.gameObject.SetActive(false);

        _btnIncomplete.onClick.RemoveListener(OnClickBtnIncomplete);
        _btnComplete.onClick.RemoveListener(OnClickBtnComplete);
        _btnClaim.onClick.RemoveListener(OnClickBtnClaim);

        switch (status)
        {
            case MissionStatus.Incomplete:
                _goIncomplete.SetActive(true);
                _btnIncomplete.gameObject.SetActive(true);
                _btnIncomplete.onClick.AddListener(OnClickBtnIncomplete);
                break;
            case MissionStatus.Complete:
                _goComplete.SetActive(true);
                _btnComplete.gameObject.SetActive(true);
                _btnComplete.onClick.AddListener(OnClickBtnComplete);
                break;
            case MissionStatus.Claimed:
                _goClaimed.SetActive(true);
                _btnClaim.gameObject.SetActive(true);
                _btnClaim.onClick.AddListener(OnClickBtnClaim);
                break;
        }
    }

    private void OnClickBtnIncomplete()
    {
        Debug.Log("DailyMissionItem: OnClickBtnIncomplete - Checking condition");

        if (CheckCondition())
        {
            Debug.Log("DailyMissionItem: Condition met - Changing status to Complete");
            status = MissionStatus.Complete;
            SetData();
        }
    }

    private void OnClickBtnComplete()
    {
        Debug.Log($"DailyMissionItem: OnClickBtnComplete - Claiming reward: {_quantity} gold");

        app.models.dataPlayerModel.AddCoins(_quantity);
        status = MissionStatus.Claimed;
        app.models.dataPlayerModel.SaveMissionStatus(GetTitle(), status);
        SetData();
    }
    public string GetTitle()
    {
        return _title;
    }
    private void OnClickBtnClaim()
    {
        if (status == MissionStatus.Claimed)
        {
            Debug.Log("DailyMissionItem: OnClickBtnClaim - Reward already claimed. Please wait until after 12 AM for the next reset.");
            ShowWaitMessage();
        }
        else
        {
            Debug.Log("DailyMissionItem: OnClickBtnClaim - Claiming reward");
            status = MissionStatus.Claimed;
            app.models.dataPlayerModel.AddCoins(_quantity);
            SetData();
        }
    }
    private void ShowWaitMessage()
    {
        Debug.Log("Please wait until after 12 AM for the next reset.");
    }
    public bool CheckCondition()
    {
        Debug.Log("DailyMissionItem: CheckCondition - Verifying mission completion");
        return true; 
    }
    public void ResetMission()
    {
        status = MissionStatus.Incomplete;
        SetData();
    }
}

