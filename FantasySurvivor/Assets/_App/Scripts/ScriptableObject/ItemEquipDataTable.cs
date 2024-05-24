using System;
using System.Collections;
using System.Collections.Generic;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemEquipData", menuName = "ScriptableObjects/ItemEquipData", order = 2)]
public class ItemEquipDataTable: ScriptableObject
{
    public List<ItemEquipDataUI> listItemEquipData;
}

[Serializable]
public class ItemEquipDataUI
{
    public ItemEquipId id;
    public string itemName;
    public ItemEquipType type;
    public Sprite skin;
    public RankItemEquip rank;
}

public class ItemEquipData
{
    public readonly ItemEquipDataUI dataUi;
    public readonly ItemEquipStat dataStat;
    public readonly Sprite spriteRank;

    public ItemEquipData(ItemEquipDataUI dataUI, ItemEquipStat dataStat, Sprite spriteRank)
    {
        this.dataUi = dataUI;
        this.dataStat = dataStat;
        this.spriteRank = spriteRank;
    }
}

public class ItemEquipStat
{
    public string id;
    public DataItemEquipConfig dataStatConfig;

    public ItemEquipStat(DataItemEquipConfig dataStatConfig)
    {
        this.id = dataStatConfig.id;
        this.dataStatConfig = dataStatConfig;
    }
}

public class ItemETC
{
    
}
