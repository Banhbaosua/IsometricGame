using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CardRollingSystem : MonoBehaviour
{
    [SerializeField] SkillTable skillTable; //0
    [SerializeField] PowerUpTable puTable; //1
    [SerializeField] List<SkillCard> skillCards;
    [SerializeField] CharacterData characterData;
    [SerializeField] CurrentClassData currentClassData;
    [SerializeField] Transform cardsHolder;
    private XpSystem xpSystem;
    private SkillSystem skillSystem;
    private IDisposable levelUpSubscription;
    private List<int> rolledPU;
    private List<Skill> rolledSkill;
    private List<Skill> ChosenSkill => skillSystem.ChosenSkill;
    private CompositeDisposable disposables;
    private bool doneChoosing;
    public bool DoneChoosing => doneChoosing;

    void EnableCards(Unit _)
    {
        cardsHolder.gameObject.SetActive(true);
        Time.timeScale = 0f;
        rolledPU.Clear();
        rolledSkill.Clear();
        RollAllCard();
        doneChoosing = false;
    }

    Skill RollSkill(SkillCard skillCard)
    {
        int rd = UnityEngine.Random.Range(0, skillTable.List.Count);
        var skill = skillTable.List[rd];

        while (rolledSkill.Contains(skill) || ChosenSkill.Contains(skill))
        {
            rd = UnityEngine.Random.Range(0, skillTable.List.Count);
            skill = skillTable.List[rd];
        }
        rolledSkill.Add(skill);
        skillCard.Set(null, skill);
        skillCard.OnSkillChosen
            .Take(1)
            .Subscribe(_ => doneChoosing = true);
        return skill;
    }

    void RollPowerUP(SkillCard skillCard) 
    {
        int rd = UnityEngine.Random.Range(0, puTable.List.Count);
        while (rolledPU.Contains(rd))
        {
            rd = UnityEngine.Random.Range(0, puTable.List.Count);
        }

        rolledPU.Add(rd);


        PowerUps pu = puTable.List[rd];
        if (pu.Type == PowerUpType.Passive)
        {
            Passive puPassive = pu as Passive;
            puPassive.RollRarity();
            skillCard.Set(puPassive, null);
        }
        else
        {

        }

        skillCard.OnSkillChosen
            .Take(1)
            .Subscribe(_ => doneChoosing = true);
    }
    void RollAllCard()
    {
        foreach (SkillCard card in skillCards)
        {
            if (skillTable.List.Count - (ChosenSkill.Count + rolledSkill.Count) >0)
            {
                int rdSkillorPU = UnityEngine.Random.Range(0, 1);
                if (rdSkillorPU == 0)
                {
                    RollSkill(card);
                }
                else
                    RollPowerUP(card);
            }
            else
                RollPowerUP(card);
        }
    }
    private void Start()
    {
        xpSystem = FindObjectOfType<XpSystem>();
        if (xpSystem != null)
        {
            levelUpSubscription = xpSystem.OnLevelUP.Subscribe(EnableCards);
        }
        else
            Debug.LogError("XpSystem not found in the scene.");

        skillSystem = FindObjectOfType<SkillSystem>();
        if (xpSystem == null)
        {
            Debug.LogError("SkillSystem not found in the scene.");
        }
    }

    private void Awake()
    {
        rolledPU = new();
        rolledSkill = new();
        puTable.LoadSkillsFromResources();
    }

    private void OnDestroy()
    {
        levelUpSubscription?.Dispose();
    }
}
