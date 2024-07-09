using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ClassData", menuName = "ScriptableObject/CharacterClass")]
public class CharacterClassData : ScriptableObject
{
    [SerializeField] private Sprite _icon; 
    [SerializeField] private GameObject _prefab;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private List<AddictiveStat> bonusStat;
    [SerializeField] private List<BaseWeapon> weaponList;
    [SerializeField] private int level;
    public bool isSelected;
    private CharacterShopData _characterShopData;
    public bool canSelect => _characterShopData.IsBought;
    public List<BaseWeapon> weapons => weaponList;
    public Sprite Icon => _icon;
    public int Level => level;
    public GameObject Prefab => _prefab;
    public List<AddictiveStat> BonusStatList => bonusStat;

    private void OnEnable()
    {
        if (_characterData == null)
            _characterData = Resources.LoadAll<CharacterData>("ScriptableObject/CharacterData")[0];
        _characterShopData = new CharacterShopData(this);

        foreach(var weapon in weaponList)
        {
            if(weapon.Tier == 1)
            {
                weapon.SetUnlock(true);
                continue;
            }
            var tempWeap = SaveUtility.LoadFromJSON<WeaponData>(weapon.name);
            if (tempWeap != null)
                weapon.SetUnlock(tempWeap.IsUnlock);
            else
                weapon.SetUnlock(false);
        }
    }

    public void ApplyBonus()
    {
        Debug.Log("apply bonus");
        if(bonusStat != null)
        foreach (var stat in bonusStat) 
        {
            if (_characterData.StatList.ContainsKey(stat.Type))
            {
                _characterData.StatList[stat.Type].AddAddictiveStats(stat.Value, _characterData);
                    
            }
        }
        isSelected = true;
    }

    public void RemoveBonus()
    {
        Debug.Log("remove bonus");
        foreach(var stat in bonusStat) 
        { 
            if(_characterData.StatList.ContainsKey(stat.Type))
            {
                _characterData.StatList[stat.Type].RemoveAllAddictiveStats(_characterData);
            }
        }
        isSelected = false;
    }

    public void OnBuy()
    {
        
    }

    public void OnEquip()
    {

    }
}
