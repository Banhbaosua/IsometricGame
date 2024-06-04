using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] SkillInventory skillInventory;
    [SerializeField] Transform player;
    
    void AddSkill(Skill skill)
    {
        skillInventory.SkillList.Add(skill);
    }

    void ReplaceSkill(Component sender, object data)
    {
        if (data.GetType() == typeof(SkillSlotInfo))
        {
            var slotInfo = (SkillSlotInfo)data;
            skillInventory.SkillList[slotInfo.slotIndex] = slotInfo.skill;
        }
    }
    
    public void SetPlayer(Component sender, object data)
    {
        if(data.GetType() == typeof(GameObject))
        { 
            var character = (GameObject)data;
            player = character.transform;
        }
    }
}
