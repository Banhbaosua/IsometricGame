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

    void EnableCards(Unit _)
    {
        cardsHolder.gameObject.SetActive(true);
        Time.timeScale = 0f;
        rolledPU.Clear();
        rolledSkill.Clear();
        RollAllCard();
    }

    Skill RollSkill(SkillCard skillCard)
    {
        int rd = UnityEngine.Random.Range(0, skillTable.List.Count-1);
        var skill = skillTable.List[rd];
        while (rolledSkill.Contains(skill) || ChosenSkill.Contains(skill) && (skillTable.List.Count - ChosenSkill.Count) >= 3)
        {
            rd = UnityEngine.Random.Range(0, skillTable.List.Count-1);
            skill = skillTable.List[rd];
        }
        rolledSkill.Add(skill);
        
        skillCard.Set(null, skill);
        return skill;
    }

    void RollPowerUP(SkillCard skillCard) 
    {
        int rd = UnityEngine.Random.Range(0, puTable.List.Count-1);
        while (rolledPU.Contains(rd))
        {
            rd = UnityEngine.Random.Range(0, puTable.List.Count-1);
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
    }
    void RollAllCard()
    {
        int rdSkillorPU = UnityEngine.Random.Range(0, 0);
        foreach (SkillCard card in skillCards)
        {
            if (rdSkillorPU == 0)
            {
                RollSkill(card);
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
