using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Boomeerang : SkillBulletActive
{
    private enum MoveState { ToC, ToB, ToA }
    private MoveState currentState = MoveState.ToC;

    private Vector3 initialPosition; // Vị trí B (Ban đầu)
    private Vector3 targetPosition;  // Vị trí C (Quái vật)
    private Vector3 endPosition;     // Vị trí A (Kết thúc)

    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFXLoop("Boomerang");
        initialPosition = transform.position;
        Vector3 direction = (target.transform.position - initialPosition).normalized;
        targetPosition = initialPosition + direction * origin.model.attackRange;
        Vector3 directionBC = (targetPosition - initialPosition).normalized;
        endPosition = initialPosition - directionBC * Vector2.Distance(initialPosition, targetPosition);

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (gameController.isStop) return;
        switch (targetType)
        {
            case TargetType.Shot:
                float step = data.valueSpecial1;

                switch (currentState)
                {
                    case MoveState.ToC:
                        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
                        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
                        {
                            currentState = MoveState.ToB;
                            attackedMonsters.Clear();
                        }
                        break;

                    case MoveState.ToB:
                        transform.position = Vector2.MoveTowards(transform.position, initialPosition, step);
                        if (Vector2.Distance(transform.position, initialPosition) <= 0.01f)
                        {
                            currentState = MoveState.ToA;
                        }
                        break;

                    case MoveState.ToA:
                        transform.position = Vector2.MoveTowards(transform.position, endPosition, step);
                        if (Vector2.Distance(transform.position, endPosition) < 0.01f)
                        {
                            Destroy(gameObject);
                        }
                        break;
                }
                break;
        }

        HandleTouch();
    }

    protected override void HandleTouch()
    {
        foreach (var mons in gameController.listMonster.ToList())
        {
            if (!attackedMonsters.Contains(mons) && gameController.CheckTouch(mons.transform.position, transform.position, sizeTouch))
            {
                TouchUnit(mons);
                attackedMonsters.Add(mons);
            }
        }
    } 
}

