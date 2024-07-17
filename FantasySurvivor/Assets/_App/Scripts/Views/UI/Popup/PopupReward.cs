using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupReward : View<GameApp>, IPopup
{
	[SerializeField] private Transform _container;
	
	[SerializeField] private Button _btnOk;

	[SerializeField] private GameObject _pfItemSlot;
	
	private List<ItemSlotUI> _listItem;
	
	public void Init(List<ItemInBag> listData)
	{
		var listItem = new List<ItemSlotUI>();

		for(int i = 0; i < listData.Count; i++)
		{
			Instantiate(_pfItemSlot, _container).TryGetComponent(out ItemSlotUI itemData);
			itemData.Init(listData[i]);
		
			listItem.Add(itemData);
		}
		
		_btnOk.onClick.AddListener(Close);
		
		_listItem = listItem;

		Open();
	}
	

	public void Open()
	{
		foreach(var item in _listItem)
		{
			item.transform.localScale = Vector3.zero;
		}
		
		_btnOk.transform.localScale = Vector3.zero;

		var sequence = DOTween.Sequence();
		
		foreach(var item in _listItem)
		{
			sequence.Append(item.transform.DOScale(Vector3.one, 0.35f));
		}

		sequence.Append(_btnOk.transform.DOScale(Vector3.one, 0.35f));
	}
	
	
	public void Close()
	{
		Destroy(gameObject);
	}
}
