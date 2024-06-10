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
    [SerializeField] Transform cardsHolder;
    private XpSystem xpSystem;
    private IDisposable levelUpSubscription;
    private List<int> rolledPU;
    private List<int> rolledSkill;
    void EnableCards(Unit _)
    {
        cardsHolder.gameObject.SetActive(true);
        rolledPU.Clear();
        RollAllCard();
    }

    void RollSkill(SkillCard skillCard)
    {
        int rd = UnityEngine.Random.Range(0, skillTable.List.Count);

        Skill skill = skillTable.List[rd];
        skillCard.Set(null, skill);
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
            Debug.Log(puPassive.RolldedStat.Value);
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
            if(rdSkillorPU == 0)
                RollSkill(card);
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
        puTable.LoadSkillsFromResources();
    }

    private void OnDestroy()
    {
        levelUpSubscription?.Dispose();
    }
}
