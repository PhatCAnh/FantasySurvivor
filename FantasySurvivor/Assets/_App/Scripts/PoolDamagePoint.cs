using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using TMPro;
using UnityEngine;

public class PoolDamagePoint : Pooler
{
    protected override void Awake()
    {
        Singleton<PoolDamagePoint>.Set(this);
    }

    protected void OnDestroy()
    {
        Singleton<PoolDamagePoint>.Unset(this);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetObjectFromPool(transform.position, 10, false);
        }
    }

    public override void GetObjectFromPool(Vector3 position, params object[] arrObject)
    {
        var a = GetObject(position);
        a.GetComponent<DamagePopup>().Create(a.transform, (int)arrObject[0], (bool)arrObject[1]);
    }

    public override void RemoveObjectToPool(GameObject theGameObject, params object[] arrObject)
    {
        TextMeshPro textMesh = (TextMeshPro) arrObject[0];
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
        textMesh.transform.localScale = Vector3.one;
        ReturnObject(theGameObject);
    }
}

