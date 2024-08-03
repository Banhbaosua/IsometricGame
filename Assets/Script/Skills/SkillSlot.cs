using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] int slotIndex;
    [SerializeField] Button button;
    [SerializeField] Image icon;
    [SerializeField] Image coolDownImg;
    private List<SkillCard> skillCards;

    private Skill currentSkill;
    private Transform skillGO;
    private Subject<int> slotIndexChosen;
    public IObservable<int> SlotIndexChosen => slotIndexChosen;
    public Skill CurrentSkill => currentSkill;
    public List<SkillCard> SkillCards => skillCards;

    private void Awake()
    {
        DisableButton();
    }

    private void Update()
    {
        if(currentSkill != null) 
        {
            coolDownImg.fillAmount = (currentSkill.SkillCoolDown-currentSkill.Timer)/currentSkill.SkillCoolDown;
        }
    }
    public void Set(Skill skill)
    {
        skillGO = skill.InstantiateSkill(null).transform;
        skillGO.gameObject.SetActive(true);
        currentSkill = skillGO.GetComponent<Skill>();
        icon.sprite = skill.Icon;
    }

    public void Remove()
    {
        Destroy(skillGO.gameObject);
    }

    public void GenerateCardList(List<SkillCard> list)
    {
        skillCards = list;
    }

    public void Initiate()
    {
        slotIndexChosen = new Subject<int>();
        button.OnClickAsObservable().Subscribe(_ =>
        {
            slotIndexChosen.OnNext(slotIndex);
            foreach(SkillCard card in skillCards)
            {
                card.DisableSkillCard();
            }
        });
    }

    public void EnableButton()
    {
        button.interactable = true;
    }

    public void DisableButton()
    {
        button.interactable = false;
    }
}
