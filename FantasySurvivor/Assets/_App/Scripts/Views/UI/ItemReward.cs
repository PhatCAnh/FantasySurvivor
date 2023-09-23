using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using TMPro;
using UnityEngine;

public class ItemReward : View<GameApp>
{
    public TextMeshProUGUI txtValue;

    public void Init(int value)
    {
        txtValue.text = $"+{value}";
    }
}
