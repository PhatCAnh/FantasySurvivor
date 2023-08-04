using ArbanFramework.MVC;

public class MonsterModel : Model<GameApp>
{
    public static EventTypeBase dataChangedEvent = new EventTypeBase(nameof(MonsterModel) + ".dataChanged");

    public MonsterModel(EventTypeBase eventType) : base(dataChangedEvent)
    {
    }

    public MonsterModel() : base(dataChangedEvent)
    {
    }

    public MonsterModel(float moveSpeed) : base(dataChangedEvent)
    {
        this.moveSpeed = moveSpeed;
    }

    private float _moveSpeed;

    public float moveSpeed
    {
        get => _moveSpeed;
        set
        {
            if (moveSpeed != value)
            {
                _moveSpeed = value;
                RaiseDataChanged(nameof(moveSpeed));
            }
        }
    }
}
