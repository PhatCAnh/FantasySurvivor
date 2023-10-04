using System;
using UnityEngine;
public enum ItemType
{
    Undefined = -1,
    HealthBar,
    Character,
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

public enum SkillType
{
    SharkSkill,
    FireBallSkill,
    TwinSkill,
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

[Serializable]
public class SkillData
{
    public SkillType type;
    public string skillName;
    public Sprite imgUI;
    public string description;
    public GameObject skillPrefab;
}
