using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(fileName = "SORef")]
public class ScriptableObjectRef : ScriptableObject
{
    [SerializeField] public List<ScriptableObject> CharacterSOList;
    public Dictionary<string, CharacterClassData> CharacterSODict;
    [SerializeField] List<ScriptableObject> WeaponSOList;
    public Dictionary<string, BaseWeapon> WeaponSODict;
    private void OnEnable()
    {
        CharacterSODict = CharacterSOList.ToDictionary(x => x.name, x => x as CharacterClassData);
        WeaponSODict = WeaponSOList.ToDictionary(x => x.name, x => x as BaseWeapon);
    }
}
