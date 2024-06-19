using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
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
	[Header("Object prefabs")]
	[SerializeField] private GameObject _healthBarPrefab;

	[SerializeField] private GameObject _characterPrefab;

	[SerializeField] private GameObject _gemExpPrefab;

	[SerializeField] private GameObject _textPopup;

	[SerializeField] private GameObject _supportItem;

	[SerializeField] private GameObject _bulletKillerBee;

	[SerializeField] private GameObject _skillFireShield;

	[SerializeField] private GameObject _skillFireBall;

	[SerializeField] private GameObject _skillThunderStrike;

	[SerializeField] private GameObject _skillThunderStrikeSmall;

	[SerializeField] private GameObject _bulletGlobloomSentry;

	[SerializeField] private GameObject _skillWaterBall;

	[SerializeField] private GameObject _skillShark;

	[SerializeField] private GameObject _skillPoisonBullet;

	[SerializeField] private GameObject _skillEarthPunch;

	[SerializeField] private GameObject _skillSkyBoom;

	[SerializeField] private GameObject _skillBoomerang;

	[SerializeField] private GameObject _skillSmilingface;

	[SerializeField] private GameObject _skillIceSpear;

	[SerializeField] private GameObject _skillTwin;
	
	[SerializeField] private GameObject _bulletOwlStriker;

    [SerializeField] private GameObject _bulletGoblinswift;

	[SerializeField] private GameObject _skillCyclone;
	
    [SerializeField] private GameObject _bulletBossGatlingCrab;

	[SerializeField] private GameObject _skillWindfield;

	[SerializeField] private GameObject _skillThunderPunch;

	[SerializeField] private GameObject _skillEarthShield;

	[SerializeField] private GameObject _skillLightningWeb;

	[SerializeField] private GameObject _skillThunderChanneling;

	[SerializeField] private GameObject _skillTimeBoom;

    private Dictionary<ItemPrefab, GameObject> _itemPrefabDic;


	[Header("UI prefabs")]
	[SerializeField] private GameObject _mainUIInGame;

	[SerializeField] private GameObject _mainUI;

	[SerializeField] private GameObject _pausePopup;

	[SerializeField] private GameObject _cheatPopup;

	[SerializeField] private GameObject _choiceMapPopup;

	[SerializeField] private GameObject _loseGamePopup;

	[SerializeField] private GameObject _choiceSkill;

	[SerializeField] private GameObject _choiceSkillOutGame;
	
	[SerializeField] private GameObject _characterInformation;

    [SerializeField] private GameObject _characterChoose;
    [SerializeField] private GameObject _itemEquipDetail;
	
	[SerializeField] private GameObject _itemPieceDetail;


	[Header("UI Tutorial prefabs")]
	[SerializeField] private GameObject _clickBulletTutorial;

	[Header("Map prefabs")]
	[SerializeField] private Map _meadowMap;

	[SerializeField] private Map _forestMap;

	public MapInfinityController mapInfinity;

	private Dictionary<MapType, Map> _mapDic;

	[Header("Monster prefabs")]
	[SerializeField] private GameObject _swampOoze;

	[SerializeField] private GameObject _killerBee;

	[SerializeField] private GameObject _globloomSentry;

	[SerializeField] private GameObject _lupineStag;

	[SerializeField] private GameObject _electroBomber;

	[SerializeField] private GameObject _slimeWandering;

	[SerializeField] private GameObject _minionSkeleton;

	[SerializeField] private GameObject _minionWolf;

	[SerializeField] private GameObject _owlStriker;

	[SerializeField] private GameObject _goblinSwift;

    [SerializeField] private GameObject _minionGoblin;

    [SerializeField] private GameObject _gatlingCrab;


    private Dictionary<string, GameObject> _typeMonsterDic;

	[Header("Item reward prefabs")]
	[SerializeField] private ItemReward _irCoin;

	private Dictionary<TypeItemReward, ItemReward> _typeItemReward;

	[Header("Drop Item prefabs")]
	[SerializeField] private float _exp;
	[SerializeField] private float _magnet;
	[SerializeField] private float _food;
	[SerializeField] private float _bomb;

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
		RegisterPopup(PopupType.ChoiceSkillOutGame, _choiceSkillOutGame);
		RegisterPopup(PopupType.CharacterInformation, _characterInformation);
		RegisterPopup(PopupType.ItemEquipDetail, _itemEquipDetail);
		RegisterPopup(PopupType.ItemPieceDetail, _itemPieceDetail);
		RegisterPopup(PopupType.CharacterChoose, _characterChoose);
	}

	private void InitDic()
	{
		_itemPrefabDic = new Dictionary<ItemPrefab, GameObject>()
		{
			{ItemPrefab.HealthBar, _healthBarPrefab},
			{ItemPrefab.Character, _characterPrefab},
			{ItemPrefab.TextPopup, _textPopup},
			{ItemPrefab.GemExp, _gemExpPrefab},

			{ItemPrefab.SwampOoze, _swampOoze},
			{ItemPrefab.KillerBee, _killerBee},
			{ItemPrefab.GlobloomSentry, _globloomSentry},
			{ItemPrefab.LupineStag, _lupineStag},
			{ItemPrefab.ElectroBomber, _electroBomber},
			{ItemPrefab.SlimeWandering, _slimeWandering},
			{ItemPrefab.MinionSkeleton, _minionSkeleton},

			{ItemPrefab.BulletKillerBee, _bulletKillerBee},
			{ItemPrefab.BulletGlobloomSentry, _bulletGlobloomSentry},

			{ItemPrefab.SupportItem, _supportItem},

			{ItemPrefab.SkillFireBall, _skillFireBall},
			{ItemPrefab.ThunderStrike, _skillThunderStrike},
			{ItemPrefab.ThunderStrikeSmall, _skillThunderStrikeSmall},
			{ItemPrefab.SkillWaterBall, _skillWaterBall},
			{ItemPrefab.FireShield, _skillFireShield},
			{ItemPrefab.Shark, _skillShark},
			{ItemPrefab.PoisonBullet, _skillPoisonBullet},
			{ItemPrefab.EarthPunch, _skillEarthPunch},
			{ItemPrefab.SkyBoom, _skillSkyBoom},
			{ItemPrefab.Boomerang, _skillBoomerang},
			{ItemPrefab.SmilingFace, _skillSmilingface},
			{ItemPrefab.IceSpear, _skillIceSpear},
			{ItemPrefab.Twin, _skillTwin},
			{ItemPrefab.MinionWolf, _minionWolf},
			{ItemPrefab.OwlStriker, _owlStriker},
			{ItemPrefab.BulletOwlStriker, _bulletOwlStriker},
			{ItemPrefab.BulletGoblinswift, _bulletGoblinswift},
			{ItemPrefab.GoblinSwift, _goblinSwift},
			{ItemPrefab.Cyclone, _skillCyclone},
			{ItemPrefab.WindField, _skillWindfield},
			{ItemPrefab.ThunderPunch, _skillThunderPunch},
			{ItemPrefab.EarthShield, _skillEarthShield},
			{ItemPrefab.LightningWeb, _skillLightningWeb},
			{ItemPrefab.ThunderChanneling, _skillThunderChanneling},
			{ItemPrefab.TimeBoom, _skillTimeBoom},
			{ItemPrefab.MinionGoblin, _minionGoblin},
			{ItemPrefab.GatlingCrab, _gatlingCrab },
			{ItemPrefab.BulletBossGatlingCrab, _bulletBossGatlingCrab},
		};

		_mapDic = new Dictionary<MapType, Map>
		{
			{MapType.Meadow, _meadowMap},
			{MapType.Forest, _forestMap},
		};

		_typeMonsterDic = new Dictionary<string, GameObject>
		{
			{"M1", _swampOoze},
			{"M2", _killerBee},
			{"M3", _globloomSentry},
			{"M4", _lupineStag},
			{"M5", _electroBomber},
			{"M6", _slimeWandering},
			{"M7", _owlStriker},
            {"M8", _goblinSwift},

            {"Skeleton", _minionSkeleton},
			{"Wolf", _minionWolf},
            {"Goblin", _minionGoblin},

			{"B1", _gatlingCrab},

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