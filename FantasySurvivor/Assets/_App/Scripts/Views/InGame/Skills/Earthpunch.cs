using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System;
using System.Linq;

public class Earthpunch : SkillBulletActive
{
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
    }
    private static Random random = new Random();

    public static bool IsActionSuccessful(double probability)
    {
        return random.NextDouble() < probability;
    }
    protected override void HandleTouch()
    {
        if (gameController.CheckTouch(targetPos, transform.position, sizeTouch))
        {
            TakeDamage();
            Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
        }
        
        if (!gameController.CheckTouch(origin.transform.position, transform.position, 30))
        {
            Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
    }
}
protected override bool CheckTouchMonsters(Monster monster)
{
    sizeTouch = size + monster.size;

    if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
    TakeDamage(monster);
    if (IsActionSuccessful(data.valueSpecial2))
    {
        monster.UpdateStat(StatModifierType.Mul, 1, 0, 1, 1, data.valueSpecial1);
    }
    return true;
}
}
