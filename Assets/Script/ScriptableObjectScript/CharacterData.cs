using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/Character")]
public class CharacterData : ScriptableObject
{
    #region on value change event
    public UnityEvent<float> onHealthChange;
    public UnityEvent<float> onCastFrequencyChange;
    public UnityEvent<float> onAreaModChange;
    #endregion
    private string _initialJson = string.Empty;
    private HealthController _healthController;
    public HealthController HealthController => _healthController;

    [SerializeField] Stat damageModifier = new Stat(0, StatType.DamageModifier);
    [SerializeField] Stat critDmgChance = new Stat(5, StatType.CritDmgChance);
    [SerializeField] Stat critDmgModifier = new Stat(200, StatType.CritDmgModifier);
    [SerializeField] Stat castFrequencyModifier = new Stat(0, StatType.CastFrequencyModifier);
    [SerializeField] Stat areaModifier = new Stat(0, StatType.AreaModifier);
    [SerializeField] Stat multicastChance = new Stat(0, StatType.MulticastChance);
    [SerializeField] Stat maxHealth = new Stat(100, StatType.MaxHealth);
    [SerializeField] Stat armorPower = new Stat(0, StatType.ArmorPower);
    [SerializeField] Stat dmgReduction = new Stat(0, StatType.DmgReduction);
    [SerializeField] Stat blockPower = new Stat(0, StatType.BlockPower);
    [SerializeField] Stat deathProtection = new Stat(0, StatType.DeathProtection);
    [SerializeField] Stat extraHealthPerCrystal = new Stat(0, StatType.ExtraHealthPerCrystal);
    [SerializeField] Stat healthPerLevel = new Stat(0, StatType.HealthPerLevel);
    [SerializeField] Stat movementSpdPerLvl = new Stat(0, StatType.MovementSpdModifier);
    [SerializeField] Stat dashCount = new Stat(1, StatType.DashCount);
    [SerializeField] Stat expModifier = new Stat(0, StatType.ExpModifier);
    [SerializeField] Stat pickUpRangeModifier = new Stat(0, StatType.PickUpRangeModifier);
    [SerializeField] Stat bonusMinorSoulstone = new Stat(0, StatType.BonusMinorSoulstone);
    [SerializeField] Stat rerollChances = new Stat(0, StatType.RerollChances);
    [SerializeField] Stat banishChances = new Stat(0, StatType.BanishChances);
    private Dictionary<StatType, Stat> statList = new();
    public Dictionary<StatType, Stat> StatList => statList;
    public float DamageModifier          => damageModifier.Value;
    public float CritDmgChance           => critDmgChance.Value;
    public float CritDmgModifier         => critDmgModifier.Value;
    public float CastFrequencyModifier
    {
        get
        {
            return castFrequencyModifier.Value;
        }
        set
        {
            castFrequencyModifier.AddAddictiveStats(value, this);
            onCastFrequencyChange?.Invoke(value);
        }
    }
    public float AreaModifier            => areaModifier.Value;
    public float MulticastChance         => multicastChance.Value;
    public float ArmorPower              => armorPower.Value;
    public float DmgReduction            => dmgReduction.Value;
    public float BlockPower              => blockPower.Value;
    public float DeathProtection         => deathProtection.Value;
    public float ExtraHealthPerCrystal   => extraHealthPerCrystal.Value;
    public float HealthPerLevel          => healthPerLevel.Value;
    public float MovementSpdPerLvl       => movementSpdPerLvl.Value;
    public float DashCount               => dashCount.Value;
    public float ExpModifier             => expModifier.Value;
    public float PickUpRangeModifier     => pickUpRangeModifier.Value;
    public float BonusMinorSoulston      => bonusMinorSoulstone.Value;
    public float RerollChances           => rerollChances.Value;
    public float BanishChances           => banishChances.Value;
    public float MaxHealth
    {
        get { return maxHealth.Value; }
        set 
        {
            if (value == maxHealth.Value)
            {
                return;
            }
            maxHealth.AddAddictiveStats(value,this);
            onHealthChange?.Invoke(value);
        }
    }

    public void ResetHealth()
    {
        maxHealth.RemoveAllAddictiveStats(this);
    }
    private void OnEnable()  
    {
        _initialJson = JsonUtility.ToJson(this);
        statList = new Dictionary<StatType, Stat>()
        {
            { StatType.DamageModifier,damageModifier },
            { StatType.CritDmgChance,critDmgChance },
            { StatType.CritDmgModifier,critDmgModifier },
            { StatType.CastFrequencyModifier,castFrequencyModifier },
            { StatType.AreaModifier,areaModifier },
            { StatType.MulticastChance,multicastChance },
            { StatType.MaxHealth,maxHealth },
            { StatType.ArmorPower,armorPower },
            { StatType.DmgReduction,dmgReduction},
            { StatType.BlockPower,blockPower },
            { StatType.DeathProtection,deathProtection },
            { StatType.ExtraHealthPerCrystal,extraHealthPerCrystal },
            { StatType.HealthPerLevel,healthPerLevel },
            { StatType.MovementSpdModifier,movementSpdPerLvl },
            { StatType.DashCount,dashCount },
            { StatType.ExpModifier,expModifier },
            { StatType.PickUpRangeModifier,pickUpRangeModifier },
            { StatType.BonusMinorSoulstone,bonusMinorSoulstone },
            { StatType.RerollChances,rerollChances },
            { StatType.BanishChances, banishChances }
        };

        _healthController = new HealthController(MaxHealth);
    }
    private void OnDisable()
    {
        JsonUtility.FromJsonOverwrite(_initialJson, this);
    }
}