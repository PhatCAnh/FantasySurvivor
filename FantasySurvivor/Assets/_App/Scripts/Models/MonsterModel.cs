using ArbanFramework.MVC;
using FantasySurvivor;

public class MonsterModel : UnitModel
{
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

    public MonsterModel(float movespeed, int maxHp) : base(movespeed, maxHp)
    {
        
    }
}
