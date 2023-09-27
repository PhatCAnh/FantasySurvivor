﻿public enum ItemType
{
    Undefined = -1,
    HealthBar,
    Tower,
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
