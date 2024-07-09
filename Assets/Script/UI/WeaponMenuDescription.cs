using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuDescription : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text weaponName;
    [SerializeField] Text skillName;
    [SerializeField] Text skillDescription;
    [SerializeField] CurrentClassData currentClassData;
    private BaseWeapon weapon;
    //use for eventlistener
    private void OnEnable()
    {
        weapon = currentClassData.GetWeapon();
        Set(this, weapon);
    }
    public void Set(Component sender, object data)
    {
        var weapon = data as BaseWeapon;
        icon.sprite = weapon.WeapImg;
        weaponName.text = weapon.Name;
        skillName.text = weapon.BaseSkill.name;
        skillDescription.text = weapon.BaseSkill.Description;
    }

    public void Preview(Component sender, object data)
    {
        var character = data as CharacterClassData;
        Set(this, character.weapons[0]);
    }
}
