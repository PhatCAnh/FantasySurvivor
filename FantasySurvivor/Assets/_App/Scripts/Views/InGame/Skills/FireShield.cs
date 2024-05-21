using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FireShield : SkillActive
{
    public float fireBallDuration = 4f; // Thời gian tồn tại của quả cầu lửa
    public float respawnDelay = 2f;     // Thời gian chờ tái tạo quả cầu lửa
    public Transform centerPoint;  // Điểm trung tâm của vòng tròn
    public float radius = 3f;      // Bán kính của vòng tròn
    public float duration;    // Thời gian để di chuyển một vòng
    public bool clockwise = true;  // Di chuyển theo chiều kim đồng hồ hay ngược lại
    private List<Monster> monsters = new List<Monster>();
    [SerializeField] private GameObject _FireShield;
    [SerializeField] private float _time;
    [SerializeField] private float speed;
    private List<GameObject> fireBalls = new List<GameObject>();

    private float angle = 0f;
    public override void Init(float damage, Monster target, int level)
    {
        base.Init(damage, target, level);

    }

    protected override void OnViewInit()
    {
        base.OnViewInit();
        MoveInCircle();
        centerPoint = gameController.character.transform;
    }



    private void FixedUpdate()
    {
        /*if (gameController.isStop) return;
        HandleTouch();*/
    }

    void Update()
    {
        // Kiểm tra giá trị của gameController.isStop
        /*if (gameController.isStop)
        {
            // Dừng Tween nếu isStop là true
            if (rotationTween != null && rotationTween.IsPlaying())
            {
                rotationTween.Pause();
            }
        }
        else
        {
            // Nếu isStop là false và Tween chưa được tạo hoặc không chạy, khởi động lại Tween
            if (rotationTween == null || !rotationTween.IsPlaying())
            {
                rotationTween.Play();
            }
        }*/
    }

    protected virtual void HandleTouch()
    {
        /*foreach (var mob in gameController.listMonster.ToList())
        {
            if (!monsters.Contains(mob) && gameController.CheckTouch(transform.position, mob.transform.position, 0.5f + mob.size))
            {
                TakeDamage(mob);
                monsters.Add(mob);
            }
        }*/
    }

    private void MoveInCircle()
    {


        //float angle = clockwise ? -360f : 360f;

        /*rotationTween = transform.DORotate(new Vector3(0, 0, angle), duration, RotateMode.FastBeyond360)
            .OnStepComplete(() =>
            {
                monsters.Clear();
            })
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart)
            .OnUpdate(UpdatePosition);*/

    }
    public void UpdatePosition(float initialAngle)
    {
        float radian1 = (initialAngle + radius) * Mathf.Deg2Rad;
        Vector3 offset1 = new Vector3(
            radius * Mathf.Cos(radian1),
            radius * Mathf.Sin(radian1),
            0
        );
        transform.position = centerPoint.position + offset1;
    }

}
