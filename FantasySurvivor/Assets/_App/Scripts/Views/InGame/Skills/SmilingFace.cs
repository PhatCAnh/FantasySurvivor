using _App.Scripts.Controllers;
using FantasySurvivor;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class SmilingFace : SkillBulletActive
{
    [SerializeField] private GameObject _explosionEffect;

    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);

        if (target == null) return;

        targetPos = target.transform.position;

        this.direction = target.transform.position - transform.position;

        skin.up = direction;
        AudioManager.Instance.PlaySFXLoop("Smiling face loop");
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void HandleTouch()
    {


        if (Vector2.Distance(targetPos, transform.position) <= 0.5)
        {
            foreach (var unit in gameController.listMonster.ToList())
            {
                if (gameController.CheckTouch(unit.transform.position, transform.position, data.valueSpecial1 + unit.size))
                {
                    base.TouchUnit(unit);
                }
            }
            var explosion = Instantiate(_explosionEffect, transform.position, quaternion.identity);
            AudioManager.Instance.StopLoopingSFX();
            AudioManager.Instance.PlaySFX("Smiling face boom");
            explosion.transform.localScale = data.valueSpecial1 * Vector3.one;
            Destroy(gameObject);
        }

    }
    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, data.valueSpecial1);
    }
}
