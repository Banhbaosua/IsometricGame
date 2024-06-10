using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillInventory", menuName = "Inventory/SkillInventory")]
public class SkillInventory : ScriptableObject
{
    private List<SkillSlot> skillSlots;
    private bool isFull;
    private Transform skillHolder;
    public Transform SkillHolder => skillHolder;
    public bool IsFull => isFull;
    public List<SkillSlot> SkillSlots => skillSlots;

    public void SetSkillHolder(Transform skillHolder)
    { 
        this.skillHolder = skillHolder;
    }

    public void GenerateSlotsInfo(List<SkillSlot> slots, List<SkillCard> cards)
    {
        skillSlots = slots;
        foreach(SkillSlot slot in skillSlots) 
        { 
            slot.GenerateCardList(cards);
        }
    }

    public void Full()
    {
        isFull = true;
    }

}
