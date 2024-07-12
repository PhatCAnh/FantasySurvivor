using System;
using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    private void Start()
    {
	    Singleton<GameController>.instance.StartGame();
    }
}
