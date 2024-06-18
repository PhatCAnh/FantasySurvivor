using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;

public class Dead : View<GameApp>
{
    private Monster mons;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        mons = transform.parent.GetComponent<Monster>();
    }

    private void MonsterDeadEffect()
    {
        Singleton<PoolController>.instance.ReturnObject(mons.type, mons.gameObject);
    }
}
