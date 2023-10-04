using System;
using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Crossed : View<GameApp>
{
    [SerializeField] private Rigidbody2D _theRb;
    [SerializeField] private GameObject _skin;
    

    [SerializeField] private Vector3 _target;

    [SerializeField] private GameObject _bullet;

    public Vector2 vector2;
    
    // Start is called before the first frame update
    protected override void OnViewInit()
    {
        base.OnViewInit();
        if(vector2.Equals(Vector2.zero))
        {
            vector2 = _target - transform.position;
        }
        _skin.transform.up = vector2;
    }

    public void Init(Vector2 vector2)
    {
        this.vector2 = vector2;
        _target = this.vector2 * 10;
    }

    private void FixedUpdate()
    {
        transform.Translate(10f * Time.fixedDeltaTime * vector2.normalized);
        if(Vector2.Distance(_target, transform.position) < 0.1f)
        {
            var bullet = Instantiate(_bullet, transform.position, quaternion.identity);
            bullet.GetComponent<Crossed>().Init(new Vector2(-vector2.y, vector2.x));
            var bullet2 = Instantiate(_bullet, transform.position, quaternion.identity);
            bullet2.GetComponent<Crossed>().Init(new Vector2(vector2.y, -vector2.x));
            Destroy(gameObject);
        }
    }
}
