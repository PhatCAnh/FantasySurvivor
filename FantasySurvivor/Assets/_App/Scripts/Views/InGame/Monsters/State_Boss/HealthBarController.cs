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

    private Vector2 fixedScreenPosition = new Vector2(0.50f, 0.80f);

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

        SetFixedPosition();

    }


    private void SetFixedPosition()
    {
        // Chuyển đổi vị trí cố định từ tỷ lệ màn hình sang tọa độ pixel
        Vector3 screenPosition = new Vector3(fixedScreenPosition.x * Screen.width, fixedScreenPosition.y * Screen.height, 0);
        transform.position = screenPosition;
    }

    public void RemoveHealthBar()
    {
        Destroy(gameObject);
    }

    public void Init(Monster monster)
    {
        _monster = monster;

        SetFixedPosition();    
    }
}
