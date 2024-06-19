using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArbanFramework.MVC;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoose : View<GameApp>
{
    [SerializeField] private Button _btnBack;
  //  [SerializeField] private GameObject _characterInforPrefab;
    //protected override async void Start()
    //{
    //    base.Start();
    //   // await Task.Delay(500); // Properly await the delay
        
    //}

    protected override void OnViewInit()
    {
        //base.OnViewInit();
        _btnBack.onClick.AddListener(Close);
    }
        public void Close()
        {
            Destroy(gameObject);
        }
    } 
