using ArbanFramework.MVC;

public class CharacterUIPopup : View<GameApp>, IPopup
{
    
    
    public void Open()
    {
        
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}