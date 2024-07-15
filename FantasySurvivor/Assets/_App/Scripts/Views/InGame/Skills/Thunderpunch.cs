using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using UnityEditor;
using UnityEngine;
public class Thunderpunch : SkillBulletActive
{

    [SerializeField] private SkillActive _Instantiate;

    private bool check = false;
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
        AudioManager.Instance.PlaySFX("Thunder punch");
    }
    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
        TakeDamage(monster); 
        if (monster.isDead )
        {
            SpawnTwinBorn();
            check = false;
        }
        return true;
    }
    private void SpawnTwinBorn()
    {
        if (check) return;
        var bullet = Instantiate(_Instantiate, transform.position, new Quaternion(0, 0, 0, 0));
        bullet.GetComponent<ThunderPunchEx>().InitThunderPunchEx(damage / 2, level,data.valueSpecial3,data.valueSpecial2);
        Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
        check = true;
    }
}