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

    IObservable<int> indexCombinedStream;
    IObservable<Skill> skillCombinedStream;
    public IObservable<(int, Skill)> indexSkillStream;

    private void Awake()
    {
        skillInventory.SetSkillHolder(this.transform);
        skillInventory.GenerateSlotsInfo(slots, cards);
        indexCombinedStream = Observable.Empty<int>();
        skillCombinedStream = Observable.Empty<Skill>();
        slots[0].Set(currentClassData.GetWeapon().BaseSkill.Prefab.GetComponent<Skill>());
    }

    private void Start()
    {
        for(int i = 0; i < slots.Count; i++) 
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
                 Debug.Log(x);
                 Add(x);
             });

        indexSkillStream = indexCombinedStream
            .Zip(skillCombinedStream, (index, skill) => (index,skill))
            .Take(1);

        indexSkillStream.Subscribe(x => Replace(x.Item1,x.Item2));
    }

    public void Add(Skill skill)
    {
        for (int i = 1; i < slots.Count; i++)
        {
            if (slots[i].CurrentSkill == null)
            {
                slots[i].Set(skill);
                if (i == 5)
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
