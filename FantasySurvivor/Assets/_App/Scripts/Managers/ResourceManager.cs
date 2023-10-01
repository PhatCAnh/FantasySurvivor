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
	[Header("Object prefabs")]
	[Required, SerializeField] private GameObject _healthBarPrefab;

	[Required, SerializeField] private GameObject _towerPrefab;

	[Required, SerializeField] private GameObject _gemExpPrefab;

	private Dictionary<ItemType, GameObject> _itemDic;


	[Header("UI prefabs")]
	[Required, SerializeField] private GameObject _mainUIInGame;


	[Required, SerializeField] private GameObject _mainUI;

	[Required, SerializeField] private GameObject _pausePopup;

	[Required, SerializeField] private GameObject _cheatPopup;

	[Required, SerializeField] private GameObject _choiceMapPopup;

	[Required, SerializeField] private GameObject _loseGamePopup;
	
	[Required, SerializeField] private GameObject _choiceSkill;
	

	[Header("UI Tutorial prefabs")]
	[Required, SerializeField] private GameObject _clickBulletTutorial;

	[Header("Map prefabs")]
	[Required, SerializeField] private GameObject _forestMap;

	[Required, SerializeField] private GameObject _oceanMap;

	private Dictionary<MapType, GameObject> _mapDic;

	[Header("Monster prefabs")]
	[Required, SerializeField] private GameObject _blueZombie;

	[Required, SerializeField] private GameObject _purpleZombie;

	[Required, SerializeField] private GameObject _blueGhost;

	[Required, SerializeField] private GameObject _yellowBomb;

	[Required, SerializeField] private GameObject _purpleDevil;

	private Dictionary<string, GameObject> _typeMonsterDic;

	[Header("Item reward prefabs")]
	[Required, SerializeField] private ItemReward _irCoin;

	private Dictionary<TypeItemReward, ItemReward> _typeItemReward;

	[SerializeField] private List<SkillData>  _poolSkill;
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
		RegisterPopup(PopupType.MainUI, _mainUI);
		RegisterPopup(PopupType.MainInGame, _mainUIInGame);
		RegisterPopup(PopupType.ClickBulletTutorial, _clickBulletTutorial);
		RegisterPopup(PopupType.ChoiceMap, _choiceMapPopup);
		RegisterPopup(PopupType.LoseGame, _loseGamePopup);
		RegisterPopup(PopupType.Pause, _pausePopup);
		RegisterPopup(PopupType.Cheat, _cheatPopup);
		RegisterPopup(PopupType.ChoiceSkill, _choiceSkill);
	}

	private void InitDic()
	{
		_itemDic = new Dictionary<ItemType, GameObject>()
		{
			{ItemType.HealthBar, _healthBarPrefab},
			{ItemType.Tower, _towerPrefab},
			{ItemType.GemExp, _gemExpPrefab},
		};

		_mapDic = new Dictionary<MapType, GameObject>
		{
			{MapType.Forest, _forestMap},
			{MapType.Ocean, _oceanMap},
		};

		_typeMonsterDic = new Dictionary<string, GameObject>
		{
			{"M1", _blueZombie},
			{"M2", _purpleZombie},
			{"M3", _blueGhost},
			{"M4", _yellowBomb},
			{"M5", _purpleDevil},
		};

		_typeItemReward = new Dictionary<TypeItemReward, ItemReward>()
		{
			{TypeItemReward.Coin, _irCoin},
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

	public GameObject GetMonster(string monsterId)
	{
		return _typeMonsterDic[monsterId];
	}

	public ItemReward GetItemReward(TypeItemReward type)
	{
		return _typeItemReward[type];
	}

	public SkillData GetSkill(SkillType type)
	{
		return _poolSkill.FirstOrDefault(skill => skill.type == type);
	}


	public override GameObject ShowPopup(PopupType type, Action<GameObject> onInit = null)
	{
		var popupGo = base.ShowPopup(type, onInit);
		if(!popupGo)
			return null;
		popupGo.GetOrAddComponent<GraphicRaycaster>();
		return popupGo;
	}


}