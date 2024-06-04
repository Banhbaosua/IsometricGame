using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class CardRollingSystem : MonoBehaviour
{
    [SerializeField] SkillInventory skillInventory; //0
    [SerializeField] List<PowerUps> PUList; //1
    [SerializeField] List<SkillCard> skillCards;
    [SerializeField] CharacterData characterData;
    [SerializeField] Transform cardsHolder;
    private XpSystem xpSystem;
    private IDisposable levelUpSubscription;
    void EnableCards(Unit _)
    {
        cardsHolder.gameObject.SetActive(true);
        RollAllCard();
    }
    void RollSkillCard(SkillCard skillCard)
    {
        int rdSkillorPU = UnityEngine.Random.Range(1, 1);
        if(rdSkillorPU == 0)
        {
            int rd = UnityEngine.Random.Range(0, skillInventory.SkillList.Count);
            Skill skill = skillInventory.SkillList[rd];
            skillCard.Set(null, skill);
        }
        else
        {
            int rd = UnityEngine.Random.Range(0, PUList.Count);
            PowerUps pu = PUList[rd];
            if(pu.Type == PowerUpType.Passive)
            {
                Passive puPassive = pu as Passive;
                puPassive.RollRarity();
                Debug.Log(puPassive.RolldedStat.Value);
                skillCard.Set(puPassive, null);
            }
            else
            {

            }
            skillCard.Set(pu, null);
        }
    }

    void RollAllCard()
    {
        foreach(SkillCard card in skillCards)
        {
            RollSkillCard(card);
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

    private void OnDestroy()
    {
        levelUpSubscription?.Dispose();
    }
}
