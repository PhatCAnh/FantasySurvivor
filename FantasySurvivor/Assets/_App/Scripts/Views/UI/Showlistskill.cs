using System.Collections;
using System.Collections.Generic;
using _App.Datas.DataScript;
using _App.Scripts.Controllers;
using ArbanFramework;
using ArbanFramework.MVC;
using DG.Tweening;
using FantasySurvivor;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class Showlistskill : View<GameApp>, IPopup
{
   
    [SerializeField] private Transform container,containerchoose, containerPopup;
    [SerializeField] private GameObject iconSkillPrefab;
    [SerializeField] private GameObject waringAddListSkill, skillDetail;
    [SerializeField] private Button closeBtn, saveBtn, okbtn;
    [SerializeField] private TextMeshProUGUI _txtName, _txtDescription, _txtValue, NumberTaget;
    
    private int currentNumberSkill = 0; // điếm số skill outr game
    private GameController gameController => Singleton<GameController>.instance;
    private SkillController skillController => Singleton<SkillController>.instance;
  
    protected override void OnViewInit()
    {
        base.OnViewInit();
        gameController._listSkill = skillController.GetListSkillDataTable();
        foreach (var skill in gameController._listSkill) // lấy list skill
        {
            Instantiate(iconSkillPrefab, container).TryGetComponent(out Icon_ListSkill icon);
            var id = skill.id;
            var result = CheckSkillSet(id);
            if (result) currentNumberSkill += 1;
            icon.Init(skill.id, this, result);
        }
        NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}";
        foreach (var skill in gameController._listSkill)
        {
            var id = skill.id;

            if (CheckSkillSet(id) == true)
            {
                Instantiate(iconSkillPrefab, containerchoose).TryGetComponent(out Icon_ListSkill icon);
                icon.ShowList(skill.id, this);
            }

        }
        
        closeBtn.onClick.AddListener(Close);
    }
    public void Open()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.35f);
    }

    public void Close()
    {
        waringAddListSkill.SetActive(false);
    }
   
    public bool UpdateTextNumberChoiceSkill(SkillId id)
    {
        if (CheckSkillSet(id))
        {
           
            currentNumberSkill -= 1;
            NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}";
            skillController.RemoveSkillSet(id);
            foreach (Transform child in containerchoose)
            {
                if (child.TryGetComponent(out Icon_ListSkill icon) && icon.id == id)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }
            return false;
        }

        if (currentNumberSkill < gameController.numberLimitChoiceSkill)
        {
            currentNumberSkill += 1;
            NumberTaget.text = $"{currentNumberSkill}/{gameController.numberLimitChoiceSkill}";
            skillController.AddSkillSet(id);
            Skilldetail(id);
            Instantiate(iconSkillPrefab, containerchoose).TryGetComponent(out Icon_ListSkill icon);
            icon.ShowList(id, this);
            return true;
        }
        if (currentNumberSkill == gameController.numberLimitChoiceSkill)
        {
            waringAddListSkill.SetActive(true);
            closeBtn.onClick.AddListener(Close);
            saveBtn.onClick.AddListener(Close);
        }
       
        return false;
    }

    private void Skilldetail(SkillId id)
    {


        skillDetail.SetActive(true);
        _txtName.text = skillController.GetDataSkill(id).skillDataUI.name.ToString();
       
        _txtDescription.text = skillController.GetDataSkill(id).statSkillData.data[1].description;
     _txtValue.text= skillController.GetDataSkill(id).statSkillData.data[1].value.ToString("F0");
        Instantiate(iconSkillPrefab, containerPopup).TryGetComponent(out Icon_ListSkill icon);
        icon.SkillDetail(id, this);
        okbtn.onClick.AddListener(() =>
        {
            Debug.Log("ok");
            skillDetail.SetActive(false);

            foreach (Transform child in containerPopup)
            {
                if (child.TryGetComponent(out Icon_ListSkill icon) && icon.id == id)
                {
                    Destroy(child.gameObject);
                    break;
                }
            }

        });
    }


    private bool CheckSkillSet(SkillId id)
    {
        return app.models.dataPlayerModel.SkillSet.Contains(id);
    }
}