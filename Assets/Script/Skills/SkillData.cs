using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName ="Data/SkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] Sprite icon;
    [SerializeField] float baseCoolDown;
    [SerializeField] float baseDamage;
    [SerializeField] GameObject prefab;
    [SerializeField] float castFreq;

    [TextArea]
    [SerializeField] string description;
    public string Description => description;
    public float BaseCoolDown => baseCoolDown;
    public float BaseDamage => baseDamage;
    public float CastFreq => castFreq;
    public Sprite Icon => icon;
}

public static class ModifyValueExtension
{
    public static void AddIntValue(this int value, int valueToAdd)
    {
        value += valueToAdd;
    }

    public static void AddFloatValue(this float value, float valueToAdd) 
    {
        value += valueToAdd;
    }

    public static void MultiplyValue(this float value, float valueToAdd)
    {
        value = value + value * valueToAdd;
    }
}
