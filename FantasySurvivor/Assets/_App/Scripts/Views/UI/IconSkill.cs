using System.Collections;
using System.Collections.Generic;
using ArbanFramework.MVC;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEngine.UI;


public class IconSkill : View<GameApp>
{
    public SkillData skillData;
    public Image image;
    public Toggle toggle;

    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = skillData.imgUI;
        toggle.isOn = !skillData.canAppear;
        toggle.onValueChanged.AddListener(OnClickTgl);
    }
    
    private void OnClickTgl(bool value)
    {
        skillData.canAppear = !value;
    }
}
