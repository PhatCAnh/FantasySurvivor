using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class HomeMainUI : View<GameApp>
{
    [SerializeField] private Button _btnDailyReward;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        _btnDailyReward.onClick.AddListener(OnClickBtnDailyReward);
    }

    private void OnClickBtnDailyReward()
    {
        app.adsController.ShowInterstitial();
    }
}
