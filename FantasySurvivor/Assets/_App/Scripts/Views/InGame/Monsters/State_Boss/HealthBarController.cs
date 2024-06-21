using ArbanFramework;
using ArbanFramework.MVC;
using System.Collections;
using FantasySurvivor;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarController : View<GameApp>
{

    [SerializeField] private Slider _sldHealthPoint;

    [SerializeField] private TextMeshProUGUI _txtPoint;

    private Monster _monster;
    public GameController gameController => Singleton<GameController>.instance;

    public Monster monster => Singleton<Monster>.instance;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        var model = app.models.monsterModel;

        AddDataBinding("sldHealthBar-maxValue", _sldHealthPoint, (control, e) =>
        {
            _txtPoint.text = $"{_monster.model.currentHealthPoint} / {_monster.model.maxHealthPoint}";
            control.maxValue = _monster.model.maxHealthPoint;
        },
            new DataChangedValue(MonsterModel.dataChangedEvent, nameof(MonsterModel.maxHealthPoint), _monster.model)
        );

        AddDataBinding("sldHealthBar-value", _sldHealthPoint, (control, e) =>
        {
            _txtPoint.text = $"{_monster.model.currentHealthPoint} / {_monster.model.maxHealthPoint}";
            control.value = _monster.model.currentHealthPoint;
        },
            new DataChangedValue(MonsterModel.dataChangedEvent, nameof(MonsterModel.currentHealthPoint), _monster.model)
        );

    }

    private void LateUpdate()
    {
        if (gameController.isStop) return;
        transform.position = Camera.main.WorldToScreenPoint(Vector3.up * 2f + _monster.transform.position);
    }

    public void Init(Monster monster)
    {
        _monster = monster;
    }
}
