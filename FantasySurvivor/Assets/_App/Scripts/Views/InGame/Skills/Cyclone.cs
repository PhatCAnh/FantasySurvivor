using ArbanFramework;
using FantasySurvivor;
using UnityEngine;

public class Cyclone : SkillBulletActive
{
    private int Count = 0;
    private Cooldown cdTime;
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFX("Cyclone");
    }

    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
        TakeDamage(monster);
        if (monster.isDead)
        {
            SpeedUP();
        }
        return true;
    }
    private void Update()
    {
        if (gameController.isStop) return;
        if (Count == 3 || data.cooldown <= 0.5f)
        {
            cdTime.Update(Time.deltaTime);
        }
        if (cdTime.isFinished)
        {
            cdTime.Restart();
        }
    }

    private void SpeedUP()
    {
        if (origin.model.moveSpeed == origin.stat.moveSpeed.BaseValue)
        {
            origin.UpdateStat(StatModifierType.Mul, 1, data.valueSpecial1, 1, 1, 1, 1, 1, data.valueSpecial2);
        }
        if (Count < 3 && data.cooldown > 0.5f && level == 6)
        {
            data.cooldown -= 0.5f;
            Count++;
        }
        else
        {
            if (Count == 3)
            {
                data.cooldown += (Count * 0.5f);
            }
            Count = 0;
        }
    }
}
