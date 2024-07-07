using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    DamageModifier,
    CritDmgChance,
    CritDmgModifier,
    CastFrequencyModifier,
    AreaModifier,
    MulticastChance,
    MaxHealth,
    ArmorPower,
    DmgReduction,
    BlockPower,
    DeathProtection,
    ExtraHealthPerCrystal,
    HealthPerLevel,
    MovementSpdModifier,
    DashCount,
    ExpModifier,
    PickUpRangeModifier,
    BonusMinorSoulstone,
    RerollChances,
    BanishChances,
}

[System.Serializable]
public class AddictiveStat
{
    [SerializeField] private float _value;
    
    [SerializeField] private StatType _type;
    
    [SerializeField] private Object _source;

    public Object Source => _source;
    public StatType Type => _type;
    public float Value => _value;
    public AddictiveStat(float value, Object source)
    {
        this._value = value;
        this._source = source;
    }
}

[System.Serializable]
public class Stat
{
    [SerializeField] private StatType Type;
    [SerializeField] private float _baseValue = 0;
    private float _finalValue = 0;
    private bool isDirty = true;
    private readonly List<AddictiveStat> addictiveStats;
    public List<AddictiveStat> AddictiveStats => addictiveStats;
    public StatType StatType => Type;
    public float Value
    {
        get 
        {   
            if(isDirty)
            {
                _finalValue = _baseValue + CalFinalStat();
                isDirty = false;
            }
            return _finalValue; 
        }
    }
    public Stat(float baseValue, StatType statType)
    {
        this._baseValue = baseValue;
        this.Type = statType;
        addictiveStats = new List<AddictiveStat>();
    }

    public void AddAddictiveStats(float value, Object source)
    {
        isDirty = true;
        addictiveStats.Add(new AddictiveStat(value,source));
    }

    public void RemoveAllAddictiveStats(Object source)
    { 
        for(int i = addictiveStats.Count -1; i >= 0;i--) 
        {
            if (addictiveStats[i].Source == source)
            {
                isDirty = true;
                addictiveStats.RemoveAt(i);
            }
        }
    }

    private float CalFinalStat()
    {
        float finalStat = 0;
        foreach(var stat in addictiveStats) 
        {
            finalStat += stat.Value;
        }

        return finalStat;
    }
}
