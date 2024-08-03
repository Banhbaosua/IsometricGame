using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using System;
using UniRx.Diagnostics;

public class SkillSystem : MonoBehaviour
{
    [SerializeField] SkillInventory skillInventory;
    [SerializeField] List<SkillSlot> slots;
    [SerializeField] List<SkillCard> cards;
    [SerializeField] CurrentClassData currentClassData;
    private List<Skill> chosenSkill;
    public List<Skill> ChosenSkill => chosenSkill;
    IObservable<int> indexCombinedStream;
    IObservable<Skill> skillCombinedStream;
    public IObservable<(int, Skill)> indexSkillStream;
    public bool IsFull => skillInventory.IsFull;

    private void Awake()
    {
        chosenSkill = new List<Skill>();
        indexCombinedStream = Observable.Empty<int>();
        skillCombinedStream = Observable.Empty<Skill>();
    }

    private void Start()
    {
        skillInventory.SetSkillHolder(this.transform);
        skillInventory.GenerateSlotsInfo(slots, cards);
        slots[0].Set(currentClassData.GetWeapon().BaseSkill.Prefab.GetComponent<Skill>());
        chosenSkill.Add(currentClassData.GetWeapon().BaseSkill.Prefab.GetComponent<Skill>());

        for (int i = 0; i < slots.Count; i++) 
        {
            slots[i].Initiate();
            indexCombinedStream = indexCombinedStream.Merge(slots[i].SlotIndexChosen);
        }

        for(int i = 0; i < cards.Count; i++)
        {
            cards[i].Initiate();
            skillCombinedStream = skillCombinedStream.Merge(cards[i].OnSkillChosen);
        }
        
        skillCombinedStream.Where(_ => !skillInventory.IsFull)
             .Subscribe(x=>
             {
                 Add(x);
             });

        skillCombinedStream.Where(_ => skillInventory.IsFull)
            .Subscribe(_ =>
            {
                foreach(var item in slots)
                {
                    item.EnableButton();
                }
            });

        indexSkillStream = indexCombinedStream
            .Where(_ => skillInventory.IsFull)
            .Zip(skillCombinedStream, (index, skill) => (index, skill))
            .Scan((previous, current) => (current.index, current.skill));
        
        indexSkillStream.Subscribe(x => Replace(x.Item1,x.Item2));
    }

    public void Add(Skill skill)
    {
        for (int i = 1; i < slots.Count; i++)
        {
            if (slots[i].CurrentSkill == null)
            {
                slots[i].Set(skill);
                chosenSkill.Add(skill);
                if (chosenSkill.Count == slots.Count)
                    skillInventory.Full();
                break;
            }
        }
    }
    public void Replace(int index, Skill skill)
    {
        slots[index].Remove();
        slots[index].Set(skill);
    }
}
