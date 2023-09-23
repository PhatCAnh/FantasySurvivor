using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using ArbanFramework.MVC;
using FantasySurvivor;
using UnityEngine;
using UnityEngine.UI;

public class CheatPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnCoin, _btnGem, _btnDeleteData, _btnClose;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        
        _btnCoin.onClick.AddListener(OnClickBtnCoin);
        _btnGem.onClick.AddListener(OnClickBtnGem);
        _btnDeleteData.onClick.AddListener(OnClickBtnDeleteData);
        _btnClose.onClick.AddListener(Close);
        
        Open();
    }

    public void Open()
    {
    }
    public void Close()
    {
        Destroy(gameObject);
    }

    private void OnClickBtnCoin()
    {
        app.models.dataPlayerModel.coin += 1000;
        app.models.WriteModel<DataPlayerModel>();
    }
    
    private void OnClickBtnGem()
    {
        GameConst.gemStartGame += 1000;
    }
    
    private void OnClickBtnDeleteData()
    {
        GameConst.gemStartGame = 100;
        PlayerPrefs.DeleteAll();
        app.models.dataPlayerModel.InitBaseData();
        app.resourceManager.CloseAllPopup();
        Singleton<GameController>.instance.ChangeSceneHome();
    }
}
