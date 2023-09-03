using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    [SerializeField] private bool _expandable;

    public List<GameObject> freeList;
    public List<GameObject> usedList;

    protected virtual void Awake()
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();
        for(int i = 0; i < _poolSize; ++i)
        {
            GenerateNewObject();
        }
    }


    //Get an object from the pool
    public GameObject GetObject(Vector3 position)
    {
        int totalFree = freeList.Count;

        if (totalFree == 0 && !_expandable) return null;
        else if (totalFree == 0)
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

    //Return an object to the pool
    protected void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }

    //Instantiate new GameObject
    private void GenerateNewObject()
    {
        GameObject game = Instantiate(_prefab, transform);
        game.SetActive(false);
        freeList.Add(game);
    }
}

public abstract class Pooler : ObjectPool
{
    public abstract void GetObjectFromPool(Vector3 position, params object[] arrObject);

    public abstract void RemoveObjectToPool(GameObject theGameObject, params object[] arrObject);
}
