using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool
{
	private readonly GameObject _prefab;
	public readonly Transform parent;

	public List<GameObject> freeList;
	public List<GameObject> usedList;

	public ObjectPool(GameObject prefab, Transform parent)
	{
		_prefab = prefab;
		this.parent = parent;

		freeList = new List<GameObject>();
		usedList = new List<GameObject>();

		GenerateNewObject();
	}

	//Get an object from the pool
	public GameObject GetObject(Vector3 position)
	{
		int totalFree = freeList.Count;

		if(totalFree == 0)
		{
			GenerateNewObject();
			totalFree = 1;
		}

		GameObject g = freeList[totalFree - 1];
		freeList.RemoveAt(totalFree - 1);
		usedList.Add(g);
		g.transform.position = position;
		g.SetActive(true);
		return g;
	}

	public void ReturnAllObject()
	{
		foreach(var item in usedList.ToList())
		{
			if(item != null)
			{
				item.SetActive(false);
				freeList.Add(item);
			}
			usedList.Remove(item);
		}
	}

	//Return an object to the pool
	public void ReturnObject(GameObject obj)
	{
		obj.SetActive(false);
		usedList.Remove(obj);
		freeList.Add(obj);
	}

	//Instantiate new GameObject
	private void GenerateNewObject()
	{
		GameObject game = GameObject.Instantiate(_prefab, parent);
		game.SetActive(false);
		freeList.Add(game);
	}
}