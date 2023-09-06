using ArbanFramework;
using ArbanFramework.MVC;
using MR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class ResourceManager : UIManagerBase<PopupType>
{
    //[SerializeField] GameObject keyPrefab;
    
    [Header("Object Prefabs")]

    [Required, SerializeField] private GameObject _healthBarPrefab;
    
    [Required, SerializeField] private GameObject _towerPrefab;

    private Dictionary<ItemType, GameObject> _itemDic;
    
    [Header("UI Prefabs")]
    
    [Required, SerializeField] private GameObject _mainUIInGame;
    
    [Required, SerializeField] private GameObject _pausePopup;
    
    [Required, SerializeField] private GameObject _choiceMapPopup;
    
    [Required, SerializeField] private GameObject _loseGamePopup;
    
    

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

        RegisterPopup(PopupType.Pause, _pausePopup);
        RegisterPopup(PopupType.MainInGame, _mainUIInGame);
        RegisterPopup(PopupType.ChoiceMap, _choiceMapPopup);
        RegisterPopup(PopupType.LoseGame, _loseGamePopup);
    }

    private void InitItemDic()
    {
        _itemDic = new Dictionary<ItemType, GameObject>()
        {
            {ItemType.HealthBar, _healthBarPrefab },
            {ItemType.Tower, _towerPrefab },
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

