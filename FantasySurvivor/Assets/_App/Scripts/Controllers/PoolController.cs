using System;
using System.Collections.Generic;
using System.Linq;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine;
namespace _App.Scripts.Controllers
{
	public class PoolController : Controller<GameApp>
	{
		private Dictionary<ItemPrefab, ObjectPool> _dictionaryPool;

		private void Awake()
		{
			Singleton<PoolController>.Set(this);
		}

		private void Start()
		{
			_dictionaryPool = new();
		}
		
		protected override void OnDestroy()
		{
			base.OnDestroy();
			Singleton<PoolController>.Unset(this);
		}

		private ObjectPool CreatePool(ItemPrefab key)
		{
			var op = new ObjectPool(
				app.resourceManager.GetItemPrefab(key),
				Instantiate(new GameObject(key.ToString()),transform).transform);
			_dictionaryPool.Add(key, op);
			return op;
		}

		public GameObject GetObject(ItemPrefab key, Vector3 position, bool willDestroy = true)
		{
			return _dictionaryPool.TryGetValue(key, out var pool) ? pool.GetObject(position) : CreatePool(key).GetObject(position);
		}
		
		public ObjectPool GetPool(ItemPrefab key, bool willDestroy = false)
		{
			return _dictionaryPool.TryGetValue(key, out var pool) ? pool : CreatePool(key);
		}
		
		public void ReturnObject(ItemPrefab key, GameObject objectReturn)
		{
			_dictionaryPool[key].ReturnObject(objectReturn);
		}

		public void RemoveAllObject(ItemPrefab key)
		{
			if(_dictionaryPool.TryGetValue(key, out var value))
			{
				value.ReturnAllObject();
			}
		}
	}
}