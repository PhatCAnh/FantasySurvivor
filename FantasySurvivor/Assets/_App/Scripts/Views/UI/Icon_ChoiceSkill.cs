

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

    public void Init(SkillId id, PopupChoiceSkill parent)
    {
        this.id = id;
        this.parent = parent;
        _skillData = Singleton<SkillController>.instance.GetDataSkill(id).skillDataUI;

    }
    protected override void OnViewInit()
    {
        base.OnViewInit();
        image.sprite = _skillData.imgUI;
        toggle.isOn = _skillData.ChoiceSkill;
        toggle.onValueChanged.AddListener(OnClickTgl);
        
    }
    private void OnClickTgl(bool value)
    {
        _skillData.ChoiceSkill = value;
        parent.UpdateTextNumberChoiceSkill(value);
        var skillController = Singleton<SkillController>.instance;
        var listSkill = skillController.GetListSkillInGame();
        var skill = skillController.GetDataSkill(id);
        if(value)
        {
            listSkill.Add(id);
        }
        else
        {
            listSkill.Remove(id);
        }
        if (gameController.currentNumberSkill > gameController.numberLimitChoiceSkill)
        {
            toggle.isOn = false;
            return;
        }
       
    }
}