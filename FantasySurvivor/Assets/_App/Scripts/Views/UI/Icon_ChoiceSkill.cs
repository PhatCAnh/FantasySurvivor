

using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using UnityEngine.UI;

public class Icon_ChoiceSkill : View<GameApp>
{
    public SkillId id;
    public Image image;
    public Toggle toggle;
    private PopupChoiceSkill parent;
    private SkillData _skillData;

    private GameController gameController => Singleton<GameController>.instance;
    private SkillController skillController => Singleton<SkillController>.instance;

    public void Init(SkillId id, PopupChoiceSkill parent, bool checkSKillSet)
    {
        this.id = id;
        this.parent = parent;
        _skillData = skillController.GetDataSkill(id).skillDataUI;
        toggle.isOn = checkSKillSet;
        if(checkSKillSet)
        {
            skillController.GetListSkillInGame().Add(id);
            parent.UpdateTextNumberChoiceSkill(true);
        }
    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = _skillData.imgUI;
        toggle.onValueChanged.AddListener(OnClickTgl);
        
    }
    private void OnClickTgl(bool value)
    {
        parent.UpdateTextNumberChoiceSkill(value);
        if(value)
        {
            skillController.AddSkillSet(id);
        }
        else
        {
            skillController.RemoveSkillSet(id);
        }
        if (gameController.currentNumberSkill > gameController.numberLimitChoiceSkill)
        {
            toggle.isOn = false;
            return;
        }
    }
}