using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ResourceManager : UIManagerBase<PopupType>
{
    [SerializeField] GameObject keyPrefab;

    [SerializeField] private GameObject mainUIPopupPrefab;
    [SerializeField] private GameObject notificationPopupPrefab;
    [SerializeField] private GameObject winGamePopupPrefab;
    [SerializeField] private GameObject loseGamePopupPrefab; 
    [SerializeField] private GameObject choiceMapPopupPrefab;

    private Dictionary<ItemType, GameObject> _itemDic;

    private void Awake()
    {
        Singleton<ResourceManager>.Set(this);
        Init();
    }

    private void OnDestroy()
    {
        Singleton<ResourceManager>.Unset(this);
    }

    public void Init()
    {
        InitItemDic();

        RegisterPopup(PopupType.Main, mainUIPopupPrefab);
        RegisterPopup(PopupType.Notification, notificationPopupPrefab);
        RegisterPopup(PopupType.WinGame, winGamePopupPrefab);
        RegisterPopup(PopupType.LoseGame, loseGamePopupPrefab);
        RegisterPopup(PopupType.ChoiceMap, choiceMapPopupPrefab);
    }

    private void InitItemDic()
    {
        _itemDic = new Dictionary<ItemType, GameObject>()
        {
            {ItemType.Key, keyPrefab }
        };
    }    

    public GameObject GetItem(ItemType itemType)
    {
        return _itemDic[itemType];
    }    

    public override GameObject ShowPopup(PopupType type, Action<GameObject> onInit = null)
    {
        var popupGo = base.ShowPopup(type, onInit);
        if (!popupGo)
            return null;
        popupGo.GetOrAddComponent<GraphicRaycaster>();
        return popupGo;
    }
}

