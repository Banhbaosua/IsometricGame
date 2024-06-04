using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDataComponentMenu : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private CharacterClassData _characterClassData;
    [SerializeField] private BaseWeapon _weaponData;
    [SerializeField] private ScriptableObjectRef _scriptableObjectRef;
    [SerializeField] private CurrentClassData _currentCharacterData;
    [SerializeField] GameEvent _onCharInitiate;
    public CharacterData CharData => _characterData;
    public CharacterClassData CharClassData
    {
        get { return _characterClassData; }
        private set
        {
            if (_characterClassData != null)
            {
                if (_characterClassData.name == value.name)
                    return;
                _characterClassData.RemoveBonus();
            }

            if (value == null)
                return;

            _characterClassData = value;
            _characterClassData.ApplyBonus();
        }
    }

    void Awake()
    {
        _characterData = Resources.LoadAll<CharacterData>("ScriptableObject/CharacterData")[0];

        //Load Character Class
        SaveData<string> className = SaveUtility.LoadFromJSON<SaveData<string>>("character");
        if (className != null)
            if (_scriptableObjectRef.CharacterSODict[className.value] != null)
                CharClassData = _scriptableObjectRef.CharacterSODict[className.value];
        if (CharClassData == null)
        {
            CharClassData = Resources.LoadAll<CharacterClassData>("ScriptableObject/ClassData/DefaultCharacter")[0];
        }
        _currentCharacterData.SetCharacterClass(CharClassData);
        CharClassData.isSelected = true;

        //Load weapon
        SaveData<string> weaponName = SaveUtility.LoadFromJSON<SaveData<string>>("currentWeapon");
        if(weaponName != null)
            if (_scriptableObjectRef.WeaponSODict.ContainsKey(weaponName.value))
                _weaponData = _scriptableObjectRef.WeaponSODict[weaponName.value];
        if (_weaponData == null)
        {
            _weaponData = CharClassData.weapons[0];
        }
        _currentCharacterData.SetWeapon(_weaponData);
    }

    private void Start()
    {
        _onCharInitiate.Notify(this, _weaponData);
    }

    public void ChooseCharacter(Component sender, object character)
    {
        if (character == null)
            return;
        if (character.GetType() == typeof(CharacterClassData))
        {
            CharClassData = character as CharacterClassData;
            _currentCharacterData.SetCharacterClass(CharClassData);

            SaveData<string> name = new SaveData<string>(CharClassData.name);
            SaveUtility.SaveToJSON("character", name);
        }
    }

    public void ChooseWeapon(Component sender, object weapon)
    {
        if (weapon == null || weapon.GetType() != typeof(BaseWeapon))
        {
            _weaponData = CharClassData.weapons[0];
        }
        if (weapon != null)
        {
            if (weapon.GetType() == typeof(BaseWeapon))
            {
                _weaponData = weapon as BaseWeapon;
            }
        }
        _currentCharacterData.SetWeapon(_weaponData);

        SaveData<string> name = new SaveData<string>(_weaponData.name);
        SaveUtility.SaveToJSON("currentWeapon", name);
    }

    
}