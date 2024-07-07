using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
[CreateAssetMenu(fileName = "CurrentClassData")]
public class CurrentClassData : ScriptableObject
{
    [SerializeField] private CharacterClassData characterClassData;
    [SerializeField] private BaseWeapon weapon;

    public void SetCharacterClass(CharacterClassData newChar)
    {
        characterClassData = newChar;
    }

    public void SetWeapon(BaseWeapon newWeap)
    {
        weapon = newWeap;
    }

    public BaseWeapon GetWeapon()
    {
        return weapon;
    }

    public CharacterClassData GetCharacterClassData() 
    { 
        return characterClassData;
    }
}
