namespace ArbanFramework.Config
{
    public interface IConfigItem
    {
        void OnReadImpl(IConfigReader reader);
        string GetId();
    }
}