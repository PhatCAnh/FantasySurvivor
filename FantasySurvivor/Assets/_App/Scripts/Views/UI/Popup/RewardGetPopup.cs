using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RewardGetPopup : View<GameApp>, IPopup
{
    [SerializeField] private Button _btnDimer;

    [SerializeField] private GameObject _prefabItem, _container;

    private Sequence sequence;

    public void Init(List<ItemInBag> data)
    {
        sequence = DOTween.Sequence();
        
        foreach (var item in data)
        {
            Instantiate(_prefabItem, _container.transform).TryGetComponent(out ItemSlotUI itemSlotUI);
            itemSlotUI.Init(item);
        }
        
        _btnDimer.onClick.AddListener(Close);

        Open();
    }

    public void Open()
    {
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}