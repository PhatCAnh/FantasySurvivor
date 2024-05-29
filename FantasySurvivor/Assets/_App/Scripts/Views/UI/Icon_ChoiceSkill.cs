

using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine.UI;

public class Icon_ChoiceSkill : View<GameApp>
{
    public SkillData skillData;
    public Image image;
    public Toggle toggle;
    private PopupChoiceSkill parent;

    private GameController gameController => Singleton<GameController>.instance;

    public void Init(SkillData skillData, PopupChoiceSkill parent)
    {
        this.skillData = skillData;
        this.parent = parent;

    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = skillData.imgUI;
        toggle.isOn = skillData.ChoiceSkill;
        toggle.onValueChanged.AddListener(OnClickTgl);
        
    }
    private void OnClickTgl(bool value)
    {
        skillData.ChoiceSkill = value;
        parent.UpdateTextNumberChoiceSkill(value);
        if (gameController.currentNumberSkill > gameController.numberLimitChoiceSkill)
        {
            toggle.isOn = false;
            return;
        }
       
    }
}