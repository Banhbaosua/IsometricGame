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

    [SerializeField,HideInInspector] private int currentTier = default;
    public int CurrentTier => currentTier;
    public SkillTreePassiveData Data => passives[currentTier];
    public Sprite Icon => icon;

    private void OnEnable()
    {
        currentTier = baseTier;
    }
    public void UnlockTier()
    {
        currentTier++;

        ApplyPassive();

        SaveUtility.SaveToJSON(this.name, Data);
    }

    void ApplyPassive()
    {
        var passiveStat = passives[currentTier];
        characterData.StatList[passiveStat.StatType].RemoveAllAddictiveStats(this);
        characterData.StatList[passiveStat.StatType].AddAddictiveStats(passiveStat.Value, this);
        Debug.Log(characterData.StatList[passiveStat.StatType].Value);
    }
    public void Initiate()
    {
        if(SaveUtility.LoadFromJSON<SkillTreePassiveData>(this.name) != default)
            currentTier = SaveUtility.LoadFromJSON<SkillTreePassiveData>(this.name).Tier;

        if (currentTier != default)
            ApplyPassive();
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
