using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData", order = 5)]
public class SkillDataTable : ScriptableObject
{
    public List<SkillData> listSkillData;
}
