using ArbanFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterWandering : Monster
{

    [SerializeField]
    float range;
    [SerializeField]
    float maxDistance;

    private GameController gameController => ArbanFramework.Singleton<GameController>.instance;
    private bool isFirstMove = true;

    Vector3 wayPoint;

    public override void Attack()
    {

    }

    protected override void HandlePhysicUpdate()
    {
        moveTarget = gameController.character.transform.position;
        moveDirection = moveTarget - transform.position;

        if (isFirstMove)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            wayPoint = moveTarget + (Vector3)(randomDirection * maxDistance);
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, model.moveSpeed * Time.deltaTime);
            isFirstMove = !gameController.CheckTouchCharacter(wayPoint, 15);
        } else {
            transform.position = Vector2.MoveTowards(transform.position, wayPoint, model.moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, wayPoint) < range)
            {
                SetNewDestination();
            }
        }

        if (moveDirection.magnitude > 25)
        {
            transform.position = gameController.RandomPositionSpawnMonster(20);
            SetNewDestination();
        }
        else
        {
            MoveState();
        }
        SetAnimation(idleDirection);
    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance),
                               transform.position.y + Random.Range(-maxDistance, maxDistance));
    }
}
