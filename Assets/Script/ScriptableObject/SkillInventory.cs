using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillInventory",menuName ="Inventory/SkillInventory")]
public class SkillInventory : ScriptableObject
{
    [SerializeField] List<Skill> skillList;
    public List<Skill> SkillList => skillList;
}
