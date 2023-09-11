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
    
    [Header("Object prefabs")]

    [Required, SerializeField] private GameObject _healthBarPrefab;
    
    [Required, SerializeField] private GameObject _towerPrefab;

    private Dictionary<ItemType, GameObject> _itemDic;
    
    
    [Header("UI prefabs")]
    
    [Required, SerializeField] private GameObject _mainUIInGame;
    
    [Required, SerializeField] private GameObject _pausePopup;
    
    [Required, SerializeField] private GameObject _choiceMapPopup;
    
    [Required, SerializeField] private GameObject _loseGamePopup;
    
    
    [Header("Map prefabs")]
    
    [Required, SerializeField] private GameObject _forestMap;

    private Dictionary<MapType, GameObject> _mapDic;
    
    [Required, SerializeField] private GameObject _blueZombie;
    
    [Required, SerializeField] private GameObject _purpleZombie;
    
    [Required, SerializeField] private GameObject _blueGhost;
    
    private Dictionary<SkinMonsterType, GameObject> _skinMonsterDic;

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
        InitDic();
        RegisterPopup(PopupType.MainInGame, _mainUIInGame);
        RegisterPopup(PopupType.ChoiceMap, _choiceMapPopup);
        RegisterPopup(PopupType.LoseGame, _loseGamePopup);
        RegisterPopup(PopupType.Pause, _pausePopup);
    }

    private void InitDic()
    {
        _itemDic = new Dictionary<ItemType, GameObject>()
        {
            {ItemType.HealthBar, _healthBarPrefab },
            {ItemType.Tower, _towerPrefab },
        };

        _mapDic = new Dictionary<MapType, GameObject>
        {
            { MapType.Forest, _forestMap },
        };
        
        _skinMonsterDic = new Dictionary<SkinMonsterType, GameObject>
        {
            { SkinMonsterType.BlueZombie, _blueZombie },
            { SkinMonsterType.PurpleZombie, _purpleZombie },
            { SkinMonsterType.BlueGhost, _blueGhost },
        };
    }    

    public GameObject GetItem(ItemType itemType)
    {
        return _itemDic[itemType];
    }

    public GameObject GetMap(MapType mapType)
    {
        return _mapDic[mapType];
    }

    public GameObject GetMonster(SkinMonsterType skinMonsterType)
    {
        return _skinMonsterDic[skinMonsterType];
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

