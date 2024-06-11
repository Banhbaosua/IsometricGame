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
    private IDisposable levelUpSubscription;
    private List<int> rolledPU;
    private List<int> rolledSkill;
    private List<int> chosenSkill;
    private CompositeDisposable disposables;
    void EnableCards(Unit _)
    {
        cardsHolder.gameObject.SetActive(true);
        Time.timeScale = 0f;
        rolledPU.Clear();
        rolledSkill.Clear();
        RollAllCard();
        Debug.Log(chosenSkill.Count);
    }

    int RollSkill(SkillCard skillCard)
    {
        int rd = UnityEngine.Random.Range(0, skillTable.List.Count-1);
        while (rolledSkill.Contains(rd) || chosenSkill.Contains(rd))
        {
            rd = UnityEngine.Random.Range(0, skillTable.List.Count-1);
        }
        rolledSkill.Add(rd);
        

        Skill skill = skillTable.List[rd];
        skillCard.Set(null, skill);

        return rd;
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
    }

    private void Awake()
    {
        rolledPU = new();
        rolledSkill = new();
        chosenSkill = new();
        disposables = new CompositeDisposable();
        puTable.LoadSkillsFromResources();
        int baseSkilIndex = skillTable.List.IndexOf(currentClassData.GetWeapon().BaseSkill.Prefab.GetComponent<Skill>());
        chosenSkill.Add(baseSkilIndex);
    }

    private void OnDestroy()
    {
        levelUpSubscription?.Dispose();
    }
}
