using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillTreePassive",menuName = "SkillTree/Passive")]
[Serializable]
public class SkillTreePassiveSO : ScriptableObject
{
    [SerializeField] List<SkillTreePassiveData> passives;
    [SerializeField] CharacterData characterData;
    [SerializeField] Sprite icon;
    [SerializeField] int baseTier;
    [SerializeField] bool firstPassive;
    [TextArea]
    [SerializeField] string description;

    [SerializeField,HideInInspector] private int currentTier = default;
    public int CurrentTier => currentTier;
    public bool FirstPassive => firstPassive;
    public SkillTreePassiveData NextTierData    =>  passives[currentTier];

    public Sprite Icon => icon;
    public string Description 
    {  
        get 
        {
            if (currentTier == 0)
                return description;
            else
                return description +" "+ passives[currentTier-1].Value.ToString();
        } 
    }

    private void OnEnable()
    {
        currentTier = baseTier;
    }
    public void UnlockTier()
    {
        currentTier++;
        ApplyPassive();

        SaveUtility.SaveToJSON(this.name, passives[currentTier-1]);
    }

    void ApplyPassive()
    {
        var passiveStat = passives[currentTier-1];
        characterData.StatList[passiveStat.StatType].RemoveAllAddictiveStats(this);
        characterData.StatList[passiveStat.StatType].AddAddictiveStats(passiveStat.Value, this);
    }
    public void Initiate()
    {
        if (SaveUtility.LoadFromJSON<SkillTreePassiveData>(this.name) != null)
        {
            currentTier = SaveUtility.LoadFromJSON<SkillTreePassiveData>(this.name).Tier;
        }
        if (currentTier > 0)
            ApplyPassive();
    }

    public void ResetTier()
    {
        currentTier = 0;
        Debug.Log(currentTier);
    }
}

[Serializable]
public class SkillTreeRequirement
{
    [SerializeField] int amount;
    [SerializeField] CurrencyType currencyType;
    public int CurrencyRequire => amount;
    public CurrencyType CurrencyType => currencyType;
}

[Serializable]
public class SkillTreePassiveData
{
    [SerializeField] int tier;
    [SerializeField] StatType statType;
    [SerializeField] float value;
    [SerializeField] SkillTreeRequirement requirements;
    private bool isUnlock;
    public bool IsUnlock => isUnlock;
    public float Value => value;
    public int Tier => tier;
    public StatType StatType => statType;
    public SkillTreeRequirement Requirements => requirements;

    public void Unlock()
    {

    }
}
