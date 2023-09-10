using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using TMPro;
using UnityEngine;

public class PoolTextPopup : Pooler
{
    protected override void Awake()
    {
        base.Awake();
        Singleton<PoolTextPopup>.Set(this);
    }

    protected void OnDestroy()
    {
        Singleton<PoolTextPopup>.Unset(this);
    }

    public override void GetObjectFromPool(Vector3 position, params object[] arrObject)
    {
        var gObject = GetObject(position);
        gObject.GetComponent<TextPopup>().Create(gObject.transform, arrObject[0].ToString());
    }

    public override void RemoveObjectToPool(GameObject theGameObject, params object[] arrObject)
    {
        TextMeshPro textMesh = (TextMeshPro) arrObject[0];
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
        textMesh.transform.localScale = Vector3.one;
        ReturnObject(theGameObject);
    }
}

