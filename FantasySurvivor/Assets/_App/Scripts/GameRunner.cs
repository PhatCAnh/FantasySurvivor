using ArbanFramework.MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : AppRunner<GameApp>
{
    private GameApp myGame;
    protected override GameApp CreateApp()
    {
        myGame = new GameApp();
        return myGame;
    }

    public GameApp GetApp()
    {
        return myGame;
    }
}
