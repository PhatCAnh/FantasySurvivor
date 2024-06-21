using System;
using UnityEngine;
using UnityEngine.Serialization;
public enum ItemPrefab
{
	Undefined,

    SwampOoze,
    KillerBee,
    GlobloomSentry,
    LupineStag,
    ElectroBomber,
	SlimeWandering,
    MinionSkeleton,
    BulletKillerBee,
    BulletGlobloomSentry,
    SkillFireBall,
	ThunderStrikeSmall,
	ThunderStrike,
	SkillWaterBall,
	FireShield,
	Shark,
	PoisonBullet,
	EarthPunch,
	SkyBoom,
	Boomerang,
	SmilingFace,
	IceSpear,
	Twin,
	HealthBar,
	Character,
	TextPopup,
	GemExp,
	SupportItem,
	BulletOwlStriker,
	OwlStriker,
	MinionWolf,
	BulletGoblinswift,
	GoblinSwift,
	Cyclone,
    WindField,
	ThunderPunch,
    EarthShield,
    LightningWeb,
    ThunderChanneling,
    TimeBoom,
	MinionGoblin,
	GatlingCrab,
    BulletBossGatlingCrab,
	GatlingCrab_HealthBar,

}

public enum MapType
{
	Undefined = -1,
	Forest = 1,
	Meadow = 2,
	Ocean = 3,
}

public enum MonsterType
{
	
}

public enum TextPopupType
{
    Healing,
    Red,
    Normal,
    Fire,
	Poison,
	Lightning
}

public enum TypeStatTower
{
	AttackDamage,
	AttackRange,
	AttackSpeed,
	CriticalRate,
	CriticalDamage,
	Health,
	RegenHp,
}

public enum TypeItemReward
{
	Coin,
}

public enum LevelResult
{
	Start,
	Failure,
	Completed,
	Exit,
}

public enum TypeAds
{
	Interstitial,
	Reward,
	Banner,
}

public enum SkillName
{
	Fireball,
	ThunderStrike,
	Waterball,
	Food,
	ThunderChanneling,
	FireShield,
	Shark,
	ThunderBird,
	PoisonBullet, 
	Earthpunch,
	SkyBoom,
	Boomerang,
    SmilingFace,
	IceSpear,
	Twin,
	Cyclone,
	WindField,
	ThunderPunch,
    EarthShield,
    LightningWeb,
    TimeBoom,
    Swamp,
    BlackDrum,
}

public enum SkillType
{
	Active,
	Passive,
	Buff,
}

public enum SkillElementalType
{
	/*Water,
	Fire,
	Wind,
	Dark,
	Electric,
	Light,*/
	Fire,
	Water,
	Thunder,
	Wind,
	Wood,
}

public enum PopupType
{
	MainUI,
	Notification,
	Pause,
	WinGame,
	LoseGame,
	ChoiceMap,
	MainInGame,
	ClickBulletTutorial,
	HealthBar,
	Cheat,
	ChoiceSkill,
	ChoiceSkillOutGame,
	CharacterInformation,
	CharacterChoose,
	ItemEquipDetail,
	ItemPieceDetail,
    GatlingCrab_HealthBar,
}

public enum SkillDamagedType
{
	Single,
	AreaOfEffect,
}

public enum DropItemType
{
	Exp,
	Magnet,
	Bomb,
	Food,
}

public enum MapHorizontalType
{
	Top = 0,
	Mid = 1,
	Bot = 2,
}

public enum MapVerticalType
{
	Left = 0,
	Mid = 1,
	Right = 2,
}

public enum SpawnPos
{
	Character,
	Monster,
	OldBullet,
	Other,
}

public enum TargetType
{
    Target,
    Shot,
    None,
}
