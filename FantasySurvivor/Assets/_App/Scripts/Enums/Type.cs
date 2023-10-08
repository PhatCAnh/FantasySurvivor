using System;
using UnityEngine;
using UnityEngine.Serialization;
public enum Type
{
    Undefined = -1,
    HealthBar,
    Character,
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
    Shark,
    Fireball,
    Twin,
    Food,
}

public enum SkillType
{
    Proactive,
    Passive,
    Buff,
}

public enum SkillElementalType
{
    Water,
    Fire,
    Wind,
    Ground,
    Electric,
    Light,
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




