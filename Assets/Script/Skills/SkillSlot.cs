using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int slotIndex;
    [SerializeField] GameEvent onSkillReplace;
    private Skill skillToReplace;
    private SkillSlotInfo slotInfo;

    public void OnPointerClick(PointerEventData eventData)
    {
        WrapSkillSlotInfo(slotIndex, skillToReplace);
        onSkillReplace.Notify(this, slotInfo);
    }

    public void SkillCardInfo(Component sender, object data)
    {
        if(data.GetType() == typeof(Skill))
        {
            skillToReplace = (Skill)data;
        }
    }
    public void WrapSkillSlotInfo(int slotIndex, Skill skill)
    {
        slotInfo = new SkillSlotInfo(slotIndex, skill);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class SkillSlotInfo
{
    public int slotIndex;
    public Skill skill;
    public SkillSlotInfo(int slotIndex, Skill skill) 
    { 
        this.slotIndex = slotIndex;
        this.skill = skill;
    }
}
