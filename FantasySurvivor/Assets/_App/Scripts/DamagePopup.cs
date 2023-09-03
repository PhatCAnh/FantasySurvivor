using System.Collections;
using System.Collections.Generic;
using ArbanFramework;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMesh;
    
    [SerializeField] private int _fontSize;

    private const float DisappearTimeMax = 1f;
    
    private float _disappearTimer;
    
    private Color _textColor;

    private Vector3 _moveVector;
    
    private static int _sortingOrder;

    public void Setup(int dmgAmount, bool isCriticalHit)
    {
        if(dmgAmount != 0)
        {
            _textMesh.SetText(dmgAmount.ToString());
        }
        else
        {
            _textMesh.SetText("Miss");
        }

        if(!isCriticalHit)
        {
            _textMesh.fontSize = _fontSize;
            //_textMesh.color = new Color(0.8207547f, 0, 0.007291546f);
        }
        else
        {
            _textMesh.fontSize = _fontSize * 2f;
            //textMesh.color = new Color(1, 0.8128629f, 0.1367925f);
        }
        _sortingOrder++;
        _textMesh.sortingOrder = _sortingOrder;
        _textColor = _textMesh.color;
        _disappearTimer = DisappearTimeMax;
        _moveVector = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0f) * 10f;
    }

    public DamagePopup Create(Transform dmgPopupTransform, int dmgAmount, bool isCriticalHit)
    {
        DamagePopup dmgPopup = dmgPopupTransform.GetComponent<DamagePopup>();
        dmgPopup.Setup(dmgAmount, isCriticalHit);
        return dmgPopup;
    }

    private void Update()
    {
        transform.position += _moveVector * Time.deltaTime;
        _moveVector -= 8f * Time.deltaTime * _moveVector;

        if(_disappearTimer > DisappearTimeMax * 0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= decreaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        _disappearTimer -= Time.deltaTime;
        if(_disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            _textColor.a -= disappearSpeed * Time.deltaTime;
            _textMesh.color = _textColor;

            if(_textColor.a < 0)
            {
                Singleton<PoolDamagePoint>.instance.RemoveObjectToPool(gameObject, _textMesh);
            }
        }
    }
}
