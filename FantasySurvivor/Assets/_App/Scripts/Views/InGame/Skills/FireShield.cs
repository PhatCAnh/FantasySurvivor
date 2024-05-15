using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FireShield : SkillActive
{
    public float radius = 5f;      // Bán kính vòng tròn
    public float speed = 1f;       // Tốc độ di chuyển
    private float angle = 0f;      // Góc ban đầu
    [SerializeField] GameObject FireShieldPre;
    public bool canBlock;

    

    private void FixedUpdate()
    {
        float x = origin.transform.position.x + radius * Mathf.Cos(angle);
        float y = origin.transform.position.y + radius * Mathf.Sin(angle);
        transform.position = new Vector3(x, y, transform.position.z);
        angle += speed * Time.deltaTime;
    }

    protected virtual void HandleTouch()
    {
        if (!canBlock)
        {

        }
        
    }

}
