using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Weapon",menuName = "WeaponSO")]
public class BaseWeapon : ScriptableObject, IUnlockable
{
    [SerializeField] CharacterClassData characterClassData;
    [SerializeField] int _tier;
    [SerializeField] string _name;
    [SerializeField] GameObject weaponPrefabMain;
    [SerializeField] GameObject weaponPrefabOffhand;
    [SerializeField] Sprite weapImg;
    [SerializeField] SkillData baseSkill;
    [SerializeField] List<MaterialRequirement> unlockRequirement;
    [SerializeField] int minorSoulReq;
    [SerializeField] List<AddictiveStat> addictiveStat;
    public List<MaterialRequirement> UnlockRequirement => unlockRequirement;

    public bool hasOffhand;
    public Sprite WeapImg => weapImg;
    public SkillData BaseSkill => baseSkill;
    public GameObject WeaponPrefabMain => weaponPrefabMain;
    public GameObject WeaponPrefabOffhand => hasOffhand ? weaponPrefabOffhand : null;
    public int Tier => _tier;
    public CharacterClassData CharacterClassData => characterClassData;
    public string Name => _name;
    public int MinorSoulReq => minorSoulReq;
    [SerializeField] public bool isUnlocked { get; private set; }

    public void SetUnlock(bool value)
    {
        isUnlocked = value;
        SaveUtility.SaveToJSON(this.name, new WeaponData(value));
    }
}

public class WeaponData
{
    [SerializeField] bool isUnlock;
    public WeaponData(bool isUnlock)
    {
        this.isUnlock = isUnlock;
    }

    public bool IsUnlock => isUnlock;
}
