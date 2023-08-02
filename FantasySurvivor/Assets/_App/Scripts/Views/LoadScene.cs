using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadScene : MonoBehaviour
{
    private void Start()
    {
        var load = SceneManager.LoadSceneAsync("scn_Main", LoadSceneMode.Single);
        load.completed += o => Singleton<GameController>.instance.ShowChoiceMap();
    }
}
