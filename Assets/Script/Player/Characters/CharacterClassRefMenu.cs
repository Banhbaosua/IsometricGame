using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClassRefMenu : MonoBehaviour
{
    [SerializeField] private CharacterClassData _class;
    [SerializeField] private GameObject model;
    [SerializeField] private Transform _mainWeaponHolder;
    [SerializeField] private Transform _offHandWeaponHolder;
    private void Awake()
    {
        if (_class != null)
        {
            if (_class.isSelected)
                model.SetActive(true);
            else 
                model.SetActive(false);
        }
    }


    public void EnableWeaponGO(Component sender, object data)
    {
        if (data == null)
            return;
        if(data.GetType() == typeof(BaseWeapon))
        {
            var weapon = (BaseWeapon)data;
            EnableWeapon(weapon, _mainWeaponHolder);
            EnableWeapon(weapon, _offHandWeaponHolder); 
        }
    }

    public void ChooseCharacter(Component sender, object data)
    {
        if(data.GetType() == typeof(CharacterClassData))
        {
            var charClass = (CharacterClassData)data;

            if (_class == charClass)
                model.SetActive(true);
            else
                model.SetActive(false);
        }
    }

    public void EnableWeapon(BaseWeapon weapon, Transform weaponHolder)
    {
        if (weaponHolder.transform.childCount == 0)
            return;

        if (weapon.CharacterClassData != _class)
            return;

        weaponHolder.GetChild(weapon.Tier - 1).gameObject.SetActive(true);

        for (int i = 0; i < weaponHolder.childCount; i++)
        {
            if (weaponHolder.GetChild(i).GetSiblingIndex() == weapon.Tier - 1)
                continue;

            weaponHolder.GetChild(i).gameObject.SetActive(false);
        }
    }
}
