using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using TMPro;
using UnityEngine;

public class ThunderChanneling : SkillBulletActive
{
    [SerializeField] private float maxRange;

    private float channelingTime;

    private bool check = false;

    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFX("LightningChanneling");

        channelingTime = data.valueSpecial1;

        if (target == null) return;

        this.direction = target.transform.position - transform.position;

        skin.up = direction;

        callBackDamaged += SpawnChanneling;

        check = false;
    }
    private void SpawnChanneling()
    {
        if (check) return;
        Monster nearestMonster = gameController.FindNearestMonster(target.transform.position, maxRange);
        if (nearestMonster != null)
        {
            var Channeling = new Channeling(nearestMonster, damage/2, 0.1f,level, channelingTime - 1);
        }
        else 
        {
            var Channeling = new Channeling(target,damage/2, 0.1f, level, channelingTime - 1);
        }
        Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
        check = true;
    }
}