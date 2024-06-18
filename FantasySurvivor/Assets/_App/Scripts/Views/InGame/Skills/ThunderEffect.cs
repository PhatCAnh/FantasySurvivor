using _App.Scripts.Controllers;
using FantasySurvivor;
using UnityEngine;
public class ThunderEffect : Effect
{
    SkillActive _skillActive;
    [SerializeField] private SkillActive _Instantiate;
    protected override void OnViewInit()
    {
        base.OnViewInit();
        _skillActive = GetComponentInParent<SkillActive>();
    }
    private void Expolsive()
    {
        var bullet2 = Instantiate(_skillActive, new Vector3(transform.position.x, transform.position.y), new Quaternion(0, 0, 0, 0));
        bullet2.GetComponent<ThunderPunchEx>().
        InitThunderPunchEx(
           _skillActive.data.value / 10,
           _skillActive.level,
           _skillActive.size,
           _skillActive.data.valueSpecial2
           );
    }
    protected override void DoneEffect()
    {
        Destroy(gameObject);
        if (_skillActive.level == 6)
        {
            var bullet2 = Instantiate(_Instantiate, new Vector3(transform.position.x, transform.position.y - 1), new Quaternion(0, 0, 0, 0));
            bullet2.GetComponent<ThunderPunchEx>().
                InitThunderPunchEx(
                _skillActive.data.value / 10,
                _skillActive.level,
                _skillActive.size,
                _skillActive.data.valueSpecial2
                );
        }
    }
}
