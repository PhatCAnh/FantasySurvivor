using _App.Scripts.Controllers;
using ArbanFramework;
using FantasySurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoomEffect : Effect
{
    SkillActive _skillActive;
    [SerializeField] private SkillActive _burnInstantiate;
    protected override void OnViewInit()
    {
        base.OnViewInit();
        _skillActive = GetComponentInParent<SkillActive>();
    }
    protected virtual void Attack()
    {
        _skillActive.TakeDamage();
    }
    protected override void DoneEffect()
    {
        Destroy(gameObject);
        if (_skillActive.level == 6)
        {
            var bullet2 = Instantiate(_burnInstantiate, new Vector3(transform.position.x,transform.position.y-1) , new Quaternion(0, 0, 0, 0));
        bullet2.GetComponent<FireBurnArea>().
            InitFireArea(
            _skillActive.data.value/10,
            _skillActive.level,
            _skillActive.size,
            _skillActive.data.valueSpecial3,
            _skillActive.data.valueSpecial2
            );
        }
    }
}