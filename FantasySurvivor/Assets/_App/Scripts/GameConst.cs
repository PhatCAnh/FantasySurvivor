using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameConst
{
    public static int BLOCK_SIZE = 18;
    public static float MIN_ASSIGN_RANGE = 1.5f;
    public static int FINAL_SORTING = -100;
    public static int TIME_SHOW_INTERTITIAL => Singleton<GameApp>.instance.configs.constConfig.GetValueInt(nameof(TIME_SHOW_INTERTITIAL));
    public static int LEVEL_SHOW_ADS => Singleton<GameApp>.instance.configs.constConfig.GetValueInt(nameof(LEVEL_SHOW_ADS));

    public static int VERSION_ID = 2;
    
    public static float MOVE_SPEED_ANIMATION_RATIO = 2.5f;
    public static float SPEED_UP_VALUE = 2.25f;
    public static float DASH_DELAY = 1f;

    public static int gemStartGame = 100;

    public static string iconCoin = "<sprite index=0>";
    public static string iconAd = "<sprite index=1>";
    public static string iconAr = "<sprite index=2>";
    public static string iconAs = "<sprite index=3>";
    public static string iconHealth = "<sprite index=4>";
    public static string iconExp = "<sprite index=5>";
    public static string iconCritical = "<sprite index=6>";

    public static string nameScene_Loading = "scn_Loading";
    public static string nameScene_Main = "scn_Main";
    public static string nameScene_Game = "scn_Game";

    public static string GetNameWithVersion(string str)
    {
        return $"{str}_{VERSION_ID}";
    }
}

