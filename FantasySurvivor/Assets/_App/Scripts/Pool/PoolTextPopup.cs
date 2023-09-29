using ArbanFramework;
using TMPro;
using UnityEngine;
namespace _App.Scripts.Pool
{
    public class PoolTextPopup : ObjectPool
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

        public void GetObjectFromPool(Vector3 position, string value, TextPopupType type, bool isCritical = false)
        {
            GetObject(position).GetComponent<TextPopup>().Create(value, type, isCritical);
        }

        public void RemoveObjectToPool(GameObject theGameObject, params object[] arrObject)
        {
            TextMeshPro textMesh = (TextMeshPro) arrObject[0];
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
            textMesh.transform.localScale = Vector3.one;
            ReturnObject(theGameObject);
        }
    }
}

