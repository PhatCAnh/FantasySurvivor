using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using UnityEngine;

public class LoadingPopup : View<GameApp>, IPopup
{
    [SerializeField] private GameObject _goBackground;

    public void Init(bool isHaveBackground = false)
    {
        _goBackground.SetActive(isHaveBackground);
    }
    
    public void Open()
    {
        
    }

    public void Close()
    {
    }
}
