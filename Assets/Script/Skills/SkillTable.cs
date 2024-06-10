using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName ="SkillTable",menuName ="Inventory/SkillTable")]
public class SkillTable : ScriptableObject
{
    [SerializeField] List<Skill> list;
    public List<Skill> List => list;

    public void Add(Skill skill)
    {
        list.Add(skill);
    }

}
