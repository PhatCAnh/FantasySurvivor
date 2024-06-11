using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using UnityEngine;
public class LightningWeb : SkillBulletActive
{
    [SerializeField] private SkillActive _Instantiate;

    private bool check = false;
    public override void Init(LevelSkillData data, Monster target, int level, ItemPrefab type)
    {
        base.Init(data, target, level, type);
    }
    protected override bool CheckTouchMonsters(Monster monster)
    {
        sizeTouch = size + monster.size;
        if (!gameController.CheckTouch(monster.transform.position, transform.position, sizeTouch)) return false;
            SpawnTwinBorn();
        return true;
    }
    private void SpawnTwinBorn()
    {
        var bullet = Instantiate(_Instantiate, transform.position, new Quaternion(0, 0, 0, 0));
        bullet.GetComponent<LightningWebWeb>().InitThunderWeb(damage / 2, level, data.valueSpecial1, data.valueSpecial2, data.valueSpecial3);
        Singleton<PoolController>.instance.ReturnObject(this.type, gameObject);
    }
}
