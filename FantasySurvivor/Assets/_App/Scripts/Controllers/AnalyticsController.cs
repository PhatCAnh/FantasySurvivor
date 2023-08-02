using ArbanFramework;
using ArbanFramework.MVC;

public class AnalyticsController : Controller<GameApp>
{
    private void Awake()
    {
        Singleton<AnalyticsController>.Set(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Singleton<AnalyticsController>.Unset(this);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
            
    }
}

