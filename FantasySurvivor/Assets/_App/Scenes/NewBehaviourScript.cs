using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform object1;       // Đối tượng thứ nhất
    public Transform object2;       // Đối tượng thứ hai
    public Transform centerPoint;   // Điểm trung tâm cố định
    public float radius = 5f;       // Bán kính quay
    public float duration = 5f;     // Thời gian để hoàn thành một vòng tròn
    public bool clockwise = true;   // Xoay theo chiều kim đồng hồ hay ngược lại

    private void Start()
    {
        RotateObjectsAroundPoint();
    }

    private void RotateObjectsAroundPoint()
    {
        // Tạo một trình tự DOTween để xoay đối tượng quanh điểm trung tâm
        float angleStep = (clockwise ? -360f : 360f) / duration;
        DOTween.To(() => 0f, value => UpdatePositions(value * angleStep), 1f, duration)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    private void UpdatePositions(float angle)
    {
        float radian1 = angle * Mathf.Deg2Rad;
        Vector3 offset1 = new Vector3(
            radius * Mathf.Cos(radian1),
            radius * Mathf.Sin(radian1),
            0
        );
        object1.position = centerPoint.position + offset1;

        float radian2 = (angle + 180f) * Mathf.Deg2Rad;
        Vector3 offset2 = new Vector3(
            radius * Mathf.Cos(radian2),
            radius * Mathf.Sin(radian2),
            0
        );
        object2.position = centerPoint.position + offset2;
    }
}
