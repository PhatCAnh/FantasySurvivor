using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : View<GameApp>
{
    [SerializeField] private TextMeshProUGUI _txtHealth;
    [SerializeField] private TextMeshProUGUI _txtCoin;
    [SerializeField] private TextMeshProUGUI _txtGem;
    [SerializeField] private TextMeshProUGUI _txtMovespeed;
    [SerializeField] private TextMeshProUGUI _txtAtkdamage;
    [SerializeField] private TextMeshProUGUI _txtArmor;
    protected override void OnViewInit()
    {
        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-healthValue", _txtHealth, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Health}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Health), app.models.dataPlayerModel)
        );


        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-gemValue", _txtGem, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Gem}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Gem), app.models.dataPlayerModel)
        );

        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-coinValue", _txtCoin, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Coin}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Coin), app.models.dataPlayerModel)
        );

        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-movespeedValue", _txtMovespeed, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Movespeed}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Movespeed), app.models.dataPlayerModel)
        );

        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-atkdamageValue", _txtAtkdamage, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Atkdamage}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Atkdamage), app.models.dataPlayerModel)
        );

        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-armorValue", _txtArmor, (control, e) => {
            float armorPercent = (float)app.models.dataPlayerModel.Armor / 100f;
            control.text = $"{armorPercent * 100f}%"; 
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Armor), app.models.dataPlayerModel));


    }
}
