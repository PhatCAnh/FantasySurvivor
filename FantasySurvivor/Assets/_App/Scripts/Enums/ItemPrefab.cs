using System;
using UnityEngine;
using UnityEngine.Serialization;
public enum ItemPrefab
{
    Undefined = -1,
    HealthBar,
    Character,
    TextPopup,
    GemExp,
}

public enum MapType
{
    Undefined = -1,
    Forest = 1,
    Ocean = 2,
}

public enum MonsterType
{
    BlueZombie = 0,
    PurpleZombie = 1,
    BlueGhost = 2,
}

public enum TowerType
{
    Basic = 0,
    Speed = 1,
    Tank = 2,
}

public enum TextPopupType
{
    MonsterDamage,
    TowerDamage,
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
    /* ThunderBird,*/
    Food,
    /*Shark,
    Twin,
    ZoneOfJudgment,
    BlackDrum,
    Passive1,
    */
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
}

public enum TargetType
{
    Target,
    Shot,
}




