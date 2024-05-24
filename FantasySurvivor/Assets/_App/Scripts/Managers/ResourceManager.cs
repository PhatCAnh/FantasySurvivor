using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
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

	[Required, SerializeField] private GameObject _characterPrefab;

	[Required, SerializeField] private GameObject _gemExpPrefab;
	
	[Required, SerializeField] private GameObject _textPopup;
	
	[Required, SerializeField] private GameObject _supportItem;
	
	[Required, SerializeField] private GameObject _bulletKillerBee;

    [Required, SerializeField] private GameObject _bulletGlobloomSentry;

    private Dictionary<ItemPrefab, GameObject> _itemPrefabDic;


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
	[Required, SerializeField] private Map _forestMap;
	
	public MapInfinityController mapInfinity;

	private Dictionary<MapType, Map> _mapDic;

	[Header("Monster prefabs")]
	[Required, SerializeField] private GameObject _blueZombie;

	[Required, SerializeField] private GameObject _purpleZombie;

	[Required, SerializeField] private GameObject _blueGhost;

	[Required, SerializeField] private GameObject _yellowBomb;

	[Required, SerializeField] private GameObject _lupineStag;

    [Required, SerializeField] private GameObject _killerBee;

    [Required, SerializeField] private GameObject _globloomSentry;

    [Required, SerializeField] private GameObject _electroBomber;

    [Required, SerializeField] private GameObject _slimeWandering;

    [Required, SerializeField] private GameObject _minionSkeleton;

    [Required, SerializeField] private GameObject _swampOoze;





    private Dictionary<string, GameObject> _typeMonsterDic;

	[Header("Item reward prefabs")]
	[Required, SerializeField] private ItemReward _irCoin;

	private Dictionary<TypeItemReward, ItemReward> _typeItemReward;

	[Header("Drop Item prefabs")]
	[Required, SerializeField] private float _exp;
	[Required, SerializeField] private float _magnet;
	[Required, SerializeField] private float _food;
	[Required, SerializeField] private float _bomb;

	private Dictionary<DropItemType, float> _dropItemDic;

	[SerializeField] private List<SkillData> _poolSkill;

	public Dictionary<SkillName, Skill> dictionarySkill = new Dictionary<SkillName, Skill>();
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
		_itemPrefabDic = new Dictionary<ItemPrefab, GameObject>()
		{
			{ItemPrefab.HealthBar, _healthBarPrefab},
			{ItemPrefab.Character, _characterPrefab},
			{ItemPrefab.TextPopup, _textPopup},
			{ItemPrefab.GemExp, _gemExpPrefab},
			{ItemPrefab.BlueZombie, _blueZombie},
            {ItemPrefab.PurpleZombie, _purpleZombie},
            {ItemPrefab.BlueGhost, _blueGhost},
			{ItemPrefab.YellowBomb, _yellowBomb},
			{ItemPrefab.LupineStag, _lupineStag},
			{ItemPrefab.KillerBee, _killerBee},
            {ItemPrefab.GlobloomSentry, _globloomSentry},
			{ItemPrefab.ElectroBomber, _electroBomber},
            {ItemPrefab.SlimeWandering, _slimeWandering},
			{ItemPrefab.MinionSkeleton, _minionSkeleton},
			{ItemPrefab.SwampOoze, _swampOoze},

            {ItemPrefab.SupportItem, _supportItem},
			{ItemPrefab.BulletKillerBee, _bulletKillerBee},
            {ItemPrefab.BulletGlobloomSentry, _bulletGlobloomSentry},

        };

		_mapDic = new Dictionary<MapType, Map>
		{
			{MapType.Forest, _forestMap},
		};

		_typeMonsterDic = new Dictionary<string, GameObject>
		{
			{"M1", _blueZombie},
			{"M2", _purpleZombie},
			{"M3", _blueGhost},
			{"M4", _yellowBomb},
			{"M5", _lupineStag},
            {"M6", _killerBee},
            {"M7", _globloomSentry},
			{"M8", _electroBomber},
			{"M9", _slimeWandering},
			{"M10", _swampOoze},
			{"Skeleton", _minionSkeleton},

        };

		_typeItemReward = new Dictionary<TypeItemReward, ItemReward>()
		{
			{TypeItemReward.Coin, _irCoin},
		};

		_dropItemDic = new Dictionary<DropItemType, float>()
		{
			{DropItemType.Exp, _exp},
			{DropItemType.Magnet, _magnet},
			{DropItemType.Bomb, _bomb},
			{DropItemType.Food, _food},
		};
	}

	public GameObject GetItemPrefab(ItemPrefab itemPrefab)
	{
		return _itemPrefabDic[itemPrefab];
	}

	public Map GetMap(MapType mapType)
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

	public List<SkillData> GetListSkill()
	{
		return _poolSkill.Where(skill => skill.name != SkillName.Food).ToList();
	}

	public SkillData GetSkill(SkillName name)
	{
		return _poolSkill.FirstOrDefault(skill => skill.name == name);
	}

	public Dictionary<DropItemType, float> GetDicDropItem()
	{
		return _dropItemDic;
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