using ArbanFramework.MVC;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : View<GameApp>
{
    [SerializeField] private TextMeshProUGUI _txtHealth;
    protected override void OnViewInit()
    {
        base.OnViewInit();
        AddDataBinding("fieldDataCharacter-healthValue", _txtHealth, (control, e) =>
        {
            control.text = $"{app.models.dataPlayerModel.Health}";
        }, new DataChangedValue(DataPlayerModel.dataChangedEvent, nameof(DataPlayerModel.Health), app.models.dataPlayerModel)
        );
    }
}
